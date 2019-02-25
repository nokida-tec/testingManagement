﻿using System;
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
using XT_CETC23.Instances;

namespace XT_CETC23.DataCom
{
   public  class Robot
   {
       protected class Rail
       {
           private Object lockRail = new Object();
           private Robot mRobot = null;

           public enum Position
           { // 需要和PLC对应
               [EnumDescription("料架位置")]
               FramePos = 100,
               [EnumDescription("测试台1")]
               Cabinet1 = 101,
               [EnumDescription("测试台2")]
               Cabinet2 = 102,
               [EnumDescription("测试台3")]
               Cabinet3 = 103,
               [EnumDescription("测试台4")]
               Cabinet4 = 104,
               [EnumDescription("测试台5")]
               Cabinet5 = 105,
               [EnumDescription("测试台6")]
               Cabinet6 = 106,
           }

           public Rail (Robot robot)
           {
               mRobot = robot;
           }

           public ReturnCode doMoveToFrame()
           {
               lock (lockRail)
               {
                   try 
                   {
                       Logger.WriteLine("移动轨道到料架" + " 开始");

                       // 确保机器人在安全位置
                       WaitCondition.waitCondition(mRobot.isUsable);  // 不应该发生,双保险

                       // 移动轨道
                       Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis7Pos, PlcData._writeLength1, new byte[] { Convert.ToByte(Position.FramePos) });
                       
                       WaitCondition.waitCondition(isMoveFinished);

                       Plc.GetInstanse().DBWrite(100, 2, 1, new Byte[] { 0 }); // Todo: 什么意思

                       Logger.WriteLine("移动轨道到料架" + " 结束");
                       return ReturnCode.OK;
                   }
                   catch (AbortException ae)
                   {
                       Logger.WriteLine(ae);
                       throw ae;
                   }
                   catch (Exception e)
                   {
                       Logger.WriteLine(e);
                       throw e;
                   }
               }
           }


           public ReturnCode doMoveToCabinet(int cabinetNo)
           {   // 移动到测试柜
               lock (lockRail)
               {
                   try 
                   {
                       Logger.WriteLine("移动轨道到:" + (cabinetNo + 1) + " 开始");

                       WaitCondition.waitCondition(mRobot.isUsable);  // 不应该发生,双保险
                 
                       Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis7Pos, PlcData._writeLength1, new byte[] { Convert.ToByte(Position.Cabinet1 + cabinetNo) });

                       WaitCondition.waitCondition(isMoveFinished);

                       Plc.GetInstanse().DBWrite(100, 2, 1, new Byte[] { 0 }); // Todo: 什么意思

                       Logger.WriteLine("移动轨道到:" + (cabinetNo + 1) + " 完成");
                       return ReturnCode.OK;
                   }
                   catch (AbortException ae)
                   {
                       Logger.WriteLine(ae);
                       throw ae;
                   }
                   catch (Exception e)
                   {
                       Logger.WriteLine(e);
                       throw e;
                   }
               }
           }

