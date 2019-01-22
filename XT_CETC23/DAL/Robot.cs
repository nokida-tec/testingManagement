using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using XT_CETC23.DataManager;
using System.Threading;

namespace XT_CETC23.DataCom
{
   public  class Robot
    {
        static Robot robot = null;
        Socket socketClient = null;
        IPAddress ip;
        IPEndPoint iEndPoint = null;
        System.Threading.Thread dThread;
        public bool robotConnected = false;
        public bool robotInitialized = false;
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
            dThread = new Thread(new ThreadStart(DataStream));
            dThread.Name = "机器人操作";
            dThread.IsBackground = true;
            //dThread.Start();
            //robot = this;
        }
        public bool InitRobot()
        {
            ip = IPAddress.Parse("192.168.10.1");
            iEndPoint = new IPEndPoint(ip, 1000);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socketClient.Connect(iEndPoint);
               if(!dThread.IsAlive)
                {
                    dThread.Start();                    
                }
                robotInitialized = true;
                return true;
            }
            catch (SocketException sex)
            {
                return false;
            }

        }

        public void RobotSocketReconnect()
        {
            ip = IPAddress.Parse("192.168.10.1");
            iEndPoint = new IPEndPoint(ip, 1000);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(iEndPoint);
        }

        private void DataStream()
        {
            while (true)
            {
                if (socketClient.Connected)
                {
                    robotConnected = true;
                    byte[] arrMsgRec = new byte[20];
                    int length = socketClient.Receive(arrMsgRec);
                    String strMsgRec = Encoding.UTF8.GetString(arrMsgRec, 0, length);
                    RobotData.Response = strMsgRec;
                }
                else
                {
                    robotConnected = false;
                    socketClient.Close();
                    socketClient.Dispose();             
                }
                Thread.Sleep(100);
            }
        }
        public void sendDataToRobot(string sendStr)
        {
            if (socketClient.Connected)
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
        }
    }
}
