using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Model;
using XT_CETC23.DAL;

namespace XT_CETC23.Instances
{
    class TestingCabinets
    {
        static private Cabinet[] mTestingCabinet;
        private static readonly object lockRoot = new object();

        static public Cabinet getInstance(int ID)
        {
            if (mTestingCabinet == null)
            {
                lock (lockRoot)
                {
                    if (mTestingCabinet == null)
                    {
                        mTestingCabinet = new Cabinet[DeviceCount.TestingCabinetCount];
                    }
                }
            }
            if (mTestingCabinet.Length != DeviceCount.TestingCabinetCount)
            {
                // show error or renew
            }
            if (mTestingCabinet[ID] == null)
            {
                lock (lockRoot)
                {
                    if (mTestingCabinet[ID] == null)
                    {
                        mTestingCabinet[ID] = new Cabinet(ID);
                    }
                }
            }
            return mTestingCabinet[ID];
        }

        static public int getEnableCount()
        {
            int count = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; i ++ )
            {
                if (getInstance(i).Enable == TestingCabinet.ENABLE.Enable)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
