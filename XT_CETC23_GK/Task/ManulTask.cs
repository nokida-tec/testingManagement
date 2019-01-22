using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23_GK.Task
{
    public class ManulTask
    {
        static ManulTask mTask;
        Axlis2Task pTask=new Axlis2Task();
        RobotTask rTask = new RobotTask();
        GroupTask gTask = GroupTask.GetInstance();
        Queue<string> gQueue = new Queue<string>();
        //Queue<GroupTask> gQueueTask = new Queue<GroupTask>();
        public static ManulTask GetInstanse()
        {
            if(mTask==null)
            {
                mTask = new ManulTask();
            }
            return mTask;
        }
        public Queue<string> EnterQueue(string Axlis7Pos,string OrderType,string ProductSort,string ProductNum)
        {
            pTask.Axlis7Pos = Axlis7Pos;
            gQueue.Enqueue(pTask.Axlis7Pos);
            //rTask.OrderType = Convert.ToInt16(OrderType);
            rTask.OrderType = OrderType;
            gQueue.Enqueue(rTask.OrderType.ToString());
            rTask.ProductType = ProductSort;
            gQueue.Enqueue(rTask.ProductType);
            rTask.Position = Convert.ToInt16(ProductNum);
            gQueue.Enqueue(rTask.Position.ToString());
            //gTask.pTask = pTask;
            //gTask.rTask = rTask;
            return gQueue;
        }
        public Queue<string> EnterQueue(string Axlis7Pos, string OrderType,string ProductSort)
        {
            pTask.Axlis7Pos = Axlis7Pos;
            gQueue.Enqueue(pTask.Axlis7Pos);
            //rTask.OrderType = Convert.ToInt16(OrderType);
            rTask.OrderType = OrderType;
            rTask.ProductType = ProductSort;
            gQueue.Enqueue(rTask.ProductType);
            gQueue.Enqueue(rTask.OrderType.ToString());
            //gQueue.Enqueue(rTask.OrderType);
            //gTask.pTask = pTask;
            //gTask.rTask = rTask;
            return gQueue;
        }
    }
}
