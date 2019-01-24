using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
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
    }
}
