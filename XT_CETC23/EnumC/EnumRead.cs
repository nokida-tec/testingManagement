using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;

namespace XT_CETC23.EnumC
{
    enum Robot
    {
        [EnumDescription("机器人故障")]
        Fault,
        [EnumDescription("机器人上电完成")]
        PowerOnOver,
        [EnumDescription("机器人空闲中")]
        Freeing,
        [EnumDescription("机器人暂停中")]
        Pauseing,
        [EnumDescription("机器人运行中")]
        Running,
    }

    enum Plc
    {
        [EnumDescription("OFF模式")]
        OffMode = 21,
        [EnumDescription("手动，未准备好自动")]
        ManulNoReady = 22,
        [EnumDescription("手动准备好自动")]
        ManulReady = 23,
        [EnumDescription("自动模式")]
        Auto = 24,
        [EnumDescription("自动模式中")]
        AutoRuning = 25,
    }
    enum Grab
    {
        [EnumDescription("A组件")]
        SortA,
        [EnumDescription("B组件")]
        SortB,
        [EnumDescription("2类组件")]
        Sort2,
        [EnumDescription("AB组件")]
        SortAB,
        [EnumDescription("C组件")]
        SortC,
        [EnumDescription("D组件")]
        SortD,
    }
    enum RobotS
    {
        [EnumDescription("机器人在取料位")]
        GetPiece=100,
        [EnumDescription("机器人在1#机台放料位")]
        PutPiece_Cabinet1=101,
        [EnumDescription("机器人在2#机台放料位")]
        PutPiece_Cabinet2=102,
        [EnumDescription("机器人在3#机台放料位")]
        PutPiece_Cabinet3=103,
        [EnumDescription("机器人在4#机台放料位")]
        PutPiece_Cabinet4=104,
        [EnumDescription("机器人在5#机台放料位")]
        PutPiece_Cabinet5=105,
        [EnumDescription("机器人在6#机台放料位")]
        PutPiece_Cabinet6=106,
    }
}
