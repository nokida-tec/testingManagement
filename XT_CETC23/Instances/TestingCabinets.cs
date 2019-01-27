using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Model;

namespace XT_CETC23.Instances
{
    class TestingCabinets
    {
        static private TestingCabinet[] mTestingCabinet;

        static public TestingCabinet getInstance(int ID)
        {
            if (mTestingCabinet == null)
            {
                mTestingCabinet = new TestingCabinet[DeviceCount.TestingCabinetCount];
            }
            if (mTestingCabinet.Length != DeviceCount.TestingCabinetCount)
            {
                // show error or renew
            }
            if (mTestingCabinet[ID] == null)
            {
                mTestingCabinet[ID] = new TestingCabinet(ID);
            }
            return mTestingCabinet[ID];
        }
    }
}
