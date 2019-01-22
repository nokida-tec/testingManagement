using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;

namespace XT_CETC23.DataCom
{
    public static class Camera
    {
        public static CogAcqFifoTool CCD;
        public static CogToolBlock Tool;
        public static string ccd1Path, tool1Path;
        public static Result CCDResult;
        public struct Result
        {
            public int CCDDone;
            public double XPos;
            public double YPos;
            public double Angel;
        }

    }
}
