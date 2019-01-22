using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23_GK.Task
{
    public class GroupTask
    {
        public Axlis2Task pTask;
        public RobotTask rTask;
        static GroupTask gTask;
        public static GroupTask GetInstance()
        {
            if(gTask==null)
            {
                gTask = new GroupTask();
            }
            return gTask;
        }
    }
}
