using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    public enum ReturnCode
    {
        [EnumDescription("正常")]
        OK = 0,
        [EnumDescription("系统退出")]
        SystemExiting = -1,
        [EnumDescription("异常报警")]
        Exception = -2,
        [EnumDescription("扫描错误")]
        ScanFailed = -3,
        [EnumDescription("系统忙")]
        SysBusy = -4,
        [EnumDescription("没有产品")]
        NoProduct = -5,
    }
}
