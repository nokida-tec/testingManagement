using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.INTransfer;
using XT_CETC23.DataCom;
using XT_CETC23.DataManager;
using XT_CETC23.Common;
using System.Threading;

namespace XT_CETC23.SonForm
{
    public partial class RunForm : Form,IRunForm
    {
        public delegate void transMessageToMain(string message);
        public event transMessageToMain TransMessageToMain;
        public delegate void transStatusToMain(string name,string message);
        public event transStatusToMain TransStatusToMain;
        //public Run run;
        //bool InitStatus = false;
        IAutoForm AutoForm;
        ICameraForm CameraForm;
        IDatabaseForm DataForm;
        IManulForm ManulForm;
        IParaForm ParaForm;
        IUserForm UserForm;
        IMainForm MainForm;
        public Plc plc;
        public Robot robot;
        static RunForm rForm;
        Label[] grab,mode;
        byte[] plcModeValue=new byte[1];
        DataBase db = DataBase.GetInstanse();
        DataTable dtFeedBin = new DataTable();

        public static RunForm GetInstanse(IAutoForm AutoForm, ICameraForm CameraForm, IDatabaseForm DataForm, IManulForm ManulForm, IMainForm MainForm)
        {
            if(rForm==null)
            {
                rForm= new RunForm(AutoForm, CameraForm, DataForm, ManulForm, MainForm);
            }         
            return rForm;
        }
        RunForm(IAutoForm AutoForm, ICameraForm CameraForm, IDatabaseForm DataForm, IManulForm ManulForm,IMainForm MainForm)
        {
            InitializeComponent();
            InitForm();
            this.AutoForm = AutoForm;
            this.CameraForm = CameraForm;
            this.DataForm = DataForm;
            this.ManulForm = ManulForm;
            this.MainForm = MainForm;

            // 注册显示函数
            TestingSystem.GetInstance().RegistryDelegate(ShowMode);
            TestingSystem.GetInstance().RegistryDelegate(ShowInitialize);
            TestingSystem.GetInstance().RegistryDelegate(ShowStatus);
            Plc.GetInstanse().RegistryDelegate(ShowPlcMode);
            Robot.GetInstanse().RegistryDelegate(onRobotStatusChanged);

            // run = Run.GetInstanse(this, this.AutoForm,this.MainForm,this.ManulForm,this.CameraForm);
            //this.UserForm = iUserForm;
            mode = new Label[] { lb_Cabinet1_env, lb_Cabinet2_env, lb_Cabinet3_env, lb_Cabinet4_env, lb_Cabinet5_env, lb_Cabinet6_env };
            grab = new Label[] { lb_Cabinet1_gv, lb_Cabinet2_gv, lb_Cabinet3_gv, lb_Cabinet4_gv, lb_Cabinet5_gv, lb_Cabinet6_gv };
        }

        private void ShowMode(TestingSystem.Mode mode)
        {
            switch (mode)
            {
                case TestingSystem.Mode.Auto:
                    run_btnAuto.BackColor = Color.Green;
                    run_btnManul.BackColor = Color.PowderBlue;
                    break;
                case TestingSystem.Mode.Manual:
                    run_btnAuto.BackColor = Color.PowderBlue;
                    run_btnManul.BackColor = Color.Green;
                    break;
                default:
                    run_btnAuto.BackColor = Color.Green;
                    run_btnManul.BackColor = Color.Green;
                    break;
            }
        }

        private void ShowPlcMode(bool status)
        {  // 显示PLC状态
            switch (status)
            {
                case true:
                    run_lbPlcStatusv.Text = "运行中";
                    run_lbPlcStatusv.BackColor = Color.Green;
                    break;
                default:
                    run_lbPlcStatusv.Text = "故障";
                    run_lbPlcStatusv.BackColor = Color.PowderBlue;
                    break;
            }
        }

