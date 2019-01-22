using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23_GK.Task
{
   public class RobotTask
    {
        public int Axlis7Pos;
        public string OrderType;
        public string ProductType;
        public int Position;
        static RobotTask robotTask;
        public static RobotTask GetInstanse()
        {
            if(robotTask==null)
            {
                robotTask = new RobotTask();
            }
            return robotTask;
        }
    }
}
