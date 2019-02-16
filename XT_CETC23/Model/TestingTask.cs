using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.DAL;

namespace XT_CETC23
{
    class TestingTask
    {
        static public int finish(int ID, String checkResult, String failedReason = null)
        {
            DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID = " + ID);
            if (dt == null || dt.Rows.Count == 0)
            {
                return -1;
            }

            String prodCode = failedReason != null ? failedReason : dt.Rows[0]["ProductID"].ToString();
            String prodType = dt.Rows[0]["ProductType"].ToString();
            String cabinetName = dt.Rows[0]["CabinetID"].ToString();
            String trayNo = dt.Rows[0]["FrameLocation"].ToString();
            String pieceNo = dt.Rows[0]["SalverLocation"].ToString();

            DataBase.GetInstanse().DBInsert("insert into dbo.ActualData("
                + " ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,CheckResult"
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
                + checkResult + "')");
            DataBase.GetInstanse().DBInsert("insert into dbo.FrameData("
                + "BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,CheckResult" 
                + " )values(" 
                + ID + ",'" 
                + prodCode + "','" 
                + prodType + "'," 
                + trayNo + "," 
                + pieceNo + ",'" 
                + cabinetName + "','" 
                + checkResult + "')");
            DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + ID);
            return ID;
        }
    }
}
