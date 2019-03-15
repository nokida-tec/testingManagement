using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.DAL;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using XT_CETC23.Instances;

namespace XT_CETC23
{
    class TestingTask
    {
        private int mID;        // 任务号
        private int mCabinetID; // 分配的测试柜
        private String mProductType; // 测试产品类型
        private Thread mTask;
        private Object lockObject = new Object();
        private bool isRunning = false;

        public enum status
        { // 需要和PLC对应
            [EnumDescription("料架位置")]
            FramePos = 100,
            [EnumDescription("测试台1")]
            Running = 101,
            [EnumDescription("测试台2")]
            Cabinet2 = 102,
            [EnumDescription("测试台3")]
            Cabinet3 = 103,
            [EnumDescription("测试台4")]
            Cabinet4 = 104,
            [EnumDescription("测试台5")]
            Cabinet5 = 105,
            [EnumDescription("测试台6")]
            Cabinet6 = 106,
        }

        public class Step
        {
            public enum Name
            { // 需要和PLC对应
                [EnumDescription("没有任务")]
                None = 100,
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
                    if (isRunning)
                    {
                        Logger.WriteLine("  ***   测试线程: " + this.mCabinetID + "在运行中 线程:" + mTask.ManagedThreadId + " 状态：" + mTask.ThreadState);
                        return ReturnCode.SysBusy;
                    }

                    isRunning = true;

                    mProductType = productType;

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

        private void runOne()
        {   // 运行一次
            lock (lockObject)
            {
                try
                {
                    isRunning = true;

                    Frame.Location location = new Frame.Location();

                    // 料仓取料
                    Frame.getInstance().doGetProduct(mProductType, ref location);

                    // 机器人从料仓取料
                    Robot.GetInstanse().doGetProductFromFrame(mProductType, location.mSlot, location.CordinatorX, location.CordinatorY, location.CordinatorU);
                    
                    // 机器人放料到测试台
                    Robot.GetInstanse().doPutProductToCabinet(mProductType, mCabinetID);

                    // 测试开始
                    TestingCabinets.getInstance(mCabinetID).doTest();

                    // 机器人从测试台取料
                    Robot.GetInstanse().doGetProductFromCabinet(mProductType, mCabinetID);

                    // 插入料架取料任务，取出托盘（要区分取出和放入）
                    Frame.getInstance().doGet(location.mTray);
                    
                    // 插入机器人回料任务
                    Robot.GetInstanse().doPutProductToFrame(mProductType, location.mSlot);
                    
                    Frame.getInstance().doPut(location.mTray);

                    // 根据结果更新FeedBin表格                                                      
                    int colNo = location.mTray % 10;
                    int rowNo = location.mTray / 10;
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
                    TestingTask.finish(MTR.globalBasicID, checkResult);
                    return ;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                    isRunning = false;
                }
            }
        }

        public int finish()
        {
            try
            { /*
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID = " + this.mCabinetID);
                String prodCode = failedReason != null ? failedReason : dt.Rows[0]["ProductID"].ToString();
                if (dt != null || dt.Rows.Count > 0)
                {
                    String prodType = dt.Rows[0]["ProductType"].ToString();
                    String cabinetName = dt.Rows[0]["CabinetID"].ToString();
                    String trayNo = dt.Rows[0]["FrameLocation"].ToString();
                    String pieceNo = dt.Rows[0]["SalverLocation"].ToString();
                    String BeginTime = dt.Rows[0]["BeginTime"].ToString();
                    String EndTime = dt.Rows[0]["EndTime"].ToString();

                    DataBase.GetInstanse().DBInsert("insert into dbo.ActualData("
                        + " ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,BeginTime,EndTime,CheckResult"
                        + " )values( '"
                        + prodCode + "','"
                        + prodType + "',"
                        + trayNo + ","
                        + pieceNo + ",'"
                        + cabinetName + "','"
                        + "0" + "','"
                        + "0" + "','"
                        + "0" + "','"
                        + "0" + "','"
                        + BeginTime + "','"
                        + EndTime + "','"
                        + checkResult + "')");
                    try
                    {
                        DataBase.GetInstanse().DBInsert("insert into dbo.FrameData("
                            + "BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,BeginTime,EndTime,CheckResult"
                            + " )values("
                            + ID + ",'"
                            + prodCode + "','"
                            + prodType + "',"
                            + trayNo + ","
                            + pieceNo + ",'"
                            + cabinetName + "','"
                            + BeginTime + "','"
                            + EndTime + "','"
                            + checkResult + "')");
                    }
                    catch (Exception e1)
                    {
                        Logger.WriteLine(e1);
                    }
                    DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + ID);
                } 
               */
                return 0;
            } 
            catch (Exception e)
            {
                Logger.WriteLine(e);
                return 0;
            }
            finally
            {
                isRunning = false;
            }
        }

        static public int finish(int ID, String checkResult, String failedReason = null)
        {
            DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID = " + ID);
            if (dt == null || dt.Rows.Count == 0)
            {
                return -1;
            }

            try
            {
                String prodCode = failedReason != null ? failedReason : dt.Rows[0]["ProductID"].ToString();
                String prodType = dt.Rows[0]["ProductType"].ToString();
                String cabinetName = dt.Rows[0]["CabinetID"].ToString();
                String trayNo = dt.Rows[0]["FrameLocation"].ToString();
                String pieceNo = dt.Rows[0]["SalverLocation"].ToString();
                String BeginTime = dt.Rows[0]["BeginTime"].ToString();
                String EndTime = dt.Rows[0]["EndTime"].ToString();

                DataBase.GetInstanse().DBInsert("insert into dbo.ActualData("
                    + " ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,BeginTime,EndTime,CheckResult"
                    + " )values( '"
                    + prodCode + "','"
                    + prodType + "',"
                    + trayNo + ","
                    + pieceNo + ",'"
                    + cabinetName + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + "0" + "','"
                    + BeginTime + "','"
                    + EndTime + "','"
                    + checkResult + "')");
                try
                {
                    DataBase.GetInstanse().DBInsert("insert into dbo.FrameData("
                        + "BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,BeginTime,EndTime,CheckResult"
                        + " )values("
                        + ID + ",'"
                        + prodCode + "','"
                        + prodType + "',"
                        + trayNo + ","
                        + pieceNo + ",'"
                        + cabinetName + "','"
                        + BeginTime + "','"
                        + EndTime + "','"
                        + checkResult + "')");
                }
                catch (Exception e1)
                {
                    Logger.WriteLine(e1);
                }
                DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + ID);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
            }
            return ID;
        }


        public bool abort()
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