        private void ShowInitialize(TestingSystem.Initialize initialize)
        {
            switch (initialize)
            {
                case TestingSystem.Initialize.Initialize:
                    break;
                case TestingSystem.Initialize.Initialized:
                    break;
                default:
                    break;
            }
        }

        private void ShowStatus(TestingSystem.Mode mode, TestingSystem.Status status)
        {
            if (mode == TestingSystem.Mode.Auto && status == TestingSystem.Status.Running)
            {
                run_btnInit.BackColor = Color.Green;
                run_btnOff.BackColor = Color.PowderBlue;
            }
            else
            {
                run_btnInit.BackColor = Color.PowderBlue;
                run_btnOff.BackColor = Color.Green;
            }
        }

        void InitForm()
        {
            run_btnInit.Click += Run_btn_Click;
            run_btnAuto.Click += Run_btn_Click;
            run_btnManul.Click += Run_btn_Click;
            run_btnOff.Click += Run_btn_Click;
        }

        private void Run_btn_Click(object sender, EventArgs e)
        {
            string name = (sender as Button).Name;
            switch (name)
            {
                case "run_btnInit":
                   //InitStatus= run.getInitResult();
                    break;
                case "run_btnAuto":
                    run_BtnAuto();
                    break;
                case "run_btnManul":
                    run_BtnManul();
                    break;
                case "run_btnOff":
                    run_BtnOff();
                    break;
                   
            }
        }
        void run_BtnAuto()
        {
            //if(Run.InitStatus)
            {
                //plcModeValue[0] =(byte)EnumC.PlcModeW.AutoMode;
                //run.writePlcMode(plcModeValue);
            }
        }
        void run_BtnManul()
        {
            //if (Run.InitStatus)
            {
                //plcModeValue[0] = (byte)EnumC.PlcModeW.ManulMode;
                //run.writePlcMode(plcModeValue);
            }
        }
        void run_BtnOff()
        {
            //if (Run.InitStatus)
            {
                //plcModeValue[0] = (byte)EnumC.PlcModeW.OffMode;
                //run.writePlcMode(plcModeValue);
            }
        }

        //public void getPlcMode(int mode)
        //{
        //if (mode == (int)EnumC.Plc.Auto)
        //{
        //    run_lbPlcModev.Invoke(new Action<string>((s)=> { run_lbPlcModev.Text = s; }),EnumHelper.GetDescription(EnumC.Plc.Auto)) ;
        //    this.Invoke(new Action(() => {
        //        run_btnAuto.BackColor = Color.Green;
        //        run_btnManul.BackColor = Color.PowderBlue;
        //        run_btnOff.BackColor = Color.PowderBlue;
        //    }));

        //    TransStatusToMain("plcMode",run_lbPlcModev.Text);
        //}
        //if (mode == (int)EnumC.Plc.AutoRuning)
        //{
        //    run_lbPlcModev.Invoke(new Action<string>((s) => { run_lbPlcModev.Text = s; }), EnumHelper.GetDescription(EnumC.Plc.AutoRuning));
        //    this.Invoke(new Action(() => {
        //        run_btnAuto.BackColor = Color.Green;
        //        run_btnManul.BackColor = Color.PowderBlue;
        //        run_btnOff.BackColor = Color.PowderBlue;
        //    }));              
        //    TransStatusToMain("plcMode", run_lbPlcModev.Text);
        //}
        //if (mode == (int)EnumC.Plc.ManulNoReady)
        //{
        //    run_lbPlcModev.Invoke(new Action<string>((s) => { run_lbPlcModev.Text = s; }), EnumHelper.GetDescription(EnumC.Plc.ManulNoReady));
        //    this.Invoke(new Action(() => {
        //        run_btnAuto.BackColor = Color.PowderBlue;
        //        run_btnManul.BackColor = Color.Green;
        //        run_btnOff.BackColor = Color.PowderBlue;
        //    }));

