using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23;
using XT_CETC23.DataManager;
using System.Threading;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23.DataCom
{
   public  class Robot
   {
       protected class Rail
       {
           static private Object lockRail = new Object();

           public enum Position
           {
               [EnumDescription("料架位置")]
               FramePos = 0,
               [EnumDescription("测试台1")]
               Cabinet1 = 31,
               [EnumDescription("测试台2")]
               Cabinet2 = 32,
               [EnumDescription("测试台3")]
               Cabinet3 = 33,
               [EnumDescription("测试台4")]
               Cabinet4 = 34,
               [EnumDescription("测试台5")]
               Cabinet5 = 35,
               [EnumDescription("测试台6")]
               Cabinet6 = 36,
           }

           public ReturnCode doMoveToFrame()
           {
               lock (lockRail)
               {
                   Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis7Pos, PlcData._writeLength1, new byte[] { Convert.ToByte(0) });
                   while (PlcData._axlis7Status != (byte)55)
                   {
                       if (TestingSystem.GetInstanse().isSystemExisting() == true)
                       {
                           return ReturnCode.SystemExiting;
                       }
                   }
                   Plc.GetInstanse().DBWrite(100, 2, 1, new Byte[] { 0 }); // Todo: 什么意思
                   //if (TaskCycle.actionType == "FrameToCabinet")
                   //{
                   //    TaskCycle.PickStep = TaskCycle.PickStep + 10;
                   //}
                   //if (TaskCycle.actionType == "CabinetToFrame")
                   //{
                   //    TaskCycle.PutStep = TaskCycle.PutStep + 10;
                   //}
                   return ReturnCode.OK;
               }
           }


           public ReturnCode doMoveToCabinet(int cabinetNo)
           {
               lock (lockRail)
               {
                   Logger.WriteLine("移动轨道到:" + (cabinetNo + 1) + " 开始");
                   Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis7Pos, PlcData._writeLength1, new byte[] { Convert.ToByte(101 + cabinetNo) });
                   while (PlcData._axlis7Status != (byte)55)
                   {
                       if (TestingSystem.GetInstanse().isSystemExisting() == true)
                       {
                           return ReturnCode.SystemExiting;
                       }
                   }
                   Plc.GetInstanse().DBWrite(100, 2, 1, new Byte[] { 0 }); // Todo: 什么意思
                   //if (TaskCycle.actionType == "FrameToCabinet")
                   //{
                   //    TaskCycle.PickStep = TaskCycle.PickStep + 10;
                   //}
                   //if (TaskCycle.actionType == "CabinetToFrame")
                   //{
                   //    TaskCycle.PutStep = TaskCycle.PutStep + 10;
                   //}
                   Logger.WriteLine("移动轨道到:" + (cabinetNo + 1) + " 完成");
                   return ReturnCode.OK;
               }
           }
       }

       static public Object lockRobot = new Object();

       private static Robot robot = null;
       protected Rail mRail = new Rail();

       private State state;
        Socket socketClient = null;
        System.Threading.Thread dThread;

        public enum State
        {
            Unkown = 0,
            Initializing = 1,
            Initialized = 2,
            Closed = 3,
            Alarming = 4,
        }

        public static Robot GetInstanse()
        {
            if (robot == null)
            {
                robot = new Robot();
            }
            return robot;
        }

        private Robot()
        {
            state = State.Unkown;
            mRail = new Rail();
        }

        ~Robot()
        {
            Close();
        }

        public bool Open()
        {
            Logger.WriteLine("Robot Open:" + socketClient);
            Close();
            if (tryConnectSocket(10 * 60 * 1000)) // 10分钟
            {
                if (dThread != null)
                {
                    dThread.Abort();
                    dThread = null;
                }
                dThread = new Thread(readFunc);
                dThread.Name = "机器人读线程";
                dThread.Start();
                Logger.WriteLine("Robot Read Thread start:" + socketClient);
                Logger.WriteLine("Robot Read Thread start:" + dThread);
                return true;
            }

            return false;
        }

        bool Close()
        {
            if (dThread != null)
            {
                dThread.Abort();
                dThread = null;
            } 

            CloseSocket();

            return true;
        }

        bool CloseSocket()
        {
            Logger.WriteLine("socket CloseSocket:" + socketClient);
            if (socketClient != null)
            {
                try
                {
                    socketClient.Shutdown(SocketShutdown.Both);
                    socketClient.Disconnect(true);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                socketClient.Close();
            }

            socketClient = null;

            return true;
        }


        public bool tryConnectSocket(int millisecond)
        {
            IPAddress ip = IPAddress.Parse("192.168.10.1");
            IPEndPoint iEndPoint = new IPEndPoint(ip, 1000);
//            Logger.WriteLine("socket tryConnectSocket:" + millisecond);

            int timeInter = 200;
            int tryCount = (millisecond < 0) ? 99999 : (millisecond + timeInter) / timeInter;
            bool isConnected = false;

            while (isConnected == false && tryCount -- > 0)
            {
                try
                {
                    if (socketClient == null)
                    {
                        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
                        //socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                        Logger.WriteLine("New socket:" + socketClient);

                    }

                    if (socketClient.Connected == false)
                    {
                        socketClient.Connect(iEndPoint);
                        Logger.WriteLine("socket connect:" + socketClient);
                    }

                    isConnected = socketClient.Connected;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    CloseSocket();
                }
                Thread.Sleep(timeInter);
            }
            return isConnected;
        }

        private void readFunc()
        {
            int count = 2;
            while (true)
            {
                if (tryConnectSocket(60 * 1000) == true)
                {
                    try
                    {
                        byte[] arrMsgRec = new byte[20];
                        int length = socketClient.Receive(arrMsgRec);
                        String strMsgRec = Encoding.UTF8.GetString(arrMsgRec, 0, length);
                        RobotData.Response = strMsgRec;
                        Logger.WriteLine("RobotData.Response:" + strMsgRec);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(e);
                    }
                }
                
                Thread.Sleep(100);
                count --;
                if (count == 0)
                {
//                    sendDataToRobot("keepalive");
                    count = 2;
                }
            }
        }

        private ReturnCode sendDataToRobotSync(String command, string param = null)
        {  // 同步方式发送命令
            sendDataToRobotAsync(command, param);
            return waitResponse(RobotData.Command + "Done");
        }

        private void sendDataToRobotAsync(String command, string param = null)
        {  // 异步方式发送命令
                try
                {
                    Logger.WriteLine("sendDataToRobot Command:" + command);
                    Logger.WriteLine("sendDataToRobot Param:" + param);
                    RobotData.Command = command;
                    RobotData.Response = "";
                    string strMsg = (param != null) ? (command + "," + param) : command;
                    byte[] arrMsg = Encoding.UTF8.GetBytes(strMsg);
                    byte[] arrMsgSend = new byte[arrMsg.Length];
                    // 添加标识位，0代表发送的是文字
                    arrMsgSend[0] = 0;
                    Buffer.BlockCopy(arrMsg, 0, arrMsgSend, 0, arrMsg.Length);
                    socketClient.Send(arrMsg);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
        }

        private ReturnCode waitResponse(String response)
        {
            while (RobotData.Response == null || RobotData.Response != response)
            {
                if (TestingSystem.GetInstanse().isSystemExisting() == true)
                {
                    return ReturnCode.SystemExiting;
                } 
                Thread.Sleep(100);
            }
            return ReturnCode.OK;
        }

        public ReturnCode waitCondition(bool condition)
        {
            while (!condition)
            {
                if (TestingSystem.GetInstanse().isSystemExisting() == true)
                {
                    return ReturnCode.SystemExiting;
                }
                Thread.Sleep(100);
            }
            return ReturnCode.OK;
        }

        
        public int doScanCodeDone ()
        {
             sendDataToRobotAsync("ScanDone");              //给机器人发送扫码完成消息 
             return 10;
        }

        public ReturnCode doMoveToZeroPos()
        {
            lock (lockRobot)
            {
                return mRail.doMoveToFrame();
            }
        }

        public ReturnCode doGetProductFromFrame(String productType, int position, String x, String y, String z)
        {   // 机器人取料
            lock (lockRobot)
            {
                Logger.WriteLine("取产品: " + productType + " 从料架: " + position + " 开始");
                if (x != null)
                {
                    Logger.WriteLine("[x,y,z]: [" + x + "," + y + "," + z + "]");
                }

                ReturnCode ret = ReturnCode.Exception;
                ret = mRail.doMoveToFrame();
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("取产品: " + productType + " 从料架: " + position + " 系统终止");
                    return ret;
                }

                sendDataToRobotAsync("GetProTray", productType + "," + position + "," + x + "," + y + "," + z);

                //等待机器人触发扫码
                ret = waitResponse("ScanStart");
                if (ret != ReturnCode.OK)
                {
                    return ret;
                }

                //通知Plc扫码
                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, 1, new Byte[] { 33 });

                //等待Plc扫码完成
                if (waitCondition((PlcData._axlis2Status == 33) || (PlcData._axlis2Status == 38)) != ReturnCode.OK)
                {
                    return ReturnCode.SystemExiting;
                }

                if (PlcData._axlis2Status == 38)
                {
                    //  scanStatus = false;
                }
                if (PlcData._axlis2Status == 33)
                {
                    //  scanStatus = true;
                }

                doScanCodeDone();              // 给机器人发送扫码完成消息 
                Logger.WriteLine("取产品: " + productType + " 从料架: " + position + " 结束");
                return ReturnCode.OK;
            }
        }

        public ReturnCode doPutProductToFrame(String productType, int position, String x = null, String y = null, String z = null)
        {   // 机器人放料
            lock (lockRobot)
            {
                Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 开始");
                if (x != null)
                {
                    Logger.WriteLine("[x,y,z]: [" + x + "," + y + "," + z + "]");
                }

                ReturnCode ret = ReturnCode.Exception;
                ret = mRail.doMoveToFrame();
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 系统终止");
                    return ret;
                }

                ret = sendDataToRobotSync("PutProTray", productType + "," + position 
                    + ((x != null) ? ("," + x + "," + y + "," + z) : ""));
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 系统终止");
                    return ret;
                }
                Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 结束");
                return ReturnCode.OK;
            }
        }

        public ReturnCode doGetProductFromCabinet(String productType, int cabinetNo)
        {   // 机器人从测试台取料
            lock (lockRobot)
            {
                Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 开始");

                ReturnCode ret = ReturnCode.Exception;
                ret = mRail.doMoveToCabinet(cabinetNo);
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }

                ret = sendDataToRobotSync("GetProTest", productType + "," + (cabinetNo + 1));
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }

                //通知PLC从测试柜取料完成
                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 8 });

                DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = 'Robot',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 完成");
                return ReturnCode.OK;
            }
        }

        public ReturnCode doPutProductToCabinet(String productType, int cabinetNo)
        {   // 机器人放料到测试台
            lock (lockRobot)
            {
                Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 开始");
                ReturnCode ret = ReturnCode.Exception;
                ret = mRail.doMoveToCabinet(cabinetNo);
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }
                ret = sendDataToRobotSync("PutProTest", productType + "," + (cabinetNo + 1));
                if (ret != ReturnCode.OK) 
                {
                    Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }

                DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 完成");
                return ReturnCode.OK;
            }
        }
    }
}
