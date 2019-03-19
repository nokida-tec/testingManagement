using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23
{
    class TestingTasks
    {
        static private TestingTask[] mInstances;
        private static readonly object lockRoot = new object();

        static public TestingTask getInstance(int ID)
        {
            if (mInstances == null)
            {
                lock (lockRoot)
                {
                    if (mInstances == null)
                    {
                        mInstances = new TestingTask[DeviceCount.TestingCabinetCount];
                    }
                }
            }
            if (mInstances.Length != DeviceCount.TestingCabinetCount)
            {
                // show error or renew
            }
            if (mInstances[ID] == null)
            {
                lock (lockRoot)
                {
                    if (mInstances[ID] == null)
                    {
                        mInstances[ID] = new TestingTask(ID);
                    }
                }
            }
            return mInstances[ID];
        }

        // 紧急停止
        static public int Abort()
        {
            int count = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; i++)
            {
                getInstance(i).Abort();
            }
            return count;
        }

        static public int Pause()
        {
            int count = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; i++)
            {
                getInstance(i).Pause();
            }
            return count;
        }

        static public int Resume()
        {
            int count = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; i++)
            {
                getInstance(i).Resume();
            }
            return count;
        }
    }
}
