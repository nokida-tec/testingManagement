using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
namespace XT_CETC23.DataCom
{
    public class DataBase
    {
        SqlConnection con;
        SqlCommand com;
        private static DataBase db = null;
        private static DataBase dbOfU8 = null;
        delegate void DBMessage(string message);
        DBMessage dbMessage;
        DataTable dt ;
        SqlDataAdapter dread;
        object lockConCLose = new object();
        object lockConOpen = new object();
        object lockQuery = new object();
        public static DataBase GetInstanse()
        {
            if (db == null)
            {
                db = new DataBase(DataManager.DBstr.conn);
            }
            return db;
        }

        public static DataBase GetU8DBInstanse()
        {
            if (dbOfU8 == null)
            {
                dbOfU8 = new DataBase(Config.Database.db_u8_conn);
            }
            return dbOfU8;
        }

        private DataBase(string sqlcon)
        {
            //con.ConnectionString = sqlcon;
            con = new SqlConnection(sqlcon);
        }
        ~DataBase()
        {
            //con.Close();
        }
        void conClose()
        {
            lock (lockConCLose)
            {
                if (con.State == ConnectionState.Open)
                {
                    //con.Close();
                }
            }
        }
        void conOpen()
        {
            lock (lockConCLose)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
        }
        public bool DBInit()
        {
            try
            {
                conOpen();
                conClose();
                return true;
            }
            catch (SqlException e)
            {
                Logger.WriteLine(e);
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public bool DBConnect()
        {
            try
            {
                conOpen();
                return true;
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                return false;
            }
        }
        public bool DisDBConnect()
        {
            try
            {
                conClose();
                return true;
            }
            catch (SqlException e)
            {
                Logger.WriteLine(e);
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public DataTable DBQuery(string questr)
        {
            lock (lockQuery)
            {
                if (Config.Config.ENABLED_LOG_SQL)
                {
                    Logger.WriteLine("SQL: " + questr);
                }
                try
                {
                    GC.Collect();
                    dt = new DataTable();
                    dt.Columns.Clear();
                    dt.Rows.Clear();
                    conOpen();
                    using (dread = new SqlDataAdapter(questr, con))
                    {
                        dread.Fill(dt);
                        conClose();
                        dread.Dispose();
                        return dt;
                    }
                }
                catch (SqlException e)
                {
                    Logger.WriteLine(questr);
                    Logger.WriteLine(e);
                    //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                    return null;
                }
            }
        }
        public int DBUpdate(string questr)
        {
            try
            {
                if (Config.Config.ENABLED_LOG_SQL)
                {
                    Logger.WriteLine("SQL: " + questr);
                }
                GC.Collect();
                conOpen();
                using (com = new SqlCommand(questr, con))
                {
                    int count = com.ExecuteNonQuery();
                    conClose();
                    com.Dispose();
                    return count;
                }
            }
            catch (SqlException e)
            {
                Logger.WriteLine(questr);
                Logger.WriteLine(e);
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return 0;
            }
        }
        public bool DBDelete(string questr)
        {
            try
            {
                if (Config.Config.ENABLED_LOG_SQL)
                {
                    Logger.WriteLine("SQL: " + questr);
                }
                GC.Collect();
                conOpen();
                using (com = new SqlCommand(questr, con))
                {
                    com.ExecuteNonQuery();
                    conClose();
                    com.Dispose();
                    return true;
                }
            }
            catch (SqlException e)
            {
                Logger.WriteLine(questr);
                Logger.WriteLine(e);
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public int DBInsert(string questr)
        {
            try
            {
                if (Config.Config.ENABLED_LOG_SQL)
                {
                    Logger.WriteLine("SQL: " + questr);
                }
                GC.Collect();
                conOpen();
                using (com = new SqlCommand(questr, con))
                {
                    int count = com.ExecuteNonQuery();
                    conClose();
                    com.Dispose();
                    return count;
                }                
            }
            catch (SqlException e)
            {
                Logger.WriteLine(questr);
                Logger.WriteLine(e);
                return 0;
            }
        }
    }
}
