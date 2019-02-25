using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.INTransfer;
using System.Threading;
using XT_CETC23.SonForm;
using XT_CETC23.DataManager;
using XT_CETC23.Common;
using System.IO;
using System.Windows.Forms;
using System.Data;
using XT_CETC23.Model;
using XT_CETC23.Instances;
using XT_CETC23.DAL;

namespace XT_CETC23.DataCom
{
   public  class Run
    {
        static Run run;
        public Plc plc;
        public DataBase db;
        TaskCycle taskCycle;
        IRunForm iRunForm;
        IAutoForm iAutoForm;
        IManulForm iManulForm;
        ICameraForm iCameraForm;
        
        delegate void getCabinetResult(int CabinetNum,string message);
        getCabinetResult GetCabinetResult;
        delegate void getCabinetStatus(int CabinetNum, string message);
        getCabinetStatus GetCabinetStatus;
        delegate void getGrabNO(int grabNo);
        getGrabNO GetGrabNO;
        delegate void getInitStatus(bool sta);
        getInitStatus GetInitStatus;
        delegate void getPlcMode(string mode,string status);
        getPlcMode GetPlcMode;
        delegate void getRobotMode(byte mode);
        getRobotMode GetRobotMode;
        delegate void getProductID(string id);
        getProductID GetProductID;
        delegate void transMessage(string message);
        transMessage TransMessage;
        delegate void transAutoData(string[] str, string[] str1);
        transAutoData TransAutoData;
        delegate void frameDataUpdate();
        frameDataUpdate FrameDataUpdate;
        delegate void manulEnable(string mode,string status);
        manulEnable ManulEnable;

        CameraForm cFrom;
        RunForm rForm;
        AutoForm aForm;
        ManulForm mForm;
        Thread readPlcTh;
        Thread Image;
        Thread readCabinetTh;
        Thread modeControl;
        Thread mainSchedule;
        public static bool InitStatus;

        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public static string modeByPlc = "";
        public static string statusByPlc = "";
        public static string commandByPlc = "";
        public static bool frameUpdate = false;
        public static bool stepEnable = false;
        public static bool readyForStep = false;
        int preAlarmNo = 0;
        public static bool gSheduleExit = false;

        static public Run GetInstanse(IRunForm iRunForm,IAutoForm iAutoForm,IMainForm iMainFrom,IManulForm iManulForm, ICameraForm iCameraForm)
        {
            if(run==null)
            {
                run = new Run(iRunForm, iAutoForm, iMainFrom, iManulForm, iCameraForm);
            }
            return run;
        }