        //    TransStatusToMain("plcMode", run_lbPlcModev.Text);
        //}
        //if (mode == (int)EnumC.Plc.ManulReady)
        //{
        //    run_lbPlcModev.Invoke(new Action<string>((s) => { run_lbPlcModev.Text = s; }), EnumHelper.GetDescription(EnumC.Plc.ManulReady));
        //    this.Invoke(new Action(() => {
        //        run_btnAuto.BackColor = Color.PowderBlue;
        //        run_btnManul.BackColor = Color.Green;
        //        run_btnOff.BackColor = Color.PowderBlue;
        //    }));              
        //    TransStatusToMain("plcMode", run_lbPlcModev.Text);
        //}
        //if (mode == (int)EnumC.Plc.OffMode)
        //{
        //    run_lbPlcModev.Invoke(new Action<string>((s)=> { run_lbPlcModev.Text = s; }), EnumHelper.GetDescription(EnumC.Plc.OffMode));
        //    this.Invoke(new Action(() => { run_btnAuto.BackColor = Color.PowderBlue; run_btnManul.BackColor = Color.PowderBlue; run_btnOff.BackColor = Color.Green; }));                             
        //    TransStatusToMain("plcMode", run_lbPlcModev.Text);
        //}
        //}

        public void getInitStatus(bool sta)
        {
            throw new NotImplementedException();
        }

        public void getProductID(string id)
        {
            throw new NotImplementedException();
        }

        public void getCabinetResult(int CabinetNum, string message)
        {
                if (this.IsHandleCreated && TestingSystem.stepEnable == false)
                {
                    if (CabinetNum == 1)
                        lb_Cabinet11_rv.Invoke(new Action<string>((s) => { lb_Cabinet11_rv.Text = message; }), message);
                    if (CabinetNum == 2)
                        lb_Cabinet21_rv.Invoke(new Action<string>((s) => { lb_Cabinet21_rv.Text = message; }), message);
                    if (CabinetNum == 3)
                        lb_Cabinet31_rv.Invoke(new Action<string>((s) => { lb_Cabinet31_rv.Text = message; }), message);
                    if (CabinetNum == 4)
                        lb_Cabinet41_rv.Invoke(new Action<string>((s) => { lb_Cabinet41_rv.Text = message; }), message);
                    if (CabinetNum == 5)
                        lb_Cabinet51_rv.Invoke(new Action<string>((s) => { lb_Cabinet51_rv.Text = message; }), message);
                    if (CabinetNum == 6)
                        lb_Cabinet61_rv.Invoke(new Action<string>((s) => { lb_Cabinet61_rv.Text = message; }), message);

                    dtFeedBin = db.DBQuery("select * from dbo.FeedBin where LayerID=88");
                    String feedBinScanDone = dtFeedBin.Rows[0]["Sort"].ToString().Trim();
                    if (feedBinScanDone == "No")
                    {
                        run_lbGramStatusv.Text = "料架取空";
                        btnFrameUpdate.BackColor = Color.Yellow;
                    }
                    if (feedBinScanDone == "Yes")
                    {
                        run_lbGramStatusv.Text = "使用中";
                        btnFrameUpdate.BackColor = Color.PowderBlue;
                    }
                }
        }

        public void getCabinetStatus(int CabinetNum, string message)
        {
            if (this.IsHandleCreated && TestingSystem.stepEnable == false)
            {
                if (CabinetNum == 1)
                    lb_Cabinet11_sv.Invoke(new Action<string>((s) => { lb_Cabinet11_sv.Text = message; }), message);
                if (CabinetNum == 2)
                    lb_Cabinet21_sv.Invoke(new Action<string>((s) => { lb_Cabinet21_sv.Text = message; }), message);
                if (CabinetNum == 3)
                    lb_Cabinet31_sv.Invoke(new Action<string>((s) => { lb_Cabinet31_sv.Text = message; }), message);
                if (CabinetNum == 4)
                    lb_Cabinet41_sv.Invoke(new Action<string>((s) => { lb_Cabinet41_sv.Text = message; }), message);
                if (CabinetNum == 5)
                    lb_Cabinet51_sv.Invoke(new Action<string>((s) => { lb_Cabinet51_sv.Text = message; }), message);
                if (CabinetNum == 6)
                    lb_Cabinet61_sv.Invoke(new Action<string>((s) => { lb_Cabinet61_sv.Text = message; }), message);
            }
        }

