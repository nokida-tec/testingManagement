using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snap7;
using XT_CETC23.Config;
using XT_CETC23.DataManager;

namespace XT_CETC23
{
   public class Plc
    {
        static Plc plc;
        S7Client s7client = new S7Client();
        S7Client s7clientRead = new S7Client();
        byte[] myBytes=new byte[556];
        INTransfer.IMessage iMessage;
        //delegate void plcMessage(string message);
        //plcMessage PlcMessage;
        public bool plcConnected = false;
        object lockConnect = new object();
        object lockDbRead = new object();
        object lockDbWrite = new object();
       static object InitPlc = new object();
       public static Plc GetInstanse()//双重锁定多线程安全，懒装载
        {          
            if (plc==null)
            {
                lock (InitPlc)
                {
                    if(plc == null)
                    {
                        plc = new Plc();
                    }
                }               
            }
            return plc;
        }
        Plc()
        {
            //plc = this;
        }
        public bool ConnectPlc(string IPdress, int Rack, int Slot)
        {
            //int rack=Convert.ToInt16(Rack.ToString())
            lock (lockConnect)
            {
                if (!s7client.Connected())
                {
                    int result = s7client.ConnectTo(IPdress, Rack, Slot);
                    result = s7clientRead.ConnectTo(IPdress, Rack, Slot);
                    //int result = s7client.ConnectTo("192.168.0.10", 0, 0);
                    if (result == 0)
                    {
                        plcConnected = true;
                        return true;
                    }
                    else
                    {
                        plcConnected = false;
                        return false;
                    }
                }
                else
                {
                    //PlcMessage("PLC已经被连接");
                    return true;
                }
            }
        }
        public bool DisconnectPlc(string IPdress, int Rack, int Slot)
        {
            //int rack=Convert.ToInt16(Rack.ToString())
            lock (lockConnect)
            {
                int result = s7client.Disconnect();
                result = s7clientRead.Disconnect();
                if (result == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public byte[] DbRead(int DbNumber, int Start, int Size)
        {
            if (Config.Config.ENABLED_PLC == false)
            {
                return null;
            }
            lock (lockDbRead)
            {
                if (plcConnected)
                {
                    int Result = s7clientRead.DBRead(DbNumber, Start, Size, myBytes);
                    ShowResult(Result);
                    if (Result == 0)
                        return myBytes;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool DBWrite(int DBNumber, int Start,int Size, byte[] Data)
        {
            if (Config.Config.ENABLED_PLC == false)
            {
                return true;
            }
            // Declaration separated from the code for readability
            lock (lockDbWrite)
            {
                int Result;
                Result = s7client.DBWrite(DBNumber, Start, Size, Data);
                if(Result==0)
                {
                    ShowResult(Result);
                    return true;
                }
                else
                { ShowResult(Result); return false; }               
            }
        }
        private void ShowResult(int Result)
        {
            // This function returns a textual explaination of the error code
            //PlcMessage(s7client.ErrorText(Result) + " (" + s7client.ExecTime().ToString() + " ms)");
        }

        private Thread mThreadMonitor;
        public void start()
       {
           mThreadMonitor = new Thread(ReadPlc);
           mThreadMonitor.Name = "读取PLC数据";
       }

       private void ReadPlc()
       {
           int flag = 0;
           while (true)
           {
               try
               {
                   PlcData.PlcStatusValue = plc.DbRead(100, 0, 21);
                   if (PlcData.PlcStatusValue != null)
                   {
                       flag = 0;
                       PlcData._robotStatus = PlcData.PlcStatusValue[0];

                       PlcData._plcMode = PlcData.PlcStatusValue[1];

                       PlcData._axlis7Status = PlcData.PlcStatusValue[2];
                       PlcData._axlis2Status = PlcData.PlcStatusValue[3];
                       PlcData._axlis2Pos = PlcData.PlcStatusValue[4];
                       PlcData._cabinetStatus_old = PlcData.PlcStatusValue[5];

                       PlcData._frameFeedBack = PlcData.PlcStatusValue[6];                //料架气缸

                       PlcData._limitFeedBack1 = PlcData.PlcStatusValue[7];
                       PlcData._limitFeedBack2 = PlcData.PlcStatusValue[8];
                       PlcData._limitFeedBack3 = PlcData.PlcStatusValue[9];
                       PlcData._limitFeedBack4 = PlcData.PlcStatusValue[10];
                       PlcData._limitFeedBack5 = PlcData.PlcStatusValue[11];
                       PlcData._limitFeedBack6 = PlcData.PlcStatusValue[12];

                       PlcData._cabinetStatus[0] = PlcData.PlcStatusValue[13];
                       PlcData._cabinetStatus[1] = PlcData.PlcStatusValue[14];
                       PlcData._cabinetStatus[2] = PlcData.PlcStatusValue[15];
                       PlcData._cabinetStatus[3] = PlcData.PlcStatusValue[16];
                       PlcData._cabinetStatus[4] = PlcData.PlcStatusValue[17];
                       PlcData._cabinetStatus[5] = PlcData.PlcStatusValue[18];
                       PlcData._alarmNumber = PlcData.PlcStatusValue[20];
                       /*
                       #region  测试柜状态读取
                       for (int i = 0; i < 6; i++)
                       {

                           if (stepEnable == false)
                           {
                               if ((PlcData._cabinetStatus[i] & 1) != 0)
                                   GetCabinetStatus(i + 1, "可放料");
                               else if ((PlcData._cabinetStatus[i] & 2) != 0)
                                   GetCabinetStatus(i + 1, "可测试");
                               else if ((PlcData._cabinetStatus[i] & 4) != 0)
                                   GetCabinetStatus(i + 1, "测试中");
                               else if ((PlcData._cabinetStatus[i] & 8) != 0)
                                   GetCabinetStatus(i + 1, "可取料");
                               else
                                   GetCabinetStatus(i + 1, "准备中");
                           }
                       }
                       #endregion

                       #region  PLC控制状态读取和解析
                       if ((PlcData._plcMode & PlcData._readPlcMode) != 0)
                       {
                           modeByPlc = "Auto";
                       }
                       else
                       {
                           modeByPlc = "Manul";
                       }

                       if ((PlcData._plcMode & PlcData._readPlcInited) != 0)
                       {
                           commandByPlc = "Initialize";
                       }
                       if ((PlcData._plcMode & PlcData._readPlcStart) != 0)
                       {
                           commandByPlc = "Start";
                       }
                       if ((PlcData._plcMode & PlcData._readPlcEmergency) != 0)
                       {
                           commandByPlc = "Emergency";
                       }

                       if ((PlcData._plcMode & PlcData._readPlcAutoRunning) != 0)
                       {
                           statusByPlc = "AutoRunning";
                       }
                       if ((PlcData._plcMode & PlcData._readPlcPausing) != 0)
                       {
                           statusByPlc = "Pausing";
                       }
                       if ((PlcData._plcMode & PlcData._readPlcAlarming) != 0)
                       {
                           statusByPlc = "Alarming";
                       }
                       if ((PlcData._plcMode & PlcData._readPlcInited) != 0)
                       {
                           statusByPlc = "Initalized";
                       }
                       #endregion

                       GetRobotMode(PlcData._robotStatus);
                       GetPlcMode(modeByPlc, statusByPlc);
                       ManulEnable(modeByPlc, statusByPlc);

                       Thread.Sleep(100);
                        */
                   }
                   else
                   {
                       if (flag == 0)
                       {
                           Logger.WriteLine("PLC数据读取失败");
                       }
                       flag = 1;
                       Thread.Sleep(100);
                   }
               }
               catch (Exception e)
               {
                   Logger.WriteLine(e);
                   //MessageBox.Show(e.Message);
               }
               Thread.Sleep(100);
           }
       }
    }
}
