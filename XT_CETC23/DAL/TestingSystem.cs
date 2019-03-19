﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using XT_CETC23.Instances;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.Windows.Forms;
using System.Data;
using XT_CETC23.DataManager;
using XT_CETC23.Model;

namespace XT_CETC23
{
    class TestingSystem
    {
        public enum Mode
        {
            [EnumDescription("未定义")]
            Unknown = 0,
            [EnumDescription("自动模式")]
            Auto = 1,
            [EnumDescription("手动模式")]
            Manual = 2,
        }

        public enum Initialize
        {
            [EnumDescription("未定义")]
            Unknown = 0,
            [EnumDescription("开始初始化")]
            Initialize = 2,
            [EnumDescription("初始化完成")]
            Initialized = 4,
        }

        public enum StepCommand
        {
            [EnumDescription("自动模式")]
            Auto = 1,
            [EnumDescription("单步模式")]
            Step = 2,
        }

        public enum StepStatus
        {
            [EnumDescription("未定义")]
            Unknown = 0,
            [EnumDescription("自动模式")]
            Auto = 1,
            [EnumDescription("单步模式")]
            Step = 2,
        }

        public enum Status
        {
            [EnumDescription("未定义")]
            Unknown = 0,
            [EnumDescription("运行中")]
            Running = 8,
            [EnumDescription("暂停")]
            Pausing = 16,
            [EnumDescription("报警中")]
            Alarming = 32,
            [EnumDescription("启动")]
            Start = 64,
            [EnumDescription("紧急急停")]
            Emergency = 128,
        }

        private static TestingSystem mInstance = null;
        private static Object lockInstance = new Object();
        private bool mSystemExisting = false;
        private bool mSystemRunning = true;
        private Thread mTaskSchedule = null;
        private Thread mTaskMonitor = null;

        private Mode mMode = Mode.Unknown;
        public Mode mode
        {
            get
            {
                return mMode;
            }
        }
        private Status mStatus = Status.Unknown;
        private Initialize mInitialize = Initialize.Unknown;
        private Byte mPlcMode = 0;

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
            grabTypeQuery();

            mTaskMonitor = new Thread(SystemMonitor);
            mTaskMonitor.Name = "模式监控";
            if (!mTaskMonitor.IsAlive)
            {
                mTaskMonitor.Start();
            }
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

            TestingCabinets.Abort();
            TestingTasks.Abort();
            
            mSystemRunning = false;
        }

