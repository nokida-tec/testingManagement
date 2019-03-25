using MySkins.Entity;
using MySkins.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.INTransfer;
using XT_CETC23.SonForm;
using XT_CETC23.DataCom;
using XT_CETC23.UserForms;
using XT_CETC23.DataManager;
using XT_CETC23.Common;
using XT_CETC23.Model;
using System.IO;
using System.Threading;
using System.Collections;

namespace XT_CETC23
{
    public partial class MainForm : Form, IMainForm
    {
        AutoForm aForm;
        public static CameraForm cForm;
        DataForm dForm;
        TestingResultForm resultForm;
        ManulForm mForm;
        ParaForm pForm;
        RunForm rForm;
        UserForm uForm;
        StepForm sForm;
        ParamForm paramForm;
        DataBase db;

        public MainForm()
        {
            db = DataBase.GetInstanse();
            InitializeComponent();
            InitForm();
            InitFormEvent();

            aForm = new AutoForm();
            cForm = new CameraForm();
            resultForm = new TestingResultForm();
            dForm = new DataForm();
            mForm = new ManulForm();
            pForm = new ParaForm();
            uForm = new UserForm();
            sForm = new StepForm(mForm, cForm);
            paramForm = new ParamForm();
                       
            uForm.GetAcount += UForm_GetAcount;
            mForm.clearTask();
        }

        private void UForm_GetAcount(string[] str)
        {
            User.Text = str[0].ToString();
            Power.Text = str[1].ToString();
            Common.Account.user= str[0].ToString();
            Common.Account.power= str[1].ToString();
            //listBox_Alarm.Items.Add(str[0] + " " + DateTime.Now.ToString("G")+" "+"登录成功");
            //log(str[0] + " " + "登录成功");
            pB_manul.Enabled = true;
            pB_run.Enabled = true;
 //           pB_auto.Enabled = true;
            pB_ccd.Enabled = true;
            pB_database.Enabled = true;
            pB_para.Enabled = true;
        }
        private void ShowLog(string message)
        {
            if(listBox_Alarm.InvokeRequired)
            {
                //listBox_Alarm.Invoke(new Action<string>((s) => { listBox_Alarm.Items.Add(s); }), message + " " + DateTime.Now.ToString("G"));
                listBox_Alarm.Invoke(new Action<string>((s) => { ShowLog(s); }), message);
            }
            else
            {
                listBox_Alarm.Items.Add(message);
            }
        }
        void InitFormEvent()
        {
 //           pB_auto.MouseUp += PB_MouseUp;
            pB_ccd.MouseUp += PB_MouseUp;
            pB_database.MouseUp += PB_MouseUp;
            pB_manul.MouseUp += PB_MouseUp;
            pB_para.MouseUp += PB_MouseUp;
            pB_run.MouseUp += PB_MouseUp;
            pB_user.MouseUp += PB_MouseUp;
            pB_step.MouseUp += PB_MouseUp;
            pB_Param.MouseUp += PB_MouseUp;

//            pB_auto.MouseDown += PB_MouseDown;
            pB_ccd.MouseDown += PB_MouseDown;
            pB_database.MouseDown += PB_MouseDown;
            pB_manul.MouseDown += PB_MouseDown;
            pB_para.MouseDown += PB_MouseDown;
            pB_run.MouseDown += PB_MouseDown;
            pB_user.MouseDown += PB_MouseDown;
            pB_step.MouseDown += PB_MouseDown;
            pB_Param.MouseDown += PB_MouseDown;
        }

