using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Instances;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class TestingSystem
    {
        public enum Mode
        {
            [EnumDescription("自动模式")]
            Auto = 1,
            [EnumDescription("手动模式")]
            Manual = 2,
        }

        public enum Status
        {
            Normal = 1,//控件默认时
            MouseOver = 2,//鼠标移上控件时
            MouseDown = 3,//鼠标按下控件时
            Disable = 4 //当控件不可用时
        }

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
            return exitSystem;
        }

        public void SetExitSystem ()
        {
            exitSystem = false;
        }
    }
}
