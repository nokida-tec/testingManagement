using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.Common;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class TestingTask : Task
    {
        private Thread mTask;
        private bool mIsRunning = false;
        public bool isRunning
        {
            get
            {
                return mIsRunning;
            }
        }

        public class Step
        {
            public enum Name
            { // 需要和PLC对应
                [EnumDescription("没有任务")]
                None = 100,
                [EnumDescription("料仓")]
                Frame = 101,
                [EnumDescription("料仓到测试台")]
                FrameToCabinet = 102,
                [EnumDescription("测试台")]
                CabinetTesting = 103,
                [EnumDescription("测试台到料仓")]
                CabinetToFrame = 104,
            }

            ReturnCode stepIn(Name step)
            {
                return ReturnCode.OK;
            }

            ReturnCode stepOut(Name step)
            {
                return ReturnCode.OK;
            }

            ReturnCode doStep(Name step)
            {
                return ReturnCode.OK;
            }
        }

        public TestingTask (int cabinetNo)
        {
            mCabinetID = cabinetNo;
        }

        public ReturnCode start(String productType)
        {
            Logger.WriteLine("  ***   start：" + this.mCabinetID);
            lock (this)
            {
                try
                {
                    if (mIsRunning)
                    {
                        Logger.WriteLine("  ***   测试线程: " + this.mCabinetID + "在运行中 线程:" + mTask.ManagedThreadId + " 状态：" + mTask.ThreadState);
                        return ReturnCode.SysBusy;
                    }

                    mIsRunning = true;

                    CreateTask(productType, TestingSystem.GetInstance().batch.ID);

                    if (mTask != null)
                    {
                        while (mTask.ThreadState != ThreadState.Stopped && mTask.ThreadState != ThreadState.Aborted)
                        {   // 等待原有线程运行退出
                            Logger.WriteLine("  ***   测试线程: Thread" + this.mCabinetID + "在运行中 线程:" + mTask.ManagedThreadId + " 状态：" + mTask.ThreadState);
                            Thread.Sleep(100);
                        }
                        Thread.Sleep(100);
                        mTask = null;
                    }
                    if (mTask == null)
                    {
                        mTask = new Thread(runOne);
                        mTask.Name = "测试线程：" + this.mCabinetID + " 启动";
                        Logger.WriteLine("  ***   测试线程:" + this.mCabinetID + " 新启动线程:" + mTask.ManagedThreadId + " 状态：" + mTask.ThreadState);
                        mTask.Start();
                    }
                    return ReturnCode.OK;
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                return ReturnCode.Exception;
            }
        }

        private static Object sLockFrameToCabinet = new Object();
        private static Object sLockCabinetToFrame = sLockFrameToCabinet;
        private void runOne()
        {   // 运行一次
            lock (lockObject)
            {
                try
                {
                    mIsRunning = true;

                    Frame.Location location = new Frame.Location();

                    lock (sLockFrameToCabinet)
                    {
                        // 料仓取料
                        ReturnCode ret = Frame.getInstance().doGetProduct(mProductType, ref location);
                        if (ret != ReturnCode.OK)
                        {
                            return;
                        }

                        // 机器人从料仓取料
                        Robot.GetInstanse().doGetProductFromFrame(mProductType, location.slot, location.CordinatorX, location.CordinatorY, location.CordinatorU);

                        // 机器人放料到测试台
                        Robot.GetInstanse().doPutProductToCabinet(mProductType, mCabinetID);
                    }
                    // 测试开始
                    TestingCabinets.getInstance(mCabinetID).doTest();

                    lock (sLockCabinetToFrame)
                    {
                        // 机器人从测试台取料
                        Robot.GetInstanse().doGetProductFromCabinet(mProductType, mCabinetID);

                        // 插入料架取料任务，取出托盘（要区分取出和放入）
                        Frame.getInstance().doGet(location.tray);

                        // 插入机器人回料任务
                        Robot.GetInstanse().doPutProductToFrame(mProductType, location.slot);

                        Frame.getInstance().doPut(location.tray);
                    }

                    // 根据结果更新FeedBin表格                                                      
                    int colNo = location.tray % 10;
                    int rowNo = location.tray / 10;
                    int layerID = (colNo - 1) * 8 + rowNo;
                    String checkResult = "OK";
                    DataTable dtFeedBin = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin where LayerID=" + layerID);
                    if (checkResult == "OK")
                    {
                        int okNum = (int)dtFeedBin.Rows[0]["ResultOK"] + 1;
                        DataBase.GetInstanse().DBUpdate("update dbo.FeedBin set ResultOK = " + okNum + "where LayerID=" + layerID);
                    }
                    else
                    {
                        int ngNum = (int)dtFeedBin.Rows[0]["ResultNG"] + 1;
                        DataBase.GetInstanse().DBUpdate("update dbo.FeedBin set ResultNG = " + ngNum + "where LayerID=" + layerID);
                    }
                    //FrameDataUpdate();

                    //测试结果插入测试统计表格；
                    //FinishTesting(checkResult);
                    return ;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                    FinishTask();
                    mIsRunning = false;
                }
            }
        }

        public bool Abort()
        {
            if (mTask != null)
            {
                mTask.Abort();
                mTask = null;
            }
            return true;
        }

        public bool Pause()
        {
            if (mTask != null && mTask.ThreadState == ThreadState.Running)
            {
                mTask.Suspend();
            }
            return true;
        }

        public bool Resume()
        {
            if (mTask != null && mTask.ThreadState == ThreadState.Suspended)
            {
                mTask.Resume();
            }
            return true;
        }
    }
}