        private void PB_MouseDown(object sender, MouseEventArgs e)
        {
            string name = (sender as PictureBox).Name;
            switch (name)
            {
                case "pB_auto":
 //                   pB_auto.BorderStyle = BorderStyle.Fixed3D;
                    pbAuto();
                    break;
                case "pB_run":
                    pB_run.BorderStyle = BorderStyle.Fixed3D;
                    pbRun();
                    break;
                case "pB_ccd":
                    pB_ccd.BorderStyle = BorderStyle.Fixed3D;
                    pbCamera();
                    break;
                case "pB_database":
                    pB_database.BorderStyle = BorderStyle.Fixed3D;
                    //pbDataBase();
                    pbReport();
                    break;
                case "pB_manul":
                    pB_manul.BorderStyle = BorderStyle.Fixed3D;
                    pbManul();
                    break;
                case "pB_para":
                    pB_para.BorderStyle = BorderStyle.Fixed3D;
                    pbPara();
                    break;
                case "pB_user":
                    pB_user.BorderStyle = BorderStyle.Fixed3D;
                    pbUser();
                    break;
                case "pB_step":
                    pB_step.BorderStyle = BorderStyle.Fixed3D;
                    pbStep();
                    break;
                case "pB_Param":
                    pB_Param.BorderStyle = BorderStyle.Fixed3D;
                    loadForm(paramForm);
                    break;
            }
        }
        private void PB_MouseUp(object sender, MouseEventArgs e)
        {
            string name = (sender as PictureBox).Name;
            switch (name)
            {
                case "pB_auto":
//                    pB_auto.BorderStyle = BorderStyle.None;
                    break;
                case "pB_run":
                    pB_run.BorderStyle = BorderStyle.None;
                    break;
                case "pB_ccd":
                    pB_ccd.BorderStyle = BorderStyle.None;
                    break;
                case "pB_database":
                    pB_database.BorderStyle = BorderStyle.None;
                    break;
                case "pB_manul":
                    pB_manul.BorderStyle = BorderStyle.None;
                    break;
                case "pB_para":
                    pB_para.BorderStyle = BorderStyle.None;
                    break;
                case "pB_user":
                    pB_user.BorderStyle = BorderStyle.None;
                    break;
                case "pB_step":
                    pB_step.BorderStyle = BorderStyle.None;
                    break;
                case "pB_Param":
                    pB_Param.BorderStyle = BorderStyle.None;
                    break;
            }
        }

        void pbStep()
        {
            if (Config.Config.ENABLED_DEBUG == true || TestingSystem.Mode.Auto == TestingSystem.GetInstance().mode)
            {
                if (!sForm.IsDisposed)
                {
                    TestingSystem.stepEnable = true;
                    //Run.readyForStep = false;

                    mForm.clearTask();
                    panel_Load.Controls.Clear();
                    sForm.TopLevel = false;
                    sForm.Dock = DockStyle.Fill;
                    panel_Load.Controls.Add(sForm);
                    sForm.Show();
                }
            }
            else
            {
                pB_step.BorderStyle = BorderStyle.None;
                MessageBox.Show("单步操作只能在自动模式下进行！", "Information");
            }
        }   

        void pbAuto()
        {
            if (!aForm.IsDisposed)
            {
                //if (TestingSystem.stepEnable == true)
                //{
                //    mForm.clearTask();
                //    TestingSystem.stepEnable = false;
                //    TestingSystem.readyForStep = false;
                //}
                panel_Load.Controls.Clear();
                aForm.TopLevel = false;
                aForm.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(aForm);                
                aForm.Show();
            }         
        }
        void pbCamera()
        {
            if (!cForm.IsDisposed)
            {
                //if (TestingSystem.stepEnable == true)
                //{
                //    mForm.clearTask();
                //    TestingSystem.stepEnable = false;
                //    TestingSystem.readyForStep = false;
                //}
                panel_Load.Controls.Clear();
                cForm.TopLevel = false;
                cForm.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(cForm);                
                cForm.Show();
            }
        }
        void pbDataBase()
        {
            if (!dForm.IsDisposed)
            {
                //if (TestingSystem.stepEnable == true)
                //{
                //    mForm.clearTask();
                //    Run.stepEnable = false;
                //    Run.readyForStep = false;
                //}
                panel_Load.Controls.Clear();
                dForm.TopLevel = false;
                dForm.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(dForm);
                dForm.Show();
            }
        }