        private void ReadPlc()
        {
            int flag = 0;            
            while (true)
            {
                try
                {
                    PlcData.PlcStatusValue = plc.DbRead(100, 0, 21);
                    if (PlcData.PlcStatusValue != null)
                    {
                        flag = 0;
                        PlcData._robotStatus = PlcData.PlcStatusValue[0];
                        
                        PlcData._plcMode = PlcData.PlcStatusValue[1];
                        
                        PlcData._axlis7Status = PlcData.PlcStatusValue[2];
                        PlcData._axlis2Status = PlcData.PlcStatusValue[3];
                        PlcData._axlis2Pos = PlcData.PlcStatusValue[4];
                        PlcData._cabinetStatus_old = PlcData.PlcStatusValue[5];

                        PlcData._frameFeedBack = PlcData.PlcStatusValue[6];                //料架气缸

                        PlcData._limitFeedBack1 = PlcData.PlcStatusValue[7];
                        PlcData._limitFeedBack2 = PlcData.PlcStatusValue[8];
                        PlcData._limitFeedBack3 = PlcData.PlcStatusValue[9];
                        PlcData._limitFeedBack4 = PlcData.PlcStatusValue[10];
                        PlcData._limitFeedBack5 = PlcData.PlcStatusValue[11];
                        PlcData._limitFeedBack6 = PlcData.PlcStatusValue[12];

                        PlcData._cabinetStatus[0] = PlcData.PlcStatusValue[13];
                        PlcData._cabinetStatus[1] = PlcData.PlcStatusValue[14];
                        PlcData._cabinetStatus[2] = PlcData.PlcStatusValue[15];
                        PlcData._cabinetStatus[3] = PlcData.PlcStatusValue[16];
                        PlcData._cabinetStatus[4] = PlcData.PlcStatusValue[17];
                        PlcData._cabinetStatus[5] = PlcData.PlcStatusValue[18];
                        PlcData._alarmNumber = PlcData.PlcStatusValue[20];

                        #region  测试柜状态读取
                        for (int i=0;i<6;i++)
                        {
                            if (stepEnable == false)
                            {
                                if ((PlcData._cabinetStatus[i] & 1) != 0)
                                    GetCabinetStatus(i + 1, "可放料");
                                else if ((PlcData._cabinetStatus[i] & 2) != 0)
                                    GetCabinetStatus(i + 1, "可测试");
                                else if ((PlcData._cabinetStatus[i] & 4) != 0)
                                    GetCabinetStatus(i + 1, "测试中");
                                else if ((PlcData._cabinetStatus[i] & 8) != 0)
                                    GetCabinetStatus(i + 1, "可取料");
                                else
                                    GetCabinetStatus(i + 1, "准备中");
                            }
                        }
                        #endregion

                        #region  PLC控制状态读取和解析
                        if ((PlcData._plcMode & PlcData._readPlcMode) != 0)
                        {
                            modeByPlc = "Auto";                            
                        }
                        else
                        {
                            modeByPlc = "Manul";
                        }

                        if ((PlcData._plcMode & PlcData._readPlcInited) != 0)
                        {
                            commandByPlc = "Initialize";
                        }
                        if ((PlcData._plcMode & PlcData._readPlcStart) != 0)
                        {
                            commandByPlc = "Start";
                        }
                        if ((PlcData._plcMode & PlcData._readPlcEmergency) != 0)
                        {
                            commandByPlc = "Emergency";
                        }

                        if ((PlcData._plcMode & PlcData._readPlcAutoRunning) != 0)
                        {
                            statusByPlc = "AutoRunning";
                        }
                        if ((PlcData._plcMode & PlcData._readPlcPausing) != 0)
                        {
                            statusByPlc = "Pausing";
                        }
                        if ((PlcData._plcMode & PlcData._readPlcAlarming) != 0)
                        {
                            statusByPlc = "Alarming";
                        }
                        if ((PlcData._plcMode & PlcData._readPlcInited) != 0)
                        {
                            statusByPlc = "Initalized";
                        }
                        #endregion

                        GetRobotMode(PlcData._robotStatus);
                        GetPlcMode(modeByPlc,statusByPlc);
                        ManulEnable(modeByPlc,statusByPlc);                        

                        Thread.Sleep(100);
                    }
                    else
                    {
                        if (flag == 0)
                        {
                            TransMessage("PLC数据读取失败");
                        }
                        flag = 1;
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    MessageBox.Show(e.Message);
                }
                Thread.Sleep(100);
            }            
        }

        private Run(IRunForm iRunForm, IAutoForm iAutoForm, IMainForm iMainForm, IManulForm iManulForm, ICameraForm iCameraForm)
        {
            plc = Plc.GetInstanse();
            db = DataBase.GetInstanse();
            taskCycle = TaskCycle.GetInstanse();

            TestingSystem.GetInstanse().SetExitSystem();

            this.iCameraForm = iCameraForm;
            this.iRunForm = iRunForm;
            this.iAutoForm = iAutoForm;
            this.iManulForm = iManulForm;
            cFrom = (CameraForm)this.iCameraForm;
            rForm = (RunForm)this.iRunForm;
            aForm = (AutoForm)this.iAutoForm;
            mForm = (ManulForm)this.iManulForm;
            TransAutoData = iAutoForm.getSort;//返回自动模式扫描的产品种类及数量数据
            TransMessage = iRunForm.transMessage;
            GetCabinetResult = iRunForm.getCabinetResult;
            GetCabinetStatus = iRunForm.getCabinetStatus;
            GetGrabNO = iRunForm.getGrabNO;
            GetRobotMode = iRunForm.getRobotMode;
            GetInitStatus = iRunForm.getInitStatus;
            GetPlcMode = iRunForm.getPlcMode;
            GetProductID = iRunForm.getProductID;
            FrameDataUpdate = iAutoForm.frameDataUpdate;
            ManulEnable = iMainForm.manulEnable;

            grabTypeQuery();

            //readCabinetTh = new Thread(ReadCabinet);
            //readCabinetTh.Name = "读取测试柜状态";
            //if (!readCabinetTh.IsAlive)
            //{
            //    readCabinetTh.Start();
            //}

            timer.Elapsed += Timer_Elapsed;
            timer.Start();
           
            modeControl = new Thread(ModeControl);
            modeControl.Name = "模式监控";
            if (!modeControl.IsAlive)
            {
                modeControl.Start();                
            }
            //new Thread(CabinetControl).Start();
        }

        private void ModeControl()
        {
            plc = Plc.GetInstanse();
            Robot.GetInstanse();
            bool initializing = false;
            bool mainStarting = false;
            readPlcTh = new Thread(ReadPlc);
            readPlcTh.Name = "读取PLC数据";
            readCabinetTh = new Thread(ReadCabinet);
            readCabinetTh.Name = "读取测试柜状态";
            TransMessage("监控进程启动");            

            while (true)
            {
                if (plc.plcConnected)
                {                    
                    if (!readPlcTh.IsAlive)
                    {
                        readPlcTh.Start();
                    }
                    if (readCabinetTh.ThreadState == ThreadState.Suspended)
                    {
                        readCabinetTh.Resume(); 
                    }

                    if (modeByPlc == "Auto" && commandByPlc == "Start" && !mainStarting && initializing == true)
                    {
                        readyForStep = false;
                        mForm.clearTask();
                        mainSchedule = new Thread(MainSchedule);
                        mainSchedule.Name = "主调度流程";
                        mainSchedule.Start();
                        TransMessage("主调度进程启动");
                        Logger.WriteLine("mainSchedule thread started, Thread Name is: " + mainSchedule.Name + "  ID: " + mainSchedule.ManagedThreadId);
                        mainStarting = true;
                        gSheduleExit = false;
                        
                        commandByPlc = "";
                    }

                    if (modeByPlc == "Manul" || commandByPlc == "Emergency")
                    {
                        try
                        {
                            if (mainSchedule != null && mainSchedule.IsAlive)
                            {
                                mainSchedule.Abort();
                                Logger.WriteLine("mainSchedule thread exit, Thread Name is: " + mainSchedule.Name + "  ID: " + mainSchedule.ManagedThreadId);
                                gSheduleExit = true;
                                TestingSystem.GetInstanse().ExitSystem();
                                TransMessage("主调度进程退出");
                            }
                            mainStarting = false;
                            initializing = false;
                            commandByPlc = "";
                        }
                        catch(Exception e)
                        {
                            Logger.WriteLine(e);
                        }
                    }
                    if (modeByPlc == "Auto" && commandByPlc == "Initialize" && !initializing)
                    {                       
                        mForm.clearTask();
                        SetProdType2Plc();
                        InitStatus = getInitResult();
                        do
                        {
                            Thread.Sleep(100);

                        } while (!InitStatus || (statusByPlc != "Initalized"));
                                                
                        Thread.Sleep(1000);
                        do
                        {
                            Thread.Sleep(100);
                        } while(!plc.DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 1 }));
                        Thread.Sleep(1000);                       
                        TransMessage("初始化成功");
                        initializing = true;
                        //readyForStep = true;
                        commandByPlc = "";                      
                    }

                    //if (stepEnable && mainSchedule != null && mainSchedule.ThreadState == ThreadState.Running)
                    //{
                    //    mainSchedule.Suspend();
                    //    TransMessage("主调度进程挂起");
                    //}

                    //if (modeByPlc == "Auto" && statusByPlc == "AutoRunning" && !stepEnable)

                    if (modeByPlc == "Auto" && statusByPlc == "AutoRunning")
                    {
                        if (readPlcTh.ThreadState == ThreadState.Suspended)
                        {
                            readPlcTh.Resume();
                            TransMessage("读取PLC状态进程恢复");
                        }
                        if (mainSchedule != null && mainSchedule.ThreadState == ThreadState.Suspended)
                        {
                            mainSchedule.Resume();
                            TransMessage("主调度进程恢复");
                        }
                        TestingCabinets.Resume();
                    }

                }
                else
                {
                    plc.ConnectPlc("192.168.10.10", 0, 0);
                    if (readPlcTh.IsAlive)
                    {                       
                        if (readPlcTh.ThreadState == ThreadState.Running)
                        {
                            readPlcTh.Suspend();
                            TransMessage("读取PLC状态进程挂起");
                        }
                        //if (Image.ThreadState == ThreadState.Running)
                        //{
                        //    Image.Suspend();
                        //}
                        if (mainSchedule != null && mainSchedule.ThreadState == ThreadState.Running)
                        {
                            mainSchedule.Suspend();
                            TransMessage("主调度进程挂起");
                        }
                        TestingCabinets.Pause();
                    }
                }
                PrintAlarm(PlcData._alarmNumber);                
                Thread.Sleep(100);
            }                    
        }

