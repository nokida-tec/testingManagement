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
            Console.WriteLine(" ***** " + e.Message);
            Console.WriteLine(" ***** " + e.StackTrace);
        }
    }
}
