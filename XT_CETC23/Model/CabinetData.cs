using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.DataCom;

namespace XT_CETC23.DataManager
{
    class CabinetData
    {
        public static string pathRead1;
        public static string pathRead2;
        public static string pathRead3;
        public static string pathRead4;
        public static string pathRead5;
        public static string pathRead6;
        public static string[] pathCabinetStatus; //= new string[6] { pathRead1, pathRead2, pathRead3, pathRead4, pathRead5, pathRead6 };

        public static string pathWrite1;
        public static string pathWrite2;
        public static string pathWrite3;
        public static string pathWrite4;
        public static string pathWrite5;
        public static string pathWrite6;
        public static string[] pathCabinetOrder;// = new string[6] { pathWrite1, pathWrite2, pathWrite3, pathWrite4, pathWrite5, pathWrite6 };
        public static string[] cabinetStatus = new string[6];
        public static string getOrder(int order)
        {
            return DateTime.Now.ToString("G" + " "+order.ToString());
        }
        //默认6种组建，如果后续要添加或变更，须变更数据库
        public static int getType(string type)
        {                       
            if (type.Trim() == MaterielData.grabType.Rows[2]["sortname"].ToString().Trim())
                return 11;
            if (type.Trim() == MaterielData.grabType.Rows[3]["sortname"].ToString().Trim())
                return 21;
            if (type.Trim() == MaterielData.grabType.Rows[0]["sortname"].ToString().Trim())
                return 31;
            if (type.Trim() == MaterielData.grabType.Rows[1]["sortname"].ToString().Trim())
                return 41;
            //if (type == MaterielData.grabType.Rows[4]["sortname"].ToString())
            //    return 51;
            //if (type == MaterielData.grabType.Rows[5]["sortname"].ToString())
            //    return 61;
            //预留扩展种类
            //if (type == MaterielData.grabType.Rows[6]["sortname"].ToString())
            //    return 71;
            //if (type == MaterielData.grabType.Rows[7]["sortname"].ToString())
            //    return 81;
            else
                return 0;
        }
        public static int getStop()
        {
            return 02;//停止测试
        }
        /* 接收指令.txt(测试柜文件)
         * xx-xx-xx xx:xx:xx 指令字
         * =11 A组件启动测试
         * =21 B组件启动测试
         * =31 2类组件启动测试 
         * =41 AB类组件启动测试
         * =51 备用组件启动测试 
         * =61 备用组件启动测试 
         * =02 停止测试 
         * 发送指令.txt(测试柜文件)
         * =30  准备就绪
         * =31  正在测试中
         * =32  配置信息错误
         * =33  仪器控制错误
         * =34  报告生成报错
         * =40  测试完成或中止       
         */
    }
}
