using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23
{
    class AbortException : Exception
    {
        public AbortException (String message)
            : base(message)
        {
        }
    }
}
