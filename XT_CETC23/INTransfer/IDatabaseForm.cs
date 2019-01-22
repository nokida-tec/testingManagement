using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace XT_CETC23.INTransfer
{
   public interface IDatabaseForm
        
    {
        DataTable getRunData();
        void getStatus(string str);
    }
}
