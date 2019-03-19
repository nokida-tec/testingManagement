using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.DataManager
{
    class DBstr
    {

        static public string conn = "Server = localhost;Database = 23;User ID = zwl; Password =;Trusted_Connection = false;MultipleActiveResultSets=true;Integrated Security=true";//最后这个是采用windows登录的必要条件;Trusted_Connection = false;Integrated Security=,MultipleActiveResultSets=true允许多个readerAdapet同时访问
        static public string QueryStr(string tableName)
        {
            return "select * from " + tableName + "";
        }
        static public string InsertStr(string tableName, string columnName, string value)
        {
                return "insert into '" + tableName + "("+ columnName + ") values('" + value + "')"; 
        }
        static public string InsertStrUser(string tableName, string[] columnName, string[] value)
        {

            return "insert into " + tableName + "("+columnName[0]+ "," + columnName[1] + "," + columnName[2] + ") values('"+ value[0] + "','" + value[1] + "','" + value[2] + "')";         
        }
        static public string UpdateStr(string tableName,string column,string value,string condition)
        {
            return "update " + tableName + " set "+column+"='"+value+ "' where " + column + "='" + condition + "'";
        }
        static public string DeleteStr(string tableName, string column, string condition)
        {
            return "delete from " + tableName + " where " + column + "='" + condition + "'";
        }
    }
}
