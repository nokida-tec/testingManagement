using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.DAL;

namespace XT_CETC23
{
    class TestingTask
    {
        private int mID;        // 任务号
        private int mCabinetID; // 分配的测试柜
        private Thread mThread;


        static public int finish(int ID, String checkResult, String failedReason = null)
        {
            DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID = " + ID);
            if (dt == null || dt.Rows.Count == 0)
            {
                return -1;
            }

            try 
            {
                String prodCode = failedReason != null ? failedReason : dt.Rows[0]["ProductID"].ToString();
                String prodType = dt.Rows[0]["ProductType"].ToString();
                String cabinetName = dt.Rows[0]["CabinetID"].ToString();
                String trayNo = dt.Rows[0]["FrameLocation"].ToString();
                String pieceNo = dt.Rows[0]["SalverLocation"].ToString();
                String BeginTime = dt.Rows[0]["BeginTime"].ToString();
                String EndTime = dt.Rows[0]["EndTime"].ToString();

                DataBase.GetInstanse().DBInsert("insert into dbo.ActualData("
                    + " ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,BeginTime,EndTime,CheckResult"
                    + " )values( '"
                    + prodCode + "','"
                    + prodType + "',"
                    + trayNo + ","
                    + pieceNo + ",'"
                    + cabinetName + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + BeginTime + "','"
                    + EndTime + "','"
                    + checkResult + "')");
                try
                {
                    DataBase.GetInstanse().DBInsert("insert into dbo.FrameData("
                        + "BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,BeginTime,EndTime,CheckResult"
                        + " )values("
                        + ID + ",'"
                        + prodCode + "','"
                        + prodType + "',"
                        + trayNo + ","
                        + pieceNo + ",'"
                        + cabinetName + "','"
                        + BeginTime + "','"
                        + EndTime + "','"
                        + checkResult + "')");
                }
                catch (Exception e1)
                {
                    Logger.WriteLine(e1);
                }
                DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + ID);
            }
            catch (Exception e) 
            {
                Logger.WriteLine(e);
            }
            return ID;
        }
    }
}