        private void TaskSchedule ()
        {
            // 检查是否有未完成任务，且在组件在测试台中

            // 进入自动化任务
            while (true)
            {
                if (Frame.getInstance().hasTestedAll())
                {  // 全部测试完成
                    Frame.getInstance().excuteCommand(Frame.Lock.Command.Open);
                    //plc.DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 2 });
                    MessageBox.Show("料架已取空，请更换料架");

                    //plc.DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 0 });
                    Frame.getInstance().excuteCommand(Frame.Lock.Command.Close); 
                    Frame.getInstance().doScan();
                }

                for (int i = 0; i < TestingCabinets.getCount(); i ++ )
                {
                    if (TestingCabinets.getInstance(i).Enable == Model.TestingCabinet.ENABLE.Enable
                        && TestingTasks.getInstance(i).isRunning != true)
                    {
                        String[] caps = TestingCabinets.getInstance(i).getCap();
                        // 一个测试台先支持一种产品
                        if (Frame.getInstance().hasUntestedProduct(caps[0]) > 0)
                        {   // 是否有未测试产品
                            TestingTasks.getInstance(i).start(caps[0]);
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        public bool isSystemExisting()
        {
            return mSystemExisting;
        }

        public void SetExitSystem ()
        {
            mSystemExisting = false;
        }

        private void SystemMonitor()
        {
            Robot.GetInstanse();

            Logger.WriteLine("监控进程启动");            

            while (true)
            {
                if (Plc.GetInstanse().plcConnected)
                {
                    Mode mode = ((PlcData._plcMode & PlcData._readPlcMode) != 0) ? Mode.Auto : Mode.Manual;
                    onModeChanged(mode);

                    Initialize statusInitialize = Initialize.Unknown;
                    if ((PlcData._plcMode & PlcData._readPlcInit) != 0)
                    {
                        statusInitialize = Initialize.Initialize;
                    }
                    if ((PlcData._plcMode & PlcData._readPlcInited) != 0)
                    {
                        statusInitialize = Initialize.Initialized;
                    }
                    onInitializeChanged(statusInitialize);


                    Status status = Status.Unknown;
                    if ((PlcData._plcMode & PlcData._readPlcStart) != 0)
                    {
                        status = Status.Start;
                    }
                    if ((PlcData._plcMode & PlcData._readPlcAutoRunning) != 0)
                    {
                        status = Status.Running;
                    }
                    if ((PlcData._plcMode & PlcData._readPlcPausing) != 0)
                    {
                        status = Status.Pausing;
                    }
                    if ((PlcData._plcMode & PlcData._readPlcAlarming) != 0)
                    {
                        status = Status.Alarming;
                    }
                    if ((PlcData._plcMode & PlcData._readPlcEmergency) != 0)
                    {
                        status = Status.Emergency;
                    }

                    if (mPlcMode != PlcData._plcMode)
                    {
                        Logger.WriteLine("PLC Mode 改变：" + mPlcMode + " ===> " + PlcData._plcMode);
                        mPlcMode = PlcData._plcMode;
                    } 
                    
                    onStatusChanged(status);
                }
            }
            Thread.Sleep(50);   
        }

        void onModeChanged (Mode newMode)
        {
            if (newMode == mMode)
            {
                return;
            }
            Logger.WriteLine("Mode 改变：" + mMode + " ===> " + newMode);

            if (newMode == Mode.Manual)
            {
                Logger.WriteLine("主调度进程因手动切换退出");
                TestingSystem.GetInstance().ExitSystem();
            }
            mMode = newMode;
        }

        void onInitializeChanged (Initialize initialize)
        {
            if (mInitialize == initialize)
            {
                return;
            }
            Logger.WriteLine("Status 改变：" + mInitialize + " ===> " + initialize);

            if (initialize == Initialize.Initialize)
            {
                Logger.WriteLine("系统初始化中");
                clearTask();
                SetProdType2Plc();
                getInitResult();
            }
            else if (initialize == Initialize.Initialized)
            {
                Thread.Sleep(1000);
                do
                {
                    Thread.Sleep(100);
                } while (!Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 1 }));
                Thread.Sleep(1000);
                Logger.WriteLine("初始化成功");
            }
            mInitialize = initialize;
        }

        void onStatusChanged (Status newStatus)
        {
            if (mStatus == newStatus)
            {
                return;
            }
            try
            {
                Logger.WriteLine("Status 改变：" + mStatus + " ===> " + newStatus);

                if (mStatus == Status.Emergency)
                {
                    Logger.WriteLine("主调度进程紧急急停退出");
                    TestingSystem.GetInstance().ExitSystem();
                    return;
                }

                if (mStatus == Status.Pausing)
                {
                    Logger.WriteLine("系统暂停");
                    return;
                }

                if (mStatus == Status.Start && mMode == Mode.Auto && mInitialize == Initialize.Initialized)
                {
                    Logger.WriteLine("主调度进程启动");
                    TestingSystem.GetInstance().StartSystem();
                }

                if (mStatus == Status.Running)
                {
                    TestingSystem.GetInstance().Resume();
                }
            }
            finally
            {
                mStatus = newStatus;
            }
        }

        public bool isReadyForStep()
        {
            return stepEnable == true && readyForStep == true;
        }

        private void Resume()
        {
            Plc.GetInstanse().Resume();
            if (mTaskSchedule != null && mTaskSchedule.ThreadState == ThreadState.Suspended)
            {
                mTaskSchedule.Resume();
                Logger.WriteLine("主调度进程恢复");
            }
            TestingCabinets.Resume();
        }

        private void Pause()
        {
            Plc.GetInstanse().Suspend();
            if (mTaskSchedule != null && mTaskSchedule.ThreadState == ThreadState.Suspended)
            {
                mTaskSchedule.Resume();
                Logger.WriteLine("主调度进程恢复");
            }
            TestingCabinets.Resume();
        }

        private void SetProdType2Plc()
        {
            byte[] prodType = new byte[1];
            int cabinetStatus = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; ++i)
            {
                prodType[0] = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(i).Type].PlcMode;

                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 21 + i, 1, prodType);
                bool tmpBool = TestingCabinets.getInstance(i).Enable == TestingCabinet.ENABLE.Enable;
                if (tmpBool)
                {
                    switch (i)
                    {
                        case 0:
                            cabinetStatus = cabinetStatus + 1;
                            break;
                        case 1:
                            cabinetStatus = cabinetStatus + 2;
                            break;
                        case 2:
                            cabinetStatus = cabinetStatus + 4;
                            break;
                        case 3:
                            cabinetStatus = cabinetStatus + 8;
                            break;
                        case 4:
                            cabinetStatus = cabinetStatus + 16;
                            break;
                        case 5:
                            cabinetStatus = cabinetStatus + 32;
                            break;
                    }
                }
            }
            prodType[0] = Convert.ToByte(cabinetStatus);
            Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 20, 1, prodType);
        }

        public void clearTask()
        {
            try
            {
                DataBase.GetInstanse().DBDelete("delete from dbo.TaskAxlis2");

                for (int i = 0; i < DeviceCount.TestingCabinetCount; ++i)
                {
                    TestingCabinets.getInstance(i).Order = TestingCabinet.ORDER.Undefined;
                }
         
                PlcData.clearTask = true;
                Logger.WriteLine("任务清除完成");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                PlcData.clearTask = false;
                Logger.WriteLine("任务清除失败");
            }
        }

        public bool getInitResult()
        {
            int i = 0;
            //相机初始化留位 

            if (DataBase.GetInstanse().DBInit())
            {   //TransMessage("数据库初始化成功");
                ++i;
            }
            else
            {
                //TransMessage("数据库初始化失败");
            }
            Thread.Sleep(200);
            if(Robot.GetInstanse().Open())
            {
                //TransMessage("Robot初始化成功");
                ++i;
            }
            else
            {
                //TransMessage("Robot初始化失败");
            }
            
            //if (!readCabinetTh.IsAlive)
            //{
            //    readCabinetTh.Start();
            //}

            for (int j = 13; j < 19;j++ )
            {
                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, j, 1, new Byte[] { 0 });
            }
            Plc.GetInstanse().DBWrite(100, 2, 1, new Byte[] { 0 });
            Plc.GetInstanse().DBWrite(100, 3, 1, new Byte[] { 0 });
            //taskCycle = TaskCycle.GetInstanse();

            return i == 2;
        }

        private int mPreAlarmNo;
        private void PrintAlarm(int alarmId)
        {
            if (PlcData._alarmNumber != mPreAlarmNo)
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.Alarm where AlarmID =" + alarmId);
                if (dt.Rows.Count > 0)
                {
                    string message = dt.Rows[0]["AlarmDescription"].ToString().Trim();
                    Logger.getInstance().writeLine(true, "Alarm: " + message);
                    MessageBox.Show(message);
                }

                mPreAlarmNo = PlcData._alarmNumber;
            }
        }

        void grabTypeQuery()
        {
            MaterielData.grabType = DataBase.GetInstanse().DBQuery("select * from sortdata");
        }



        public static bool readyForStep { get; set; }

        public static bool stepEnable { get; set; }
    }
}
