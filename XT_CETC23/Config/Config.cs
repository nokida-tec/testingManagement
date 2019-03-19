using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23.Config
{
    class Config
    {
        static public bool ENABLED_VISIONPRO = true;
        static public bool ENABLED_PLC = true;
        static public bool ENABLED_DEBUG = false;
        static public bool ENABLED_LOG_SQL = false;
        private bool mEnableU8;
        public bool enableU8
        {
            get { return mEnableU8; }
            set
            {
                mEnableU8 = value;
                Save();
            }
        }

        static Config mInstance = null;
        static Object mLock = new Object();

        public static Config getInstance ()
        {
            if (mInstance==null)
            {
                lock (mLock)
                {
                    if(mInstance == null)
                    {
                        mInstance = new Config();
                    }
                }               
            }
            return mInstance;
        }

        private Config ()
        {
            Load();
        }

        public bool Load()
        {
            try
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.Config where Name = 'EnableU8'");
                if (dt.Rows.Count == 1)
                {
                    this.mEnableU8 = Convert.ToBoolean(dt.Rows[0]["Value"]);
                }
                else
                {
                    this.mEnableU8 = true;
                    Save();
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                this.mEnableU8 = true;
                Save();
            }
            return true;
        }

        public bool Save()
        {
            string sql = string.Format("UPDATE [dbo].[Config] "
                   + "SET [Value] = '{0}' "
                  // + ",[Type] = {2} "
                   + " WHERE Name = 'EnableU8'",
                    this.mEnableU8);
            int count = DataBase.GetInstanse().DBUpdate(sql);
            if (count < 1)
            {
                sql = string.Format("INSERT INTO [dbo].[Config] ([Name],[Value])" +
                    " VALUES ('{0}', '{1}')",
                        "EnableU8",
                        this.mEnableU8
                    );
                count = DataBase.GetInstanse().DBInsert(sql);
            }
            return count > 0;
        }

    }
}
