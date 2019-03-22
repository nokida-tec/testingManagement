using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23;
using System.Data;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.Data;

namespace XT_CETC23.Model
{
    class Batch
    {
        private String mID;
        public String ID 
        {
            get { return mID; }
        }
        private DateTime mBeginTime;
        private DateTime mEndTime;
        private int mNumOK;
        private int mNumNG;
        private int mNumExcept;

        private Object mLockDB = new Object();


        public bool Begin () 
        {
            mBeginTime = DateTime.Now;
            mEndTime = mBeginTime;
            mID = mBeginTime.ToString("yyyy-MM-dd HH:mm");
            mNumOK = 0;
            mNumNG = 0;
            mNumExcept = 0;
            Save();
            return true;
        }

        public bool End()
        {
            mEndTime = DateTime.Now;
            return Save();
        }

        public bool LoadUnfinished ()
        {
            lock (mLockDB)
            {
                String batchID = Batch.Last();
                if (batchID == null || batchID.Length == 0)
                {
                    return false;
                }
                DataTable dt = DataBase.GetInstanse().DBQuery(
                       " select * from dbo.Batch where ID = '" + batchID + "'");
                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                DateTime beginTime = Convert.ToDateTime(dt.Rows[0]["BeginTime"]);
                DateTime endTime = Convert.ToDateTime(dt.Rows[0]["EndTime"]);
                if (beginTime.Equals(endTime))
                {
                    mBeginTime = beginTime;
                    mEndTime = endTime;
                    mID = dt.Rows[0]["ID"].ToString();
                    mNumOK = Convert.ToInt32(dt.Rows[0]["Num_OK"]);
                    mNumNG = Convert.ToInt32(dt.Rows[0]["Num_NG"]);
                    mNumExcept = Convert.ToInt32(dt.Rows[0]["Num_Except"]);
                    return true;
                }
                return false;
            }
        }

        public static String Last()
        {
            //lock (mLockDB)
            {
                try
                {
                    DataTable dt = DataBase.GetInstanse().DBQuery(
                        "select max(ID) from dbo.Batch");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    String batchID = dt.Rows[0][0].ToString();
                    return batchID;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                return null;
            }
        }

        public static String LastFinished()
        {
            //lock (mLockDB)
            {
                try
                {
                    DataTable dt = DataBase.GetInstanse().DBQuery(
                        "select * from dbo.Batch ORDER BY ID DESC");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        return null;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["BeginTime"].ToString() != dt.Rows[i]["EndTime"].ToString())
                        {
                            String batchID = dt.Rows[i]["ID"].ToString();
                            return batchID;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                return null;
            }
        }

        private bool Save ()
        {
            lock (mLockDB)
            {
                try
                {
                    if (mID == null || mID.Length == 0)
                    {
                        return false;
                    }
                    DataTable dt = DataBase.GetInstanse().DBQuery("select * from [dbo].[Batch] where [ID] = '" + mID + "'");
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        DataBase.GetInstanse().DBInsert(
                                "INSERT INTO [dbo].[Batch]"
                                + "  ([ID]"
                                + "  ,[BeginTime]"
                                + "  ,[EndTime]"
                                + "  ,[Num_NG]"
                                + "  ,[Num_OK]"
                                + "  ,[Num_Except])"
                                + " VALUES "
                                + "    ('" + mID + "'"
                                + "    ,'" + mBeginTime + "'"
                                + "    ,'" + mEndTime + "'"
                                + "    ," + mNumNG
                                + "    ," + mNumOK
                                + "    ," + mNumExcept
                                + " )"
                                );
                    }
                    else
                    {
                        DataBase.GetInstanse().DBUpdate(
                                "UPDATE [dbo].[Batch] "
                                + " SET "
                                + "  [BeginTime] = '" + mBeginTime + "'"
                                + " ,[EndTime] = '" + mEndTime + "'"
                                + " ,[Num_NG] = " + mNumNG
                                + " ,[Num_OK] = " + mNumOK
                                + " ,[Num_Except] = " + mNumExcept
                                + " WHERE [ID] = '" + mID + "'"
                                );
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    return false;
                }
                return true;
            }
        }
    }
}
