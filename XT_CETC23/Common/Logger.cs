using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23
{
    class Logger
    {
        static public void printException(Exception e)
        {
            Console.WriteLine(" ***** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ") + e.Message);
            Console.WriteLine(" ***** " + e.StackTrace);
        }

        static public void WriteLine(String message)
        {
            Console.WriteLine(" ***** " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ")  + message);
        }
    }
}
