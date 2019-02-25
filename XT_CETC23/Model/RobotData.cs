using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.DataManager
{
    public static class RobotData
    {
        public static string robotStatus_orderConfirm = "指令确认";
        public static string robotStatus_orderOver = "指令完成";
        //public static byte[] robotData=new byte[10];

        public static string Command = "";
        public static string Response = "";

        /* byte[0]判断机器人是否在原点
         * byte[1]判断机器人料架操作的状态
         * =1  机器人料架取料中
         * =2  机器人料架取料完成
         * =3  机器人料架放料中
         * =4  机器人料架放料完成
         * byte[2]判断机器人1#检测柜操作的状态
         * =1  机器人1#检测柜取料中
         * =2  机器人1#检测柜取料完成
         * =3  机器人1#检测柜放料中
         * =4  机器人1#检测柜放料完成
         * byte[3]判断机器人2#检测柜操作的状态
         * =1  机器人2#检测柜取料中
         * =2  机器人2#检测柜取料完成
         * =3  机器人2#检测柜放料中
         * =4  机器人2#检测柜放料完成
         * byte[4]判断机器人3#检测柜操作的状态
         * =1  机器人3#检测柜取料中
         * =2  机器人3#检测柜取料完成
         * =3  机器人3#检测柜放料中
         * =4  机器人3#检测柜放料完成
         * byte[5]判断机器人4#检测柜操作的状态
         * =1  机器人4#检测柜取料中
         * =2  机器人4#检测柜取料完成
         * =3  机器人4#检测柜放料中
         * =4  机器人4#检测柜放料完成
         * byte[6]判断机器人5#检测柜操作的状态
         * =1  机器人5#检测柜取料中
         * =2  机器人5#检测柜取料完成
         * =3  机器人5#检测柜放料中
         * =4  机器人5#检测柜放料完成
         * byte[7]判断机器人6#检测柜操作的状态
         * =1  机器人6#检测柜取料中
         * =2  机器人6#检测柜取料完成
         * =3  机器人6#检测柜放料中
         * =4  机器人6#检测柜放料完成
         * byte[8]判断机器人是否空闲
         * =1  机器人运行任务中
         * =2  机器人空闲中
         * byte[9]备用
         */
    }
}
