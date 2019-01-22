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
        delegate void DBMessage(string message);
        DBMessage dbMessage;
        DataTable dt ;
        SqlDataAdapter dread;
        public static string logPath;
        public static string cmdPath;
        public static string sourcePath;
        public static string targetPath;
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
            catch (SqlException sex)
            {
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
            catch (Exception ex)
            {
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
            catch (SqlException sex)
            {
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public DataTable DBQuery(string questr)
        {
            lock (lockQuery)
            {
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
                catch (SqlException sex)
                {
                    //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                    return null;
                }
            }
        }
        public bool DBUpdate(string questr)
        {
            try
            {
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
            catch (SqlException sex)
            {
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public bool DBDelete(string questr)
        {
            try
            {
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
            catch (SqlException sex)
            {
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
        public bool DBInsert(string questr)
        {
            try
            {
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
            catch (SqlException sex)
            {
                //dbMessage(sex.Message.ToString() + " " + DateTime.Now.ToString("G"));
                return false;
            }
        }
    }
}
