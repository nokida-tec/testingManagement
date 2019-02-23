using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using XT_CETC23.DAL;
using XT_CETC23.DataManager;
using XT_CETC23.Model;
using System.Threading.Tasks;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.Data;

namespace XT_CETC23
{
    class Frame
    {
        public class Lock
        {
            public enum State
            {
                [EnumDescription("Closed")]
                Closed = 1,
                [EnumDescription("Opened")]
                Opened = 0,
            }

            public enum Command
            {
                [EnumDescription("Close")]
                Close = 1,
                [EnumDescription("Open")]
                Open = 0,
            }

        }
        static private Frame mInstance;
        private static readonly object lockRoot = new object();

        static public Frame getInstance()
        {
            if (mInstance == null)
            {
                lock (lockRoot)
                {
                    if (mInstance == null)
                    {
                        mInstance = new Frame();
                    }
                }
            }
            return mInstance;
        }

        public bool excuteCommand(Lock.Command command)
        {
            byte[] myByte = new byte[1];
            switch (command)
            {
                case Lock.Command.Open:
                    myByte[0] = (byte)39;
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, PlcData._writeLength1, myByte);
                    break;
                case Lock.Command.Close:
                    myByte[0] = (byte)40;
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, PlcData._writeLength1, myByte);
                    break;
            }
            return false;
        }

        public bool canPutProduct()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.PutPiece;
        }

        public bool canGetProduct()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.GetPiece;
        }

        public bool isScanDone()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.ScanSort;
        }
    }
}
