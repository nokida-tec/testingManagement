using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XT_CETC23
{
    class WaitCondition
    {
        public delegate bool Condition();

        public static ReturnCode waitCondition(Condition condition)
        {
            while (!condition())
            {
                if (TestingSystem.GetInstanse().isSystemExisting() == true)
                {
                    return ReturnCode.SystemExiting;
                }
                Thread.Sleep(100);
            }
            return ReturnCode.OK;
        }
    }
}