        public void CabinetControl()
        {
            readCabinetTh = new Thread(ReadCabinet);
            readCabinetTh.Name = "读取测试柜状态";

            if (!readCabinetTh.IsAlive)
            {
                readCabinetTh.Start();
            }

            while (true)
            {
                    if (readCabinetTh.ThreadState == ThreadState.Suspended)
                    {
                        readCabinetTh.Resume();
                    }

                Thread.Sleep(100);
            }
        }

        public bool getInitResult()
        {
            int i = 0;
            //相机初始化留位 

            if (db.DBInit())
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
            
            if (!readCabinetTh.IsAlive)
            {
                readCabinetTh.Start();
            }

            for (int j = 13; j < 19;j++ )
            {
                plc.DBWrite(PlcData.PlcWriteAddress, j, 1, new Byte[] { 0 });
            }
            plc.DBWrite(100, 2, 1, new Byte[] { 0 });
            plc.DBWrite(100, 3, 1, new Byte[] { 0 });
            //taskCycle = TaskCycle.GetInstanse();

            return i == 2;
        }

        private void MainSchedule()
        {
            try
            {
                DataTable dtMTR = new DataTable();
                DataTable dtFeedBin = new DataTable();
                DataTable dtSortData = db.DBQuery("select * from dbo.SortData");

                String prodType = "";
                String prodCode = "";
                String cabinetName = "";
                string checkResult = "";
                int cabinetNo = 0;
                int trayNo = 0;
                int pieceNo = 0;
                MTR mtr = MTR.GetIntanse();
                bool frameEmptyInd=false;
                bool isFirstTime = true;

                //判断料架是否已经完成扫码，如果没有，则插入扫码任务
                //db.DBDelete("delete from dbo.MTR");
                //db.DBUpdate("update dbo.FeedBin set Sort='" + "No" + "',NumRemain=" + 0 + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + 88);

            ReStart:
                if (!stepEnable)
                {
                    dtFeedBin = db.DBQuery("select * from dbo.FeedBin where LayerID=88");
                    TaskCycle.feedBinScanDone = dtFeedBin.Rows[0]["Sort"].ToString().Trim();
                    if (TaskCycle.feedBinScanDone == "No")
                    {
                        Frame.getInstance().doScan();
                        
                        TaskCycle.feedBinScanDone = "Yes";
                        db.DBUpdate("update dbo.FeedBin set Sort='" + "Yes" + "',NumRemain=" + 0 + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + 88);
                        FrameDataUpdate();
                        frameEmptyInd = false;
                        //readyForStep = true;
                    }
                }


                while (true)
                {
                    if(TaskCycle.feedBinScanDone == "Yes")
                    {
                        if (gSheduleExit == true)
                        {
                            return;
                        }
                        #region 根据数据库中的跟踪表MTR,区分不同的情况进行相应的处理.
                    TakeBack:
                        dtMTR = db.DBQuery("select * from dbo.MTR");
                        if (dtMTR.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtMTR.Rows.Count; j++)
                            {
                                if (!stepEnable)
                                {
                                    if (gSheduleExit == true)
                                    {
                                        return;
                                    }
                                    string tmpText = dtMTR.Rows[j]["CurrentStation"].ToString().Trim().Substring(2);
                                    bool statusTest = (bool)dtMTR.Rows[j]["StationSign"];

                                    TaskCycle.actionType = "CabinetToFrame";
                                    MTR.globalBasicID = (int)dtMTR.Rows[j]["BasicID"];
                                    cabinetNo = (int)dtMTR.Rows[j]["CabinetID"];
                                    cabinetName = dtMTR.Rows[j]["CurrentStation"].ToString().Trim();
                                    trayNo = (int)dtMTR.Rows[j]["FrameLocation"];
                                    pieceNo = (int)dtMTR.Rows[j]["SalverLocation"];
                                    prodType = dtMTR.Rows[j]["ProductType"].ToString().Trim();
                                    prodCode = dtMTR.Rows[j]["ProductID"].ToString().Trim();
                                    checkResult = dtMTR.Rows[j]["ProductCheckResult"].ToString().Trim();

                                    #region 测试柜中且测试完成,把测量完成的物料从测试柜取出放回料架，删除测试跟踪记录，把测试结果插入测试记录表
                                    if (tmpText.Equals("号机台") && statusTest)
                                    {
                                        //插入机器轨道任务
                                        //插入机器人轨道任务：到测试柜
                                        //判断机器人是否在原点
                                        //插入机器人从测试柜的取料任务；
                                        Robot.GetInstanse().doGetProductFromCabinet(prodType, cabinetNo);

                                        //插入料架取料任务，取出托盘（要区分取出和放入）
                                        Frame.getInstance().doGetAsync(trayNo);  // 异步方式, Robot.doPutProductToFrame中会检查结果
                                        
                                        //插入机器人回料任务
                                        Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo);

                                        //插入料架放料任务，放回托盘；（要区分取出和放入）
                                        db.DBUpdate("update dbo.MTR set CurrentStation = 'FeedBin',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                                        Frame.getInstance().doPut(trayNo);

                                        //根据结果更新FeedBin表格                                                      
                                        int colNo = trayNo % 10;
                                        int rowNo = trayNo / 10;
                                        int layerID = (colNo - 1) * 8 + rowNo;
                                        dtFeedBin = db.DBQuery("select * from dbo.FeedBin where LayerID=" + layerID);
                                        if (checkResult == "OK")
                                        {
                                            int okNum = (int)dtFeedBin.Rows[0]["ResultOK"] + 1;
                                            db.DBUpdate("update dbo.FeedBin set ResultOK = " + okNum + "where LayerID=" + layerID);
                                        }
                                        else
                                        {
                                            int ngNum = (int)dtFeedBin.Rows[0]["ResultNG"] + 1;
                                            db.DBUpdate("update dbo.FeedBin set ResultNG = " + ngNum + "where LayerID=" + layerID);
                                        }
                                        FrameDataUpdate();

                                        //测试结果插入测试统计表格；
                                        TestingTask.finish(MTR.globalBasicID, checkResult);
                                    }
                                    #endregion

                                    #region 物料在测试柜中且未测试完成,完成测试即可
                                    else if (tmpText.Equals("号机台") && !statusTest && isFirstTime)
                                    //else if (tmpText.Equals("号机台") && !statusTest)
                                    {                                        
                                        //插入测试任务
                                        db.DBUpdate("update dbo.MTR set CurrentStation = '" + cabinetName + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                                        TestingCabinets.getInstance(cabinetNo).cmdStart(prodType, MTR.globalBasicID);
                                        //db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                                        // DialogResult result = MessageBox.Show("有未完成测试任务，请确认把产品从测试台中拿出，然后点击确定按钮", "重要提示");
                                        // db.DBDelete("delete from dbo.MTR");
                                        // db.DBUpdate("update dbo.FeedBin set Sort='" + "No" + "',NumRemain=" + 0 + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + 88);
                                        //                            TestingCabinets.getInstance(cabinetNo).cmdStart(prodType, MTR.globalBasicID);
                                    }
                                    //=========================================================================================================================
                                    #endregion

                                    #region 其它,终止该过程
                                    else if (!tmpText.Equals("号机台"))
                                    //else
                                    {
                                        db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                                    }
                                    #endregion
                                    //readyForStep = true;                                    
                                }
                            }
                            isFirstTime = false;
                        }
                        #endregion

                        #region  料架取空处理
                        //料架取空处理
                        if (!stepEnable)
                        {
                            dtFeedBin = db.DBQuery("select * from dbo.FeedBin where LayerID=88");
                            TaskCycle.feedBinScanDone = dtFeedBin.Rows[0]["Sort"].ToString().Trim();
                            dtMTR = db.DBQuery("select * from dbo.MTR");
                            if (dtMTR.Rows.Count < 1 && frameEmptyInd == true)
                            {
                                Frame.getInstance().excuteCommand(Frame.Lock.Command.Open);
                                db.DBUpdate("update dbo.FeedBin set Sort='" + "No" + "',NumRemain=" + 0 + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + 88);
                                db.DBDelete("delete from dbo.FrameData");
                                frameUpdate = false;
                                //plc.DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 2 });
                                MessageBox.Show("料架已取空，请更换料架");
                                while (!frameUpdate)
                                {
                                    
                                    Thread.Sleep(100);
                                }

                                //plc.DBWrite(PlcData.PlcWriteAddress, 1, 1, new Byte[] { 0 });
                                Frame.getInstance().excuteCommand(Frame.Lock.Command.Close);

                                frameUpdate = false;
                                frameEmptyInd = false;                                
                                goto ReStart;
                            }
                            else if (dtMTR.Rows.Count > 0 && frameEmptyInd == true)
                            {
                                //readyForStep = true;
                                goto TakeBack;
                            }
                        }
                        #endregion


                        #region 把物料从料架取出放入测试柜并触发测试任务
                        int testedCabinetNo = 0;
                        for (int i = 0; i < DeviceCount.TestingCabinetCount; i++)
                        {
                            if (!stepEnable)
                            {
                                dtMTR = db.DBQuery("select * from dbo.MTR");
                                bool testExsit = false;
                                for (int m = 0; m < dtMTR.Rows.Count; m++)
                                {
                                    if (gSheduleExit == true)
                                    {
                                        
                                        return;
                                    }
                                    string CabInMTR = dtMTR.Rows[m]["CurrentStation"].ToString().Trim();
                                    if (TestingCabinets.getInstance(i).Name == CabInMTR)
                                    {
                                        testExsit = true;
                                        break;
                                    }
                                }

                                if (((PlcData._cabinetStatus[i] & 1) != 0)
                                    && (TestingCabinets.getInstance(i).Enable == TestingCabinet.ENABLE.Enable) && !testExsit)               //如果测试允许测试并且使能
                                {
                                Redo:
                                    testedCabinetNo++;
                                    TaskCycle.actionType = "FrameToCabinet";
                                    int numRemain = 0;
                                    int layerID = 0;
                                    cabinetNo = i;
                                    prodType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;
                                    cabinetName = TestingCabinets.getInstance(cabinetNo).Name;
                                    int ret = mtr.InsertBasicID("0", 0, 0, prodType, "FeedBin", false, "0", cabinetNo);
                                    if (ret < 0)
                                    {
                                        goto Redo;
                                    }

                                    Thread.Sleep(100);

                                    //针对MTR表格中多条纪录，选择还未取料的任务
                                    dtMTR = db.DBQuery("select * from dbo.MTR");
                                    for (int j = 0; j < dtMTR.Rows.Count; j++)
                                    {
                                        if (dtMTR.Rows[j]["CurrentStation"].ToString().Trim().Equals("FeedBin"))
                                        {
                                            MTR.globalBasicID = (int)dtMTR.Rows[j]["BasicID"];
                                        }
                                    }

                                    //加入机器人是否空闲并处在安全位置的判断

                                    //插入机器人轨道到料架任务
                                    // TaskCycle.PickStep = 0;

                                    Robot.GetInstanse().doMoveToZeroPos();

                                    // TaskCycle.PickStep = 10;
                                    //查FeedBin表，确定料盘位置和物料在料盘中的位置，插于取料盘任务
                                    db.DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                                pickAnotherTray:
                                    dtFeedBin = db.DBQuery("select * from dbo.FeedBin where sort='" + prodType + "'");
                                    for (int j = 0; j < dtFeedBin.Rows.Count; j++)
                                    {
                                        if ((int)dtFeedBin.Rows[j]["NumRemain"] != 0)
                                        {
                                            layerID = (int)dtFeedBin.Rows[j]["LayerID"];
                                            int colNo = (layerID - 1) / 8;
                                            int rowNo = (layerID - 1) % 8;
                                            trayNo = (rowNo + 1) * 10 + (colNo + 1);

                                            DataTable dtTmp = db.DBQuery("select * from dbo.SortData where sortname='" + prodType + "'");
                                            numRemain = (int)dtFeedBin.Rows[j]["NumRemain"];
                                            pieceNo = (int)dtTmp.Rows[0]["number"] - numRemain + 1;       //从0开始编号

                                            db.DBUpdate("update dbo.MTR set FrameLocation = " + trayNo + "," + "SalverLocation=" + pieceNo + " where BasicID=" + MTR.globalBasicID);
                                            Frame.getInstance().doGet(trayNo);
                                            break;
                                        }
                                        else
                                        {
                                            if ((testedCabinetNo == TestingCabinets.getEnableCount()) && (j == (dtFeedBin.Rows.Count - 1)))       //如果料架取空，设置扫描状态“No”
                                            {
                                                frameEmptyInd = true;
                                                goto PickEnd;
                                            }
                                            if ((j == (dtFeedBin.Rows.Count - 1)))                                   //如果某种产品取空，跳过本次操作
                                            {
                                                goto PickEnd;
                                            }
                                        }
                                    }

                                    int prodNumber = 0;
                                    switch (prodType)
                                    {
                                        case "A":
                                            prodNumber = 1;
                                            break;
                                        case "B":
                                            prodNumber = 2;
                                            break;
                                        case "C":
                                            prodNumber = 3;
                                            break;
                                        case "D":
                                            prodNumber = 4;
                                            break;
                                        case "E":
                                            prodNumber = 5;
                                            break;
                                        case "F":
                                            prodNumber = 6;
                                            break;
                                    }
                                shootAgain:
                                    string CordinatorX = "0";
                                    string CordinatorY = "0";
                                    string CordinatorU = "0";

                                    //D产品不对第4列进行拍照的处理
                                    //=====================================================
                                    if (prodNumber == 4)
                                    {
                                        if (pieceNo % 4 == 0)
                                        {
                                            db.DBUpdate("update dbo.FeedBin set NumRemain = " + (numRemain - 1) + "where LayerID=" + layerID);
                                            //db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                                            if ((numRemain - 1) == 0)
                                            {
                                                // 插入放回料盘任务
                                                Frame.getInstance().doPut(trayNo);
                                                goto pickAnotherTray;               //换盘
                                            }
                                            if ((numRemain - 1) > 0)
                                            {
                                                pieceNo = pieceNo + 1;              //换位置
                                                numRemain = numRemain - 1;
                                                db.DBUpdate("update dbo.MTR set SalverLocation=" + pieceNo + " where BasicID=" + MTR.globalBasicID);
                                                goto shootAgain;
                                            }
                                        }
                                    }
                                    //=======================================================

                                    //触发拍照
                                    cFrom.CCDTrigger(prodNumber, pieceNo);

                                    if (cFrom.CCDDone == -1)                                  //拍照失败
                                    {
                                        db.DBUpdate("update dbo.FeedBin set NumRemain = " + (numRemain - 1) + "where LayerID=" + layerID);
                                        //db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                                        if ((numRemain - 1) == 0)
                                        {
                                            //插入放回料盘任务
                                            Frame.getInstance().doPut(trayNo);
                                            goto pickAnotherTray;               //换盘
                                        }
                                        if ((numRemain - 1) > 0)
                                        {
                                            pieceNo = pieceNo + 1;              //换位置
                                            numRemain = numRemain - 1;
                                            db.DBUpdate("update dbo.MTR set SalverLocation=" + pieceNo + " where BasicID=" + MTR.globalBasicID);
                                            goto shootAgain;
                                        }

                                    }

                                    if (prodType == "D")
                                    {
                                        CordinatorX = cFrom.X;
                                        CordinatorY = cFrom.Y;
                                    }

                                    //插入机器人取料任务                         
                                    db.DBUpdate("update dbo.MTR set CurrentStation = 'Robot',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                                    ReturnCode retCode = Robot.GetInstanse().doGetProductFromFrame (prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);


                                    db.DBUpdate("update dbo.FeedBin set NumRemain = " + (numRemain - 1) + "where LayerID=" + layerID);

                                    if (retCode == ReturnCode.ScanFailed)      //读码失败
                                    {
                                        //db.DBUpdate("update dbo.MTR set ProductID = '" + "0" +"',"++ "'where BasicID=" + MTR.globalBasicID);
                                        TestingTask.finish(MTR.globalBasicID, "NG", "扫描失败");

                                        //插入机器人放料到料架任务
                                        Robot.GetInstanse().doPutProductToFrame (prodType ,pieceNo, CordinatorX, CordinatorY, CordinatorU);
                                        plc.DBWrite(PlcData.PlcStatusAddress, 3, 1, new Byte[] { 0 });
                                        FrameDataUpdate();

                                      //  TaskCycle.PickStep = 40;
                                        //插入放回料盘任务
                                        Frame.getInstance().doPut(trayNo);
                                        goto Redo;
                                    }

                                    //读码成功                       
                                    Byte[] myCode = plc.DbRead(104, 0, 556);
                                    Thread.Sleep(2000);
                                    plc.DBWrite(PlcData.PlcStatusAddress, 3, 1, new Byte[] { 0 });
                                    int strLen = Convert.ToInt32(myCode[504]);
                                    int realLen = Convert.ToInt32(myCode[505]);
                                    prodCode = Encoding.Default.GetString(myCode, 506, realLen).Trim();

                                    db.DBUpdate("update dbo.MTR set ProductID = '" + prodCode + "'where BasicID=" + MTR.globalBasicID);

                                    //插入放回料盘任务
                                    Frame.getInstance().doPutAsync(trayNo); 

                                    //插入机器人放料任务
                                    db.DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                                    Robot.GetInstanse().doPutProductToCabinet(prodType, cabinetNo);


                                    //插入测试任务
                                    db.DBUpdate("update dbo.MTR set CurrentStation = '" + cabinetName + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
                                    TestingCabinets.getInstance(cabinetNo).cmdStart(prodType, MTR.globalBasicID);

                                PickEnd:
                                    int PickEndI = 0;
//                                    TaskCycle.PickStep = 0;
                                }
                                //readyForStep = true;
                            }
                        }
                        #endregion                        
                    }
                    Thread.Sleep(100);
                    if (stepEnable && !readyForStep)
                    {
                        //插入机器人轨道到料架任务
                        TaskCycle.actionType = "FrameToCabinet";
                        Robot.GetInstanse().doMoveToZeroPos();
                        mForm.clearTask();
                        readyForStep = true;
                    }                                     
                }
            }
            catch(Exception e)
            {
                Logger.WriteLine(e);
            }
            Logger.WriteLine(" ***** exit 调度进程 thread");

        }

        private void SetProdType2Plc()
        {
            byte[] prodType = new byte[1];
            int cabinetStatus = 0;
            for (int i = 0; i < DeviceCount.TestingCabinetCount; ++i)
            {
                prodType[0] = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(i).Type].PlcMode;

                plc.DBWrite(PlcData.PlcWriteAddress, 21 + i, 1, prodType);
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
            plc.DBWrite(PlcData.PlcWriteAddress, 20, 1, prodType);
        }

        private void PrintAlarm(int alarmId)
        {
            string tmpStr = "";
            if (PlcData._alarmNumber!=preAlarmNo)
            {
                DataTable dt = db.DBQuery("select * from dbo.Alarm where AlarmID ="+ alarmId);
                if (dt.Rows.Count > 0)
                {
                    tmpStr = dt.Rows[0]["AlarmDescription"].ToString().Trim();
                    TransMessage(tmpStr);
                    MessageBox.Show(tmpStr);
                }
            }
        }

        void grabTypeQuery()
        {
            MaterielData.grabType = db.DBQuery("select * from sortdata");
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //自动模式
            if (PlcData._plcMode == 25)
            {
                for (int i = 0; i < CabinetData.pathCabinetStatus.Length; ++i)
                {
                    if (TestingCabinets.getInstance(i).Status == TestingCabinet.STATUS.Testing)
                    {

                    }
                }
            }

        }

        public bool writePlcMode(byte[] value)
        {
            return plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writePlcMode, PlcData._writeLength1, value);
        }

        public bool writeRobot(byte[] value)
        {
            return plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeRobot, PlcData._writeLength1, value);
        }

        void ReadCabinet()
        {
            while (true)
            {
                if (CabinetData.pathCabinetStatus != null)
                {
                    for (int i = 0; i < CabinetData.pathCabinetStatus.Length; ++i)
                    {
                        TestingCabinet.STATUS cabinetStatus = TestingCabinets.getInstance(i).ReadData();
                        if (stepEnable == false)
                        {
                            GetCabinetResult(i + 1, EnumHelper.GetDescription(cabinetStatus));
                        }
                        TestingCabinets.getInstance(i).Status = cabinetStatus;
                    }
                }
                Thread.Sleep(100);
            }
        }

    }
}
