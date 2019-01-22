using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
namespace XT_CETC23.DataManager
{
   static class PlcData
    {
        //读取plc状态
        public static int PlcStatusAddress = 100;
        //向plc发送命令的地址
        public static int PlcWriteAddress = 101;
        //读取条码
        public static int PlcDataAddress = 102;
        public static byte[] PlcStatusValue;
        public static byte[] PlcDataValue;
        public static bool clearTask=false;
        static Dictionary<string, int> Axlis7Pos = new Dictionary<string, int>();

        #region PLC写入地址
        //吸入数据得长度
        public static int _writeLength1 = 1;//写入数据的长度(字节)
        public static int _writeLength2 = 2;//写入数据的长度(字节)
        public static int _writeLength4 = 4;//写入数据的长度(字节)
        //写入机器人数据
        public static int _writeRobot = 0;//第一个字节起始地址，长度为1个字节
        public static byte _writeRobotValue;
        //写入PLC模式数据
        public static int _writePlcMode = 1;//写入PLC模式的第一个字节起始地址，长度为1个字节
        public static byte _writePlcModeValue;
        //写入7轴位置数据
        public static int _writeAxlis7Pos = 2;//写入7轴的位置命令,第一个字节起始地址，长度为1个字节
        public static byte _writeAxlis7PosValue;
        //写入2轴位置命令
        public static int _writeAxlis2Order = 3;//写入2轴的位置命令,第一个字节起始地址，长度为1个字节
        public static byte _writeAxlis2OrderValue;
        //写入2轴位置数据
        public static int _writeAxlis2Pos = 4;//写入2轴的位置值,第一个字节起始地址，长度为1个字节
        public static byte _writeAxlis2PosValue;
        //建立料架存储数组
        public static string[,] FramePos = new string[8, 5];//料架存储种类信息
        //声明点动写入的字节变量


        //建立点动命令1
        public static int _writeManulOrder1 = 5;
        public static int _writeAxlis7Home = 1;//点动7轴回原点
        public static int _writeAxlis7Forward = 2;//点动7轴前进
        public static int _writeAxlis7Backward = 4;//点动7轴后退
        public static int _writeFrameHome1 = 8;//料架横移回原点
        public static int _writeFrameForward = 16;//料架横移前进
        public static int _writeFrameReverse = 32;//料架横移后退
        public static int _writeFrameHome2 = 64;//料架升降回原点
        public static int _writeFrameUp = 128;//料架升降前进
        //建立点动命令2                                         
        public static int _writeManulOrder2 = 6;
        public static int _writeFrameDown = 1;//料架升降后退
        public static int _writeFrameGrab1 = 2;//料架固定气缸伸出
        public static int _writeFrameRealese1 = 4;//料架固定气缸缩回
        public static int _writeFrameGrab3 = 8;//料架固定气缸2伸出
        public static int _writeFrameRealese3 = 16;//料架固定气缸2缩回
        public static int _writeFrameGrab2 = 32;//料架取料气缸伸出
        public static int _writeFrameRealese2 = 64;//料架取料气缸缩回
        public static int _writeSpare3 = 128;//点动备用

        //建立点动命令3                                      
        public static int _writeManulOrder3 = 7;
        public static int _writeCabinet1CY1Extend = 1;//点动1#检测台1#气缸伸出
        public static int _writeCabinet1CY1Back = 2;//点动1#检测台1#气缸缩回
        public static int _writeCabinet1CY2Extend = 4;//点动1#检测台2#气缸伸出
        public static int _writeCabinet1CY2Back = 8;//点动1#检测台2#气缸缩回
        public static int _writeCabinet1CY3Extend = 16;//点动1#检测台3#气缸伸出
        public static int _writeCabinet1CY3Back = 32;//点动1#检测台3#气缸缩回
        public static int _writeCabinet1CY4Extend = 64;//点动1#检测台4#气缸伸出
        public static int _writeCabinet1CY4Back = 128;//点动1#检测台4#气缸缩回

        //建立点动命令4                                    
        public static int _writeManulOrder4 = 8;
        public static int _writeCabinet2CY1Extend = 1;//点动2#检测台1#气缸伸出
        public static int _writeCabinet2CY1Back = 2;//点动2#检测台1#气缸缩回
        public static int _writeCabinet2CY2Extend = 4;//点动2#检测台2#气缸伸出
        public static int _writeCabinet2CY2Back = 8;//点动2#检测台2#气缸缩回
        public static int _writeCabinet2CY3Extend = 16;//点动2#检测台3#气缸伸出
        public static int _writeCabinet2CY3Back = 32;//点动2#检测台3#气缸缩回
        public static int _writeCabinet2CY4Extend = 64;//点动3#检测台4#气缸伸出
        public static int _writeCabinet2CY4Back = 128;//点动3#检测台4#气缸缩回

        //建立点动命令5                    
        public static int _writeManulOrder5 = 9;
        public static int _writeCabinet3CY1Extend = 1;//点动3#检测台1#气缸伸出
        public static int _writeCabinet3CY1Back = 2;//点动3#检测台1#气缸缩回
        public static int _writeCabinet3CY2Extend = 4;//点动3#检测台2#气缸伸出
        public static int _writeCabinet3CY2Back = 8;//点动3#检测台2#气缸缩回    
        public static int _writeCabinet3CY3Extend = 16;//点动3#检测台3#气缸伸出
        public static int _writeCabinet3CY3Back = 32;//点动3#检测台3#气缸缩回
        public static int _writeCabinet3CY4Extend = 64;//点动3#检测台4#气缸伸出
        public static int _writeCabinet3CY4Back = 128;//点动3#检测台4#气缸缩回    

