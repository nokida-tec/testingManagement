using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.DataCom
{
    class MTR 
    {
        static MTR mtr;
        public static int globalBasicID;
        DataBase db;
        DataTable dt_Mtr;
        private Object lockObject = new Object();

        public static MTR GetIntanse()
        {
            if(mtr==null)
            {
                mtr = new MTR();
            }
            return mtr;
        }
        MTR()
        {
            db = DataBase.GetInstanse();
        }       

        public int InsertBasicID(string ProductID,int FrameLocation,int SalverLocation,string ProductType,string CurrentStation, bool StationSign,string ProductChectResult,String BatchID, int CabinetID)
        {
            lock (lockObject)
            {
                DataTable dt = db.DBQuery("select * from dbo.MTR where CabinetID = " + CabinetID);
                if (dt!= null && dt.Rows.Count > 0)
                {
                    Logger.WriteLineWithStack("测试柜" + CabinetID + "任务已经存在");
                    return -1;
                }
                int lBasicID = GetID.getID();
                dt_Mtr = db.DBQuery("select * from dbo.MTR");
                for (int i = 0; i < dt_Mtr.Rows.Count; i++)
                {
                    if ((int)dt_Mtr.Rows[i]["BasicID"] == lBasicID)
                    { 
                        lBasicID = GetID.getID();
                    }
                }

                string tmpText = "insert into dbo.MTR(ProductID,FrameLocation,SalverLocation,ProductType,CurrentStation,StationSign,ProductCheckResult,BasicID,BeginTime,CabinetID)values('"
                    + ProductID + "',"
                    + FrameLocation + ","
                    + SalverLocation + ",'"
                    + ProductType + "','"
                    + CurrentStation + "','"
                    + StationSign + "','"
                    + ProductChectResult + "',"
                    + lBasicID + ",'"
                    + DateTime.Now + "',"
                    + CabinetID + ")";
                db.DBInsert("insert into dbo.MTR(ProductID,FrameLocation,SalverLocation,ProductType,CurrentStation,StationSign,ProductCheckResult,BasicID,BeginTime,CabinetID)values('"
                    + ProductID + "',"
                    + FrameLocation + ","
                    + SalverLocation + ",'"
                    + ProductType + "','"
                    + CurrentStation + "','"
                    + StationSign + "','"
                    + ProductChectResult + "',"
                    + lBasicID + ",'"
                    + DateTime.Now + "','"
                    + BatchID + "',"
                    + CabinetID + ")");
                return lBasicID;
            }
        }

        void updateBasicID(string ProductID, int FrameLocation, int SalverLocation, string ProductType, string ProductCurrentPos, string ProductSign, string ProductChectResult)
        {

        }

    }
    class GetID
    {
        static Object mLock = new Object();
        static int BasicID = 2000;
        public static int getID()
        {
            lock (mLock)
            {
                int ID;
                int ID1 = 0;
                int ID2 = 0;
                try {
                    DataTable dt = DataBase.GetInstanse().DBQuery("select max(BasicID) from dbo.MTR");
                    ID1 = Convert.ToInt32(dt.Rows[0][0]);
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    ID1 = 0;
                }

                try {
                    DataTable dt = DataBase.GetInstanse().DBQuery("select max(BasicID) from dbo.FrameData");
                    ID2 = Convert.ToInt32(dt.Rows[0][0]);
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    ID2 = 0;
                }

                ID = Math.Max(ID1, ID2);
                ID = Math.Max(ID, 1000);

                return ID + 1;
            }
        }
    }
}
