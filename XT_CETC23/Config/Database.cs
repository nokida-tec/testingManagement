using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.Config
{
    class Database
    {
        static public string db_scada_conn = "Server = localhost;Database = 23;User ID = zwl; Password =;Trusted_Connection = false;MultipleActiveResultSets=true;Integrated Security=true";//最后这个是采用windows登录的必要条件;Trusted_Connection = false;Integrated Security=,MultipleActiveResultSets=true允许多个readerAdapet同时访问
        static public string db_scada_conn_ymb = "Server = localhost;Database = 23;User ID = sa; Password = qwe123QWE;Trusted_Connection = false;MultipleActiveResultSets=true;Integrated Security=true";
        //最后这个是采用windows登录的必要条件;Trusted_Connection = false;Integrated Security=,MultipleActiveResultSets=true允许多个readerAdapet同时访问
        static public string db_u8_conn = "Server = 192.168.1.100;Database = UFDATA_998_2014;User ID = sa; Password = ~!Topunion;Trusted_Connection = false;MultipleActiveResultSets=true;Integrated Security=false";
        //最后这个是采用windows登录的必要条件;Trusted_Connection = false;Integrated Security=,MultipleActiveResultSets=true允许多个readerAdapet同时访问
    }
}
