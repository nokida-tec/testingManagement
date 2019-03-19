using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class Rail
    {
        static public Object lockRail = new Object();

        public enum Position
        {
            [EnumDescription("料架位置")]
            FramePos = 0,
            [EnumDescription("测试台1")]
            Cabinet1 = 31,
            [EnumDescription("测试台2")]
            Cabinet2 = 32,
            [EnumDescription("测试台3")]
            Cabinet3 = 33,
            [EnumDescription("测试台4")]
            Cabinet4 = 34,
            [EnumDescription("测试台5")]
            Cabinet5 = 35,
            [EnumDescription("测试台6")]
            Cabinet6 = 36,
        }
        
    }
}
