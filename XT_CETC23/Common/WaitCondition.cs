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
            while (Config.Config.ENABLED_DEBUG == false && !condition())
            {
                if (TestingSystem.GetInstance().isSystemExisting() == true)
                {
                    throw new AbortException(ReturnCode.SystemExiting.ToString());
                }
                Thread.Sleep(100);
            }
            return ReturnCode.OK;
        }
    }
}
