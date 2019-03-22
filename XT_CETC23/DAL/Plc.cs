using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snap7;
using XT_CETC23.Config;
using XT_CETC23.DataManager;
using System.Collections;

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
                        isConnected = true;
                        return true;
                    }
                    else
                    {
                        isConnected = false;
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
                try
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
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                    isConnected = false;
                }
                return false;
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
                if (isConnected)
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
                int Result = 0;
                if (Config.Config.ENABLED_CONTROL == true)
                {
                    Result = s7client.DBWrite(DBNumber, Start, Size, Data);
                }
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
        public void Start()
        {
            lock (this)
            {
                try
                {
                    if (mThreadMonitor == null)
                    {
                        mThreadMonitor = new Thread(ReadPlc);
                        mThreadMonitor.Name = "P L C 监控线程";
                    }
                    if (!mThreadMonitor.IsAlive)
                    {
                        mThreadMonitor.Start();
                    }
                }
                catch(Exception e)
                {
                    Logger.WriteLine(e);
                }
            }

        }

       public delegate void onDataChanged();
       private List<onDataChanged> mDelegateDataChanged = new List<onDataChanged>();
       public void RegistryDelegate(onDataChanged func)
       {
           lock (this)
           {
               if (!mDelegateDataChanged.Contains(func))
               {
                   mDelegateDataChanged.Add(func);
               }
           }
       }

       public void UnregistryDelegate(onDataChanged func)
       {
           lock (this)
           {
               if (mDelegateDataChanged.Contains(func))
               {
                   mDelegateDataChanged.Remove(func);
               }
           }
       }

       private void ReadPlc()
       {
           Logger.WriteLine(mThreadMonitor.Name + ": 启动");
           while (true)
           {
               try
               {
                   if (isConnected == false)
                   {
                       ConnectPlc("192.168.10.10", 0, 0);
                       continue;
                   }

                   PlcData.PlcStatusValue = plc.DbRead(100, 0, 21);
                   if (PlcData.PlcStatusValue != null)
                   {
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

                       lock (this)
                       {
                           foreach (onDataChanged func in mDelegateDataChanged)
                           {
                               func();
                           }
                       }
                   }
                   else
                   {
                       Logger.WriteLine("PLC数据读取失败");
                       isConnected = false;
                   }
               }
               catch (Exception e)
               {
                   Logger.WriteLine(e);
               }
               finally
               {
                   Thread.Sleep(100);
               }
           }
           Logger.WriteLine(mThreadMonitor.Name + ": 退出");
       }

       public void Suspend()
       {
           try
           {
               //mThreadMonitor.Suspend();
               //Logger.WriteLine("读取PLC状态进程 suspend");
           }
           catch (Exception e)
           {
               Logger.WriteLine(e);
           }
       }

       public void Resume()
       {
           try
           {
               if (mThreadMonitor.ThreadState == ThreadState.Suspended)
               {
                   mThreadMonitor.Resume();
                   Logger.WriteLine("读取PLC状态进程恢复");
               }
           }
           catch (Exception e)
           {
               Logger.WriteLine(e);
           }
       }


       private bool mPlcConnected = false;
       public delegate void onPlcConnected(bool status);
       private onPlcConnected mDelegatePlcConnected = null;
       public void  RegistryDelegate(onPlcConnected delegatePlcConnected)
       {
           mDelegatePlcConnected = delegatePlcConnected;
       }
       public void UnregistryDelegate(onPlcConnected delegatePlcConnected)
       {
           mDelegatePlcConnected = null;
       }

       public bool isConnected
       {
           set
           {
               try
               {
                   if (mPlcConnected != value)
                   {
                       if (mDelegatePlcConnected != null)
                       {
                           mDelegatePlcConnected(value);
                       }
                   }
               }
               catch (Exception e)
               {
                   Logger.WriteLine(e);
               }
               finally
               {
                   mPlcConnected = value;
               }
           }
           get 
           {
               return mPlcConnected;
           }
       }
    }
}
