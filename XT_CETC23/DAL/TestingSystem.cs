using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using XT_CETC23.Instances;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class TestingSystem
    {
        public enum Mode
        {
            [EnumDescription("自动模式")]
            Auto = 1,
            [EnumDescription("手动模式")]
            Manual = 2,
        }

        public enum Status
        {
            Normal = 1,//控件默认时
            MouseOver = 2,//鼠标移上控件时
            MouseDown = 3,//鼠标按下控件时
            Disable = 4 //当控件不可用时
        }

        private static TestingSystem mInstance = null;
        private static Object lockInstance = new Object();
        private bool mSystemExisting = false;
        private bool mSystemRunning = true;
        private Thread mTaskSchedule = null;

        public static TestingSystem GetInstance()
        {
            if (mInstance == null)
            {
                lock (lockInstance)
                {
                    if (mInstance == null)
                    {
                        mInstance = new TestingSystem();
                    }
                    return mInstance;
                }
            }
            return mInstance;
        }

         private TestingSystem()
        {
        }

         ~TestingSystem()
        {
        }

        public void StartSystem ()
        {
            Logger.WriteLine("主调度线程启动!!!");
            lock (lockInstance)
            {
                if (mSystemRunning == true)
                {   // 
                    if (mSystemExisting == true)
                    {
                        Thread.Sleep(100);
                        mTaskSchedule = null;
                    }
                    else 
                    {   // 异常，系统重入两次
                        throw new AlarmException("系统重入两次,报警!!!");
                    }
                }
                mSystemExisting = false;
                mSystemRunning = true;

                mTaskSchedule = new Thread(TaskSchedule);
                mTaskSchedule.Name = "主调度流程";
                mTaskSchedule.Start();
                // TransMessage("主调度进程启动");
            }
        }

        public void ExitSystem ()
        {
            Logger.WriteLine("主调度线程退出!!!");
            mSystemExisting = true;
            mTaskSchedule.Abort();
            TestingCabinets.abort();
            
            mSystemRunning = false;
        }

        private void TaskSchedule ()
        {

        }

        public bool isSystemExisting()
        {
            return mSystemExisting;
        }

        public void SetExitSystem ()
        {
            mSystemExisting = false;
        }
    }
}