           bool isMoveFinished()
           {  // 移动结束
               bool finished = (PlcData._axlis7Status == (byte)55);
               return finished;
           }
       }

       public Object lockRobot = new Object();
       
       private static Robot gRobot = null;
       protected Rail mRail = null;

       private Status mStatus;
        Socket socketClient = null;
        System.Threading.Thread dThread;

        public enum Status
        {
            Unkown = 0,
            Initializing = 1,
            Initialized = 2,
            Busy = 3,
            Closed = 4,
            Alarming = 5,
        }

        public static Robot GetInstanse()
        {
            if (gRobot == null)
            {
                gRobot = new Robot();
            }
            return gRobot;
        }

        private Robot()
        {
            mStatus = Status.Unkown;
            mRail = new Rail(this);
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
            try
            {
                RobotData.Command = command;
                sendDataToRobotAsync(command, param);
                return waitResponse(RobotData.Command + "Done");
            }
            catch(Exception e)
            {
                Logger.WriteLine(e);
                throw e;
            }
            finally
            {
                RobotData.Command = "";
                RobotData.Response = "";
            }
        }

        private void sendDataToRobotAsync(String command, string param = null)
        {  // 异步方式发送命令
                try
                {
                    Logger.WriteLine("sendDataToRobot Command:" + command);
                    Logger.WriteLine("sendDataToRobot Param:" + param);
                    // 不能设置命令名，因为有命令返回前有其他交互
                    // RobotData.Command = command;
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
                    throw new AbortException(ReturnCode.SystemExiting.ToString());
                } 
                Thread.Sleep(100);
            }
            return ReturnCode.OK;
        }
        
        private int doScanCodeDone ()
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
                try
                {
                    Logger.WriteLine("取产品: " + productType + " 从料架: " + position + " 开始");
                    if (x != null)
                    {
                        Logger.WriteLine("[x,y,z]: [" + x + "," + y + "," + z + "]");
                    }

                    // 移动到料架位置
                    mRail.doMoveToFrame();

                    // 等待料架准备好物品
                    WaitCondition.waitCondition(Frame.getInstance().canGetProduct);

                    // 机器人取料
                    RobotData.Command = "GetProTray";
                    sendDataToRobotAsync("GetProTray", productType + "," + position + "," + x + "," + y + "," + z);

                    //等待机器人触发扫码
                    waitResponse("ScanStart");

                    //通知Plc扫码
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, 1, new Byte[] { 33 });

                    //等待Plc扫码完成
                    WaitCondition.waitCondition(isCodeScanFinished);

                    ReturnCode ret = ReturnCode.OK;
                    if (PlcData._axlis2Status == 38)
                    {
                        ret = ReturnCode.ScanFailed;
                        //  scanStatus = false;
                    }
                    if (PlcData._axlis2Status == 33)
                    {
                        //  scanStatus = true;
                    }

                    doScanCodeDone();    // 给机器人发送扫码完成消息 
                    //等待机器人完成从料架取料
                    waitResponse("GetProTray" + "Done");

                    Thread.Sleep(2000);  // Todo：等待机器人离开危险位置？

                    Logger.WriteLine("取产品: " + productType + " 从料架: " + position + " 结束");
                    return ret;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
                finally
                {
                    RobotData.Command = "";
                    RobotData.Response = "";
                }
            }
        }

        bool isCodeScanFinished()
        {    // 扫码结束
            return (PlcData._axlis2Status == 33) || (PlcData._axlis2Status == 38);
        }

        public ReturnCode doPutProductToFrame(String productType, int position, String x = null, String y = null, String z = null)
        {   // 机器人放料
            lock (lockRobot)
            {
                try
                {
                    Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 开始");
                    if (x != null)
                    {
                        Logger.WriteLine("[x,y,z]: [" + x + "," + y + "," + z + "]");
                    }

                    // 机器人移动到料架位置
                    mRail.doMoveToFrame();

                    // 等待料架准备好
                    WaitCondition.waitCondition(Frame.getInstance().canPutProduct);

                    // 机器人放料
                    sendDataToRobotSync("PutProTray", productType + "," + position
                        + ((x != null) ? ("," + x + "," + y + "," + z) : ""));

                    Logger.WriteLine("放产品: " + productType + " 到料架: " + position + " 结束");
                    return ReturnCode.OK;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public ReturnCode doGetProductFromCabinet(String productType, int cabinetNo)
        {   // 机器人从测试台取料
            lock (lockRobot)
            {
                Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 开始");
                
                ReturnCode ret = ReturnCode.Exception;
                // 移动轨道到测试柜
                ret = mRail.doMoveToCabinet(cabinetNo);
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }

                // 检查测试柜是否打开且可取料
                WaitCondition.waitCondition(TestingCabinets.getInstance(cabinetNo).canGet);

                // 取料
                ret = sendDataToRobotSync("GetProTest", productType + "," + (cabinetNo + 1));
                if (ret != ReturnCode.OK)
                {
                    Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 系统终止");
                    return ret;
                }

                //通知PLC从测试柜取料完成
                TestingCabinets.getInstance(cabinetNo).finishGet();

                DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = 'Robot',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                
                Logger.WriteLine("取产品: " + productType + " 从测试台: " + (cabinetNo + 1) + " 完成");
                return ReturnCode.OK;
            }
        }

        public ReturnCode doPutProductToCabinet(String productType, int cabinetNo)
        {   // 机器人放料到测试台
            lock (lockRobot)
            {
                try
                {
                    Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 开始");

                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                    // 机器人移动到测试柜位置
                    mRail.doMoveToCabinet(cabinetNo);

                    // 检查测试柜是否打开且可放料
                    WaitCondition.waitCondition(TestingCabinets.getInstance(cabinetNo).canPut);

                    // 机器人放料
                    sendDataToRobotSync("PutProTest", productType + "," + (cabinetNo + 1));

                    // 更新任务状态
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);

                    Logger.WriteLine("放产品: " + productType + " 到测试台: " + (cabinetNo + 1) + " 完成");
                    return ReturnCode.OK;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public ReturnCode doStepRailMove(String pos)
        {
            /* *
            料架位
            1#测试位
            2#测试位
            3#测试位
            4#测试位
            5#测试位
            6#测试位
             * */
            lock (lockRobot)
            {
                Logger.WriteLine("单步移动轨道到: " + pos + " 开始");
                ReturnCode ret = ReturnCode.Exception;
                if (pos == "料架位") 
                {
                    ret = mRail.doMoveToFrame();
                } 
                else
                {
                    int cabinetNo = Convert.ToInt32(pos.Substring(0, 1));
                    ret = mRail.doMoveToCabinet(cabinetNo);
                }
                Logger.WriteLine("单步移动轨道到: " + pos + " 完成");
                return ret;
            }
        }
       
        public Status getStatus()
        {
            if (RobotData.Command.Length > 0)
            {
                return Status.Busy;
            }
            if (socketClient != null && socketClient.Connected)
            {
                return Status.Initialized;
            }
            return Status.Alarming;
        }

        public bool isUsable()
        { // 是否空闲
            bool usable = (getStatus() == Status.Initialized);
            return usable;
        }
    }
}
