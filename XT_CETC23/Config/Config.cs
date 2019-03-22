using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.IO;

namespace XT_CETC23.Config
{
    class Config
    {
        static public bool ENABLED_VISIONPRO = true;
        static public bool ENABLED_PLC = true;
        static public bool ENABLED_DEBUG = false;
        static public bool ENABLED_LOG_SQL = false;
        static public bool ENABLED_CONTROL = false;  // 是否控制设施,否则只侦听设备状态

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

        private string mLogPath;
        public string logPath
        {
            get
            {
                return mLogPath;
            }
            set
            {
                mLogPath = value;

                while (true)
                {
                    try
                    {
                        if (!Directory.Exists(mLogPath))
                        {
                            Directory.CreateDirectory(mLogPath);
                        }
                        break; // 成功创建目录，退出
                    }
                    catch (Exception)
                    {
                        char disk = mLogPath.Substring(0,1).ToCharArray()[0];
                        if (disk == 'c' || disk == 'C')
                        {
                            Logger.WriteLine("不能生成log目录!!!请检查系统及配置");
                            break;
                        }
                        disk = (char)(disk - 1);
                        mLogPath = disk + mLogPath.Substring(1);
                    }
                } 
            }
        }

        private string mTargetPath;
        public string targetPath
        {
            get
            {
                return mTargetPath;
            }
            set
            {
                mTargetPath = value;

                while (true)
                {
                    try
                    {
                        if (!Directory.Exists(mTargetPath))
                        {
                            Directory.CreateDirectory(mTargetPath);
                        }
                        break; // 成功创建目录，退出
                    }
                    catch (Exception)
                    {
                        char disk = mTargetPath.Substring(0, 1).ToCharArray()[0];
                        if (disk == 'c' || disk == 'C')
                        {
                            Logger.WriteLine("不能生成target目录!!!请检查系统及配置");
                            break;
                        }
                        disk = (char)(disk - 1);
                        mTargetPath = disk + mTargetPath.Substring(1);
                    }
                } 
            }
        }

        private string mCmdPath;
        public string cmdPath
        {
            get
            {
                return mCmdPath;
            }
            set
            {
                mCmdPath = value;

                while (true)
                {
                    try
                    {
                        if (!Directory.Exists(mCmdPath))
                        {
                            Directory.CreateDirectory(mCmdPath);
                        }
                        break; // 成功创建目录，退出
                    }
                    catch (Exception)
                    {
                        char disk = mCmdPath.Substring(0, 1).ToCharArray()[0];
                        if (disk == 'c' || disk == 'C')
                        {
                            Logger.WriteLine("不能生成target目录!!!请检查系统及配置");
                            break;
                        }
                        disk = (char)(disk - 1);
                        mCmdPath = disk + mCmdPath.Substring(1);
                    }
                }
            }
        }
    }
}
