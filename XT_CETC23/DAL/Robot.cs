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

namespace XT_CETC23.DataCom
{
   public  class Robot
    {
        private static Robot robot = null;
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
                catch (Exception e2)
                {
                    Logger.printException(e2);
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
                    Logger.printException(e);
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
                        Logger.printException(e);
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

        public void sendDataToRobot(string sendStr)
        {
                try
                {
                    Logger.WriteLine("sendDataToRobot:" + sendStr);
                    RobotData.Response = "";
                    string strMsg = sendStr;
                    byte[] arrMsg = Encoding.UTF8.GetBytes(strMsg);
                    byte[] arrMsgSend = new byte[arrMsg.Length];
                    // 添加标识位，0代表发送的是文字
                    arrMsgSend[0] = 0;
                    Buffer.BlockCopy(arrMsg, 0, arrMsgSend, 0, arrMsg.Length);
                    socketClient.Send(arrMsg);
                }
                catch (Exception e)
                {
                    Logger.printException(e);
                }
        }
    }
}
