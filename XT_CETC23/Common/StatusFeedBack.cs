using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.Common
{
    class StatusFeedBack
    {
        //PLC Mode
        public static string PlcMode;

        //Robot Mode
        public static string RobotMode;

        //Cabinet Mode,是否投用
        public static string Cabinet1Mode;
        public static string Cabinet2Mode;
        public static string Cabinet3Mode;
        public static string Cabinet4Mode;
        public static string Cabinet5Mode;
        public static string Cabinet6Mode;
        //Cabinet Status,在线 运行 故障
        public static string Cabinet1Status;
        public static string Cabinet2Status;
        public static string Cabinet3Status;
        public static string Cabinet4Status;
        public static string Cabinet5Status;
        public static string Cabinet6Status;
        //Cabinet Result,OK NG
        public static string Cabinet1Result;
        public static string Cabinet2Result;
        public static string Cabinet3Result;
        public static string Cabinet4Result;
        public static string Cabinet5Result;
        public static string Cabinet6Result;
    }
}
