using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.Data;

namespace XT_CETC23.Model
{
    class TestingCabinet
    {   // 测试柜
        public enum STATUS
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

        public enum ENABLE
        {
            [EnumDescription("Enable")]
            Enable = 1,
            [EnumDescription("Disable")]
            Disable = 0,
        }

        public enum ORDER  // 指令
        {
            Undefined = 0,
            Start = 1,
            Stop = 2,
            Reset = 3,
            Pause = 4,
            Resume = 5,
        };

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { 
                name = value;
                Save();
            }
        }

        private ENABLE enable;
        public ENABLE Enable
        {
            get { return enable; }
            set {
                enable = value;
                Save();
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set {
                type = value;
                Save();
            }
        }

        private STATUS status;
        public STATUS Status
        {
            get { return status; }
            set { 
                status = value;
                Save();
            }
        }

        private ORDER order;
        public ORDER Order
        {
            get { return order; }
            set {
                order = value;
                Save();
            }
        }

        private int taskID;
        public int TaskID
        {
            get { return taskID; }
            set { 
                taskID = value;
                Save();
            }
        }

        private string productType;
        public string ProductType
        {
            get { return productType; }
            set { 
                productType = value;
                Save();
            }
        }

        public TestingCabinet(int ID)
        {
            this.id = ID;
            Load();
        }

        public bool Load()
        {
            try
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.TaskCabinet where CabinetID=" + ID);
                if (dt.Rows.Count == 1)
                {
                    this.name = Convert.ToString(dt.Rows[0]["EquipmentName"]);
                    this.type = Convert.ToString(dt.Rows[0]["Type"]);
                    this.enable = (ENABLE)Convert.ToInt32(dt.Rows[0]["Enable"]);
                    this.status = (STATUS)Convert.ToInt32(dt.Rows[0]["Status"]);
                    this.order = (ORDER)Convert.ToInt32(dt.Rows[0]["OrderType"]);
                    this.productType = Convert.ToString(dt.Rows[0]["ProductType"]);
                    this.taskID = Convert.ToInt32(dt.Rows[0]["BasicID"]);
                }
                else
                {
                    this.name = "#" + (ID + 1) + "号机台";
                    this.type = "";
                    this.enable = ENABLE.Disable;
                    this.status = STATUS.NG;
                    this.order = ORDER.Undefined;
                    this.productType = "";
                    this.taskID = 0;
                    
                    Save();
                }
            }
            catch (Exception e)
            {
                this.name = "#" + (ID + 1) + "号机台";
                this.order = ORDER.Undefined;
                this.taskID = 0;
                this.enable = ENABLE.Disable;
                this.status = STATUS.NG;
            }
            return true;
        }

        public bool Save()
        {
            string sql = string.Format("UPDATE [dbo].[TaskCabinet] "
                   + "SET [EquipmentName] = '{1}' "
                   + ",[Type] = '{2}' "
                   + ",[Enable] = {3:d} "
                   + ",[Status] = {4:d} "
                   + ",[OrderType] = {5:d} "
                   + ",[ProductType] = '{6}' "
                   + ",[BasicID] ={7:d} "
                   + " WHERE CabinetID = {0:d}",
                    this.ID,
                    this.Name,
                    this.Type,
                    this.Enable,
                    this.Status,
                    this.Order,
                    this.ProductType,
                    this.TaskID);
            bool ret = DataBase.GetInstanse().DBUpdate(sql);
            if (ret == false) 
            {
                sql = string.Format("INSERT INTO [dbo].[TaskCabinet] ([CabinetID],[EquipmentName],[Type],[Enable],[Status],[OrderType],[ProductType],[BasicID])" +
                    " VALUES ({0:d}, '{1}', '{2}', {3:d}, {4:d}, {5:d}, '{6}', {7:d})",
                        this.ID,
                        this.Name,
                        this.Type,
                        this.Enable,
                        this.Status,
                        this.Order,
                        this.ProductType,
                        this.TaskID
                    );
                ret = DataBase.GetInstanse().DBInsert(sql);
            }
            return ret;
        }

        public bool cmdStart(string productType, int taskId)
        {
            this.Order = ORDER.Start;
            this.TaskID = taskId;
            return Save();
        }

        public bool cmdStop()
        {
            this.Order = ORDER.Stop;
            return Save();
        }

        private Task task;


    }
}