        void pbReport()
        {
            if (!dForm.IsDisposed)
            {
                //if (Run.stepEnable == true)
                //{
                //    mForm.clearTask();
                //    Run.stepEnable = false;
                //    Run.readyForStep = false;
                //}
                panel_Load.Controls.Clear();
                resultForm.TopLevel = false;
                resultForm.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(resultForm);
                resultForm.Show();
            }
        }
        void pbManul()
        {
            if (TestingSystem.GetInstance().mode == TestingSystem.Mode.Manual)
            {
                //if (Common.Account.power == "system")
                if (true)
                {
                    if (!mForm.IsDisposed)
                    {
                        TestingSystem.stepEnable = false;
                        TestingSystem.readyForStep = false;
                        mForm.clearTask();
                        panel_Load.Controls.Clear();
                        mForm.TopLevel = false;
                        mForm.Dock = DockStyle.Fill;
                        panel_Load.Controls.Add(mForm);
                        mForm.Show();
                    }
                }
                else
                {
                    mForm.clearTask();
                    TestingSystem.stepEnable = false;
                    TestingSystem.readyForStep = false;
                    //MessageBox.Show("当前用户无此权限");
                    listBox_Alarm.Items.Add("当前用户无此权限");
                }
            }
            else
            {
                pB_manul.BorderStyle= BorderStyle.None;
                MessageBox.Show("手动操作只能在手动模式下进行！", "Information");
            }
        }
        void pbPara()
        {
            if (TestingSystem.GetInstance().mode != TestingSystem.Mode.Auto)
            {
                //if (Common.Account.power != "system") { MessageBox.Show("当前用户无此权限"); return; }
                if (!pForm.IsDisposed)
                {
                    //if (TestingSystem.stepEnable == true)
                    //{
                    //    mForm.clearTask();
                    //    TestingSystem.stepEnable = false;
                    //    TestingSystem.readyForStep = false;
                    //}
                    panel_Load.Controls.Clear();
                    pForm.TopLevel = false;
                    pForm.Dock = DockStyle.Fill;
                    panel_Load.Controls.Add(pForm);
                    pForm.Show();
                }
            }
            else
            {
                pB_para.BorderStyle = BorderStyle.None;
                MessageBox.Show("参数配置不能在自动模式下进行！", "Information");
            }
        }

