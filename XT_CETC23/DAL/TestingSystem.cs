using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Instances;

namespace XT_CETC23
{
    class TestingSystem
    {
        private static TestingSystem instance = null;
        private  bool exitSystem = false;

        public static TestingSystem GetInstanse()
        {
            if (instance == null)
            {
                instance = new TestingSystem();
            }
            return instance;
        }

         private TestingSystem()
        {
        }

         ~TestingSystem()
        {
        }

        public void ExitSystem ()
        {
            exitSystem = true;
            TestingCabinets.abort();
        }

        public bool isSystemExisting()
        {
            //return exitSystem;
            return true;
        }

        public void SetExitSystem ()
        {
            exitSystem = false;
        }
    }
}