        public void getGrabNO(int grabNum)
        {
            throw new NotImplementedException();
        }

        public void transMessage(string message)
        {
            try
            {
                // TransMessageToMain(message);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
            }
        }

        public void getGrab(string[] str)
        {
            for(int i=0;i<str.Length;i++)
            {
                grab[i].Text = str[i];
            }
        }

        public void getStatus(bool[] bl)
        {
            for (int i = 0; i < bl.Length; i++)
            {
                mode[i].Text = bl[i].ToString();
            }
        }

        private void run_btnRobotPause_Click(object sender, EventArgs e)
        {
            //if (run_btnRobotPause.Text == "Robot暂停")
            //{ run.writeRobot(new byte[] { 13 }); run_btnRobotPause.Text = "Robot启动"; }
            //if(run_btnRobotPause.Text == "Robot启动")
            //{ run.writeRobot(new byte[] { 14 }); run_btnRobotPause.Text = "Robot暂停"; }          
        }

        private void RunForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnFrameUpdate_Click(object sender, EventArgs e)
        {
            if (!Frame.getInstance().frameUpdate)               
            {
                if (MessageBox.Show("请确认料架更换已经完成", "确认消息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Frame.getInstance().frameUpdate = true;
                    //transMessage("确认料架更换已经完成");
                }
            }
        }

        private void onRobotStatusChanged(Robot.Status status)
        {
            lb_Robot_sv.Text = EnumHelper.GetDescription(status);
            switch (status)
            {
                case Robot.Status.Unkown:
                case Robot.Status.Alarming:
                    lb_Robot_sv.BackColor = Color.Red;
                    break;
                case Robot.Status.Closed:
                case Robot.Status.Busy:
                case Robot.Status.Initialized:
                case Robot.Status.Initializing:
                default:
                    lb_Robot_sv.BackColor = Color.Green;
                    break;
            }
            //if (mode == (int)EnumC.Robot.Fault)
            //{
            //    lb_Robot_sv.Invoke(new Action<string>((s) => { lb_Robot_sv.Text = s; }), EnumHelper.GetDescription(EnumC.Robot.Fault));
            //    TransStatusToMain("robotMode", lb_Robot_sv.Text);
            //}
            //if (mode == (int)EnumC.Robot.Freeing)
            //{
            //    lb_Robot_sv.Invoke(new Action<string>((s) => { lb_Robot_sv.Text = s; }), EnumHelper.GetDescription(EnumC.Robot.Freeing));
            //    TransStatusToMain("robotMode", lb_Robot_sv.Text);
            //}
            //if (mode == (int)EnumC.Robot.Pauseing)
            //{
            //    lb_Robot_sv.Invoke(new Action<string>((s) => { lb_Robot_sv.Text = s; }), EnumHelper.GetDescription(EnumC.Robot.Pauseing));
            //    TransStatusToMain("robotMode", lb_Robot_sv.Text);
            //}
            //if (mode == (int)EnumC.Robot.PowerOnOver)
            //{
            //    lb_Robot_sv.Invoke(new Action<string>((s) => { lb_Robot_sv.Text = s; }), EnumHelper.GetDescription(EnumC.Robot.PowerOnOver));
            //    TransStatusToMain("robotMode", lb_Robot_sv.Text);
            //}
            //if (mode == (int)EnumC.Robot.Running)
            //{
            //    lb_Robot_sv.Invoke(new Action<string>((s) => { lb_Robot_sv.Text = s; }), EnumHelper.GetDescription(EnumC.Robot.Running));
            //    TransStatusToMain("robotMode", lb_Robot_sv.Text);
            //}
        }                    
    }
}
