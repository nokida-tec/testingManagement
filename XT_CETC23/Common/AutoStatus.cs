using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.Common
{
    class AutoStatus
    {
        public static bool NewFrame;//判断是不是新的料架
        public static bool FrameScanOver;//判断料架是否扫描完成
        public static bool FrameGetPiece;//判断料架取料是否完成
        public static bool FrameDropPiece;//判断料架放料是否完成
        public static bool CabinetCyOver;//判断机台是否夹紧，允许启动测试
        public static bool Axlis7Pos;//判断7轴位置是否到达
    }
}
