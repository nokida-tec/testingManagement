using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;

namespace XT_CETC23.Model
{
    class TestingBed
    {
        enum Cabinet
        {
            [EnumDescription("NotReady")]
            NotReady = 0,
            [EnumDescription("Ready")]
            Ready = 30,
            [EnumDescription("Testing")]
            Testing = 31,
            [EnumDescription("Fault_Config")]
            Fault_Config = 32,
            [EnumDescription("Fault_Control")]
            Fault_Control = 33,
            [EnumDescription("Fault_Report")]
            Fault_Report = 34,
            [EnumDescription("Finished")]
            Finished = 40,
            [EnumDescription("OK")]
            OK = 100,
            [EnumDescription("NG")]
            NG = 101,
        }
    }
}