        //建立点动命令6                                   
        public static int _writeManulOrder6 = 10;
        public static int _writeCabinet4CY1Extend = 1;//点动4#检测台1#气缸伸出
        public static int _writeCabinet4CY1Back = 2;//点动4#检测台1#气缸缩回
        public static int _writeCabinet4CY2Extend = 4;//点动4#检测台2#气缸伸出
        public static int _writeCabinet4CY2Back = 8;//点动4#检测台2#气缸缩回
        public static int _writeCabinet4CY3Extend = 16;//点动4#检测台3#气缸伸出
        public static int _writeCabinet4CY3Back = 32;//点动4#检测台3#气缸缩回
        public static int _writeCabinet4CY4Extend = 64;//点动4#检测台4#气缸伸出
        public static int _writeCabinet4CY4Back = 128;//点动4#检测台4#气缸缩回

        //建立点动命令7                                 
        public static int _writeManulOrder7 = 11;
        public static int _writeCabinet5CY1Extend = 1;//点动5#检测台1#气缸伸出
        public static int _writeCabinet5CY1Back = 2;//点动5#检测台1#气缸缩回
        public static int _writeCabinet5CY2Extend = 4;//点动5#检测台2#气缸伸出
        public static int _writeCabinet5CY2Back = 8;//点动5#检测台2#气缸缩回
        public static int _writeCabinet5CY3Extend = 16;//点动5#检测台3#气缸伸出
        public static int _writeCabinet5CY3Back = 32;//点动5#检测台3#气缸缩回
        public static int _writeCabinet5CY4Extend = 64;//点动6#检测台4#气缸伸出
        public static int _writeCabinet5CY4Back = 128;//点动6#检测台4#气缸缩回

        //建立点动命令8                               
        public static int _writeManulOrder8 = 12;
        public static int _writeCabinet6CY1Extend = 1;//点动6#检测台1#气缸伸出
        public static int _writeCabinet6CY1Back = 2;//点动6#检测台1#气缸缩回
        public static int _writeCabinet6CY2Extend = 4;//点动6#检测台2#气缸伸出
        public static int _writeCabinet6CY2Back = 8;//点动6#检测台2#气缸缩回
        public static int _writeCabinet6CY3Extend = 16;//点动6#检测台3#气缸伸出
        public static int _writeCabinet6CY3Back = 32;//点动6#检测台3#气缸缩回
        public static int _writeCabinet6CY4Extend = 64;//点动6#检测台4#气缸伸出
        public static int _writeCabinet6CY4Back = 128;//点动6#检测台4#气缸缩回


        //建立点动命令9                               
        public static int _writeManulOrder9 = 13;
        public static int _writeSpare6 = 1;//点动备用
        public static int _writeSpare7 = 2;//点动备用
        public static int _writeFrameGrab = 4;//点动料架夹紧
        public static int _writeFrameRelease = 8;//点动料架释放
        public static int _writeSpare8 = 16;//点动备用
        public static int _writeSpare9 = 32;//点动备用
        public static int _writeSpare10 = 64;//点动备用
        public static int _writeSpare11 = 128;//点动备用
        #endregion
        #region PLC读取状态
        //读取机器状态
        public static int _readBeginAddress = 0;//读取第一个字节
        public static byte _robotStatus;                //byte0
        public static byte _plcMode;                    //byte1
        public static byte _axlis7Status;               //byte2
        public static byte _axlis2Status;               //byte3
        public static byte _axlis2Pos;                  //byte4
        public static byte _cabinetStatus_old;          //byte5
        public static byte _frameFeedBack;              //byte6
        public static byte _limitFeedBack1;             //byte7
        public static byte _limitFeedBack2;             //byte8
        public static byte _limitFeedBack3;             //byte9
        public static byte _limitFeedBack4;             //byte10
        public static byte _limitFeedBack5;             //byte11
        public static byte _limitFeedBack6;             //byte12
        public static byte[] _cabinetStatus = new byte[6];      //byte13~18
        public static byte _alarmNumber;                //byte20
       
        public static byte _alarmValue6;
        public static byte _alarmValue7;

        //_robotStatus
        public static byte _readRobotFault =1;
        public static byte _readRobotPowerOn = 2;
        public static byte _readRobotReady = 4;
        public static byte _readRobotRunning = 8;
        public static byte _readRobotPausing = 16;

        //_plcMode
        public static byte _readPlcMode = 1;
        public static byte _readPlcInit = 2;
        public static byte _readPlcInited = 4;
        public static byte _readPlcAutoRunning = 8;
        public static byte _readPlcPausing = 16;
        public static byte _readPlcAlarming = 32;
        public static byte _readPlcStart = 64;
        public static byte _readPlcEmergency = 128;

        //_axlis2Status;
        public static byte _readScanSuccess = 4;
        public static byte _readScanFailure = 128;

        #endregion


        public static void AddAxlis7Pos(string[] pos)
        {
            for(int i=0;i<pos.Length;i++)
            {
                Axlis7Pos.Add(pos[i], 100 + i);
            }
        }
        static public UInt32 rProductIDD(byte[] bytes)
        {
            string temp = bytes[0].ToString("x") + bytes[1].ToString("x") + bytes[2].ToString("x") + bytes[3].ToString("x");
            return Convert.ToUInt32(temp, 32);
        }
        static public UInt16 rProductIDW(byte[] bytes)
        {
            string temp = bytes[0].ToString("x") + bytes[1].ToString("x");
            return Convert.ToUInt16(temp, 16);
        }
        static public string rProductType(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }
        public static int getAxlis7Pos(string pos)
        {
            int position = Axlis7Pos[pos];
            return position;
        }
    }
}