        void pbRun()
        {
            if (rForm == null)
            {
                rForm = new RunForm();
            }

            if (!rForm.IsDisposed)
            {
                if (TestingSystem.stepEnable == true)
                {
                    pB_run.BorderStyle = BorderStyle.None;
                    if (MessageBox.Show("请确认单步动作已经执行完成!", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel) 
                    {
                        return;
                    }
                    mForm.clearTask();
                    TestingSystem.stepEnable = false;
                    TestingSystem.readyForStep = false;
                }
                panel_Load.Controls.Clear();
                rForm.TopLevel = false;
                rForm.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(rForm);
                rForm.Show();
            }
        }
        void pbUser()
        {
            loadForm(uForm);
            if (!uForm.IsDisposed)
            {
                //if (TestingSystem.stepEnable == true)
                //{
                //    mForm.clearTask();
                //    TestingSystem.stepEnable = false;
                //    TestingSystem.readyForStep = false;
                //}
                //panel_Load.Controls.Clear();
                //uForm.TopLevel = false;
                //uForm.Dock = DockStyle.Fill;
                //panel_Load.Controls.Add(uForm);
                //uForm.Show();
            }
        }
        void loadForm(Form form)
        {
            //if (Common.Account.power != "system") { MessageBox.Show("当前用户无此权限"); return; }
            if (!form.IsDisposed)
            {
                panel_Load.Controls.Clear();
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                panel_Load.Controls.Add(form);
//                TestingSystem.stepEnable = false;
                form.Show();
            }
        }

        #region 
        public const String IMG_MIN = "btn_min";
        public const String IMG_MAX = "btn_max";
        public const String IMG_RESTORE = "btn_restore";
        public const String IMG_CLOSE = "btn_close";
        public const String IMG_BG = "img_bg";

        private Bitmap closeBmp = null;
        private Bitmap minBmp = null;
        private Bitmap maxBmp = null;
        private Bitmap restoreBmp = null;
        private void InitForm()
        {
            this.Size = new Size(1220, 680);
            MainForm_Resize(null, null);
            BtnMin.Left = this.Width - 95;
            BtnMax.Left = this.Width - 65;
            BtnClose.Left = this.Width - 35;
            this.minBmp = ResUtils.GetResAsImage(IMG_MIN);
            this.maxBmp = ResUtils.GetResAsImage(IMG_MAX);
            this.closeBmp = ResUtils.GetResAsImage(IMG_CLOSE);
            this.restoreBmp = ResUtils.GetResAsImage(IMG_RESTORE);
            BtnClose.MouseClick += BtnWnd_MouseClick;
            BtnMax.MouseClick += BtnWnd_MouseClick;
            BtnMin.MouseClick += BtnWnd_MouseClick;

            BtnClose.MouseEnter += BtnWnd_MouseEnter;
            BtnMax.MouseEnter += BtnWnd_MouseEnter;
            BtnMin.MouseEnter += BtnWnd_MouseEnter;

            BtnClose.MouseLeave += BtnWnd_MouseLeave;
            BtnMax.MouseLeave += BtnWnd_MouseLeave;
            BtnMin.MouseLeave += BtnWnd_MouseLeave;

            BtnClose.MouseDown += BtnWnd_MouseDown;
            BtnMax.MouseDown += BtnWnd_MouseDown;
            BtnMin.MouseDown += BtnWnd_MouseDown;

            BtnClose.MouseUp += BtnWnd_MouseUp;
            BtnMax.MouseUp += BtnWnd_MouseUp;
            BtnMin.MouseUp += BtnWnd_MouseUp;
            this.toolTip1.SetToolTip(this.BtnClose, "关闭");
            this.toolTip1.SetToolTip(this.BtnMin, "最小化");
            this.toolTip1.SetToolTip(this.BtnMax, "最大化");
            //tss_lb_plcStatus.Text = "    系统未连接";
            tss_lb_net.Font = new Font("微软雅黑", 9);
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.toolTip1.SetToolTip(this.BtnMax, "还原");
                this.BtnMax.BackgroundImage = this.restoreBmp;
                this.BtnMax.Invalidate();
            }
        }
        private void BtnWnd_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender == this.BtnClose)
                {
                    this.Close();
                }
                else if (sender == this.BtnMax)
                {
                    //if (this.WindowState == FormWindowState.Normal)
                    //{
                    //    this.WindowState = FormWindowState.Maximized;
                    //    this.BtnMax.BackgroundImage = this.restoreBmp;
                    //    this.toolTip1.SetToolTip(this.BtnMax, "还原");
                    //    this.BtnMax.Invalidate();
                    //}
                    //else
                    //{
                    //    this.WindowState = FormWindowState.Normal;
                    //    this.BtnMax.BackgroundImage = this.maxBmp;
                    //    this.toolTip1.SetToolTip(this.BtnMax, "最大化");
                    //}
                }
                else if (sender == this.BtnMin)
                {
                    if (!this.ShowInTaskbar)
                    {
                        this.Hide();
                    }
                    else
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
            }
        }
        private void BtnWnd_MouseEnter(object sender, EventArgs e)
        {
            Bitmap backImage = null;
            if (sender == this.BtnClose)
            {
                backImage = ResUtils.GetResWithState(IMG_CLOSE, ControlState.MouseOver);
            }
            else if (sender == this.BtnMin)
            {
                backImage = ResUtils.GetResWithState(IMG_MIN, ControlState.MouseOver);
            }
            else if (sender == this.BtnMax)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    backImage = ResUtils.GetResWithState(IMG_MAX, ControlState.MouseOver);
                }
                else
                {
                    backImage = ResUtils.GetResWithState(IMG_RESTORE, ControlState.MouseOver);
                }
            }
            else
            {
                return;
            }
            Control control = (Control)sender;
            control.BackgroundImage = backImage;
            control.Invalidate();
        }
        private void BtnWnd_MouseLeave(object sender, EventArgs e)
        {
            Bitmap backImage = null;
            if (sender == this.BtnClose)
            {
                backImage = closeBmp;
            }
            else if (sender == this.BtnMin)
            {
                backImage = minBmp;
            }
            else if (sender == this.BtnMax)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    backImage = maxBmp;
                }
                else
                {
                    backImage = restoreBmp;
                }
            }
            else
            {
                return;
            }
            Control control = (Control)sender;
            control.BackgroundImage = backImage;
            control.Invalidate();
        }
        private void BtnWnd_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap backImage = null;
            if (sender == this.BtnClose)
            {
                backImage = ResUtils.GetResWithState(IMG_CLOSE, ControlState.MouseDown);
            }
            else if (sender == this.BtnMin)
            {
                backImage = ResUtils.GetResWithState(IMG_MIN, ControlState.MouseDown);
            }
            else if (sender == this.BtnMax)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    backImage = MySkins.Util.ResUtils.GetResWithState(IMG_MAX, ControlState.MouseDown);
                }
                else
                {
                    backImage = MySkins.Util.ResUtils.GetResWithState(IMG_RESTORE, ControlState.MouseDown);
                }
            }
            else
            {
                return;
            }
            Control control = (Control)sender;
            control.BackgroundImage = backImage;
            control.Invalidate();
        }
        private void BtnWnd_MouseUp(object sender, MouseEventArgs e)
        {
            Bitmap backImage = null;
            if (sender == this.BtnClose)
            {
                backImage = closeBmp;
            }
            else if (sender == this.BtnMin)
            {
                backImage = minBmp;
            }
            else if (sender == this.BtnMax)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    backImage = maxBmp;
                }
                else
                {
                    backImage = restoreBmp;
                }
            }
            else
            {
                return;
            }
            Control control = (Control)sender;
            control.BackgroundImage = backImage;
            control.Invalidate();
        }
        #endregion
        #region 
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            /*
            float[] scale = (float[])Tag;
            int i = 2;

            foreach (Control ctrl in this.Controls)
            {
                try
                {
                    if (ctrl is Panel)
                    {
                        ctrl.Left = (int)(Size.Width * scale[i++]);
                        ctrl.Top = (int)(Size.Height * scale[i++]);
                        ctrl.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);//!!!
                        ctrl.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);//!!!
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception e1)
                {
                    Logger.WriteLine(e1);
                }
                //每次使用的都是最初始的控件大小，保证准确无误。
            }
            panel_Status.Location = new Point(0, 170);
            panel_Status.Size = new Size(225, 615);
            panel_Status.Height = this.Height - 194;
            panel_Load.Location = new Point(224, 170);
            panel_Load.Height = this.Height - 194;
            panel_Load.Width = this.Width - panel_Status.Width;

            groupBox2.Location = new Point(8, 90);
            groupBox2.Height = panel_Status.Height - groupBox1.Height - 20;

            listBox_Alarm.Height = panel_Status.Height - groupBox1.Height - 100;

            UserName.Left = this.Width - 450;
            User.Left = this.Width - 350;
            Level.Left = this.Width - 250;
            Power.Left = this.Width - 150;
            //Company.Left = this.Width / 2 - Company.Width / 2;
            ClearSingle.Top = listBox_Alarm.Top + listBox_Alarm.Height + 10;
            ClearAll.Top = listBox_Alarm.Top + listBox_Alarm.Height + 10;
            ClearSingle.Width = 100;
            ClearSingle.Height = 40;
            ClearAll.Width = 100;
            ClearAll.Height = 40;
             * */
        }

        public string getMessageToMainForm()
        {
            throw new NotImplementedException();
        }

        private void listBox_Alarm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox_Alarm.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                MessageBox.Show(listBox_Alarm.SelectedItem.ToString());
            }
        }

        private void ClearSingle_Click(object sender, EventArgs e)
        {
            if(listBox_Alarm.SelectedIndex>-1)
            {
                listBox_Alarm.Items.Remove(listBox_Alarm.Items[listBox_Alarm.SelectedIndex]);
            }
            else
            {
                MessageBox.Show("请选择需要删除的条目");
            }
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            listBox_Alarm.Items.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        public void manulEnable(string mode,string status)
        {
            //if (mode == "Auto")
            //{
            //    pB_manul.Enabled = false;
            //    pB_para.Enabled = false;
            //    pB_step.Enabled = true;
            //}
            //if (mode == "Manul")
            //{
            //    pB_manul.Enabled = true;
            //    pB_para.Enabled = true;
            //    pB_step.Enabled = false;
            //}
            if (mode == "Manul")
            {
                labSystemStatus.Text = "手动控制";
            }
            if (mode == "Auto")
            {
                if (status == "AutoRunning")
                {
                    labSystemStatus.Text = "自动运行";
                }
                else if (status == "Pausing" && status != "Alarming")
                {
                    labSystemStatus.Text = "暂停中";
                }
                else if (status == "Initalized")
                {
                    labSystemStatus.Text = "初始化完成";
                }
                else if (status == "Pausing" && status == "Alarming")
                {
                    labSystemStatus.Text = "告警中";
                }
                else
                {
                    labSystemStatus.Text = "停止/未初始化";
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Logger.getInstance().RegistryDelegate(ShowLog);

            // 注册显示函数
            TestingSystem.GetInstance().RegistryDelegate(onModeChanged);
            TestingSystem.GetInstance().RegistryDelegate(onInitializeChanged);
            TestingSystem.GetInstance().RegistryDelegate(onStatusChanged);

            pbRun();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //TestingCabinets.getInstance(0).cmdStart("C", 9999);
            Frame.getInstance().excuteCommand(Frame.Lock.Command.Open);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
           // TestingCabinets.getInstance(0).cmdStop();
            Frame.getInstance().excuteCommand(Frame.Lock.Command.Close);
        }

        private void onModeChanged(TestingSystem.Mode mode)
        {
            //labSystemStatus.Text = EnumHelper.GetDescription(mode);
        }

        private void onInitializeChanged(TestingSystem.Initialize initialize)
        {
            labSystemStatus.Text = EnumHelper.GetDescription(initialize);
        }

        private void onStatusChanged(TestingSystem.Mode mode, TestingSystem.Status status)
        {
            labSystemStatus.Text = EnumHelper.GetDescription(status);
        }
    }
}