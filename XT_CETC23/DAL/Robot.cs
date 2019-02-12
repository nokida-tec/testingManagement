using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.DataManager;
using System.Threading;

namespace XT_CETC23.DataCom
{
   public  class Robot
    {
        static Robot robot = null;
        Socket socketClient = null;
        System.Threading.Thread dThread;


        public static Robot GetInstanse()
        {
            if (robot == null)
            {
                robot = new Robot();
            }
            return robot;
        }

        Robot()
        {
            dThread = new Thread(new ThreadStart(readFunc));
            dThread.Name = "机器人操作";
            dThread.Start();
        }

        public bool tryConnected()
        {
            IPAddress ip = IPAddress.Parse("192.168.10.1");
            IPEndPoint iEndPoint = new IPEndPoint(ip, 1000);
            int tryCount = 50;
            bool isConnected = false;

            //if (socketClient != null)
            //{             
            //    socketClient.Close();
            //    socketClient.Dispose();
            //}

            while (isConnected == false && tryCount-- > 0)
            {
                try
                {
                    if (socketClient == null)
                    {
                        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
                        //socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
                    }

                    if (socketClient.Connected == false)
                    {
                        socketClient.Connect(iEndPoint);
                    }

                    isConnected = socketClient.Connected;
                }
                catch (Exception e)
                {
                    Console.WriteLine(" ***** " + e.StackTrace);
                    //if (e.HResult == )
                    {
                        socketClient.Shutdown(SocketShutdown.Both);
                        socketClient.Disconnect(true);
                        socketClient.Close();
                        socketClient = null;
                    }
                }
                Thread.Sleep(200);
            }
            return isConnected;
        }

        private void readFunc()
        {
            Console.WriteLine(" ***** Enter readFunc ****");
            int count = 50;
            while (true)
            {
                if (tryConnected() == true)
                {
                    try
                    {
                        byte[] arrMsgRec = new byte[20];
                        int length = socketClient.Receive(arrMsgRec);
                        String strMsgRec = Encoding.UTF8.GetString(arrMsgRec, 0, length);
                        RobotData.Response = strMsgRec;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" ***** " + e.StackTrace);
                    }
                }
                Thread.Sleep(100);
                count --;
                if (count == 0)
                {
                    sendDataToRobot("keepalive");
                    count = 50;
                }
            }
            Console.WriteLine(" ***** Exit readFunc ****");
        }

       
        public void sendDataToRobot(string sendStr)
        {
            if (tryConnected() == true)
            {
                try
                {
                    RobotData.Response = "";
                    string strMsg = sendStr;
                    byte[] arrMsg = Encoding.UTF8.GetBytes(strMsg);
                    byte[] arrMsgSend = new byte[arrMsg.Length];
                    // 添加标识位，0代表发送的是文字
                    arrMsgSend[0] = 0;
                    Buffer.BlockCopy(arrMsg, 0, arrMsgSend, 0, arrMsg.Length);
                    while (!socketClient.Connected)
                    {
                        Thread.Sleep(100);
                    }
                    socketClient.Send(arrMsg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" ***** " + e.Message);
                    Console.WriteLine(" ***** " + e.StackTrace);
                }
            }
            else
            {
                Console.WriteLine(" ***** try connect to robot failed ****");
            }
        }
    }
}
