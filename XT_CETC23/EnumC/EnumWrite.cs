using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;

namespace XT_CETC23.EnumC
{
   
    //EnumHelper.GetDescription(Week.Monday);
    
    enum RobotW
    {
        [EnumDescription("机器人上电")]
        PowerOn=11,
        [EnumDescription("机器人运行")]
        Run=12,

    }
    enum RobotC
    {
        [EnumDescription("机器人取料")]
        GetPiece = 100,
        [EnumDescription("机器人放料")]
        DropPiece = 101,

    }
    enum PlcModeW
    {
        [EnumDescription("OFF模式")]
        OffMode = 21,
        [EnumDescription("手动模式")]
        ManulMode = 22,
        [EnumDescription("自动模式")]
        AutoMode = 23,

    }
    enum PlcAxlis7W
    {
        [EnumDescription("7轴到料架位置")]
        Axlis7ToFram = 100,
        [EnumDescription("7轴到1#检测位")]
        Axlis7ToPos1 = 101,
        [EnumDescription("7轴到2#检测位")]
        Axlis7ToPos2 = 102,
        [EnumDescription("7轴到3#检测位")]
        Axlis7ToPos3 = 103,
        [EnumDescription("7轴到4#检测位")]
        Axlis7ToPos4 = 104,
        [EnumDescription("7轴到5#检测位")]
        Axlis7ToPos5 = 105,
        [EnumDescription("7轴到6#检测位")]
        Axlis7ToPos6 = 106,

    }
    enum FrameW
    {
        [EnumDescription("启动扫描")]
        ScanSort =31,
        [EnumDescription("启动取料")]
        GetPiece,
        [EnumDescription("启动产品扫码")]
        ScanPiece,
        [EnumDescription("启动放料")]
        PutPiece,
    }
    enum EquipmentPos
    {
        [EnumDescription("料架位")]
        FramePos,
        [EnumDescription("1#检测位")]
        CheckCabinet1Pos,
        [EnumDescription("2#检测位")]
        CheckCabinet2Pos,
        [EnumDescription("3#检测位")]
        CheckCabinet3Pos,
        [EnumDescription("4#检测位")]
        CheckCabinet4Pos,
        [EnumDescription("5#检测位")]
        CheckCabinet5Pos,
        [EnumDescription("6#检测位")]
        CheckCabinet6Pos,
        [EnumDescription("机器人")]
        RobotPos,
    }
}
