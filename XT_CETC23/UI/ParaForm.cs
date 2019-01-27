using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using XT_CETC23.INTransfer;
using XT_CETC23.DataCom;
using XT_CETC23.DataManager;
using XT_CETC23.Common;
using XT_CETC23.EnumC;
using XT_CETC23.Model;
using XT_CETC23.Instances;
using System.Threading;


namespace XT_CETC23.SonForm
{
    public partial class ParaForm : Form,IParaForm
    {
        CameraForm cf = new CameraForm();
        Thread th1;
        DataBase db;
        ComboBox[] cb;
        CheckBox[] chb;
        IRunForm rform;
        Run paraRun;
        Plc plc;
        UserForms.SortAdd sortAdd1;
        string[] str=new string[6];
        bool[] bl=new bool[6];
        TextBox[] textBoxCmd;
        TextBox[] textBoxData;
        Button[] btnCmd;
        Button[] btnData;
         
        public class ProductTypeItem
        {

        }

        public ParaForm(IRunForm iRunForm)
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            cb = new ComboBox[] { para_cbCabinet1, para_cbCabinet2, para_cbCabinet3, para_cbCabinet4, para_cbCabinet5, para_cbCabinet6 };
            chb = new CheckBox[] { para_chbService1, para_chbService2, para_chbService3, para_chbService4, para_chbService5, para_chbService6 };

            textBoxCmd = new TextBox[] { txtCmd1, txtCmd2, txtCmd3, txtCmd4, txtCmd5, txtCmd6 };
            textBoxData = new TextBox[] { txtSource1, txtSource2, txtSource3, txtSource4, txtSource5, txtSource6 };
            btnCmd = new Button[] { btnCmd1, btnCmd2, btnCmd3, btnCmd4, btnCmd5, btnCmd6 };
            btnData = new Button[] { btnData1, btnData2, btnData3, btnData4, btnData5, btnData6 };

            this.rform = iRunForm;
            InitData();
            sortAdd1 = new UserForms.SortAdd(this);
            plc = Plc.GetInstanse();

            db = DataBase.GetInstanse();
            DataTable dt = db.DBQuery("select * from dbo.Path");
            for (int i = 0; i < 4; i++)
            {
                if (dt.Rows[0]["CmdPathName"].ToString().Trim() == null || dt.Rows[0]["DataPathName"].ToString().Trim() == null)
                {
                    MessageBox.Show("系统所需文件目录配置信息不完整，请通过参数配置页面配置完整！");
                    return;
                }
            }
            DataBase.logPath = dt.Rows[6]["CmdPathName"].ToString().Trim();
            txtLog.Text = DataBase.logPath;
            if (!Directory.Exists(DataBase.logPath))
            {
                Directory.CreateDirectory(DataBase.logPath);
                string filePath = DataBase.logPath + @"\log.txt";
                File.Create(filePath);
            }
            else
            {
                string filePath = DataBase.logPath + @"\log.txt";
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
            }
            DataBase.targetPath = dt.Rows[7]["CmdPathName"].ToString().Trim();
            txtTarget.Text = DataBase.targetPath;
            if (!Directory.Exists(DataBase.targetPath))
            {
                Directory.CreateDirectory(DataBase.targetPath);
            }

            CabinetData.pathCabinetStatus = new string[6];
            CabinetData.pathCabinetOrder = new string[6];
            CabinetData.sourcePath = new string[6];
            for (int i = 0; i < 6;i++ )
            {
                CabinetData.pathCabinetStatus[i] = @dt.Rows[i]["CmdPathName"].ToString().Trim() + @"\发送指令.txt";
                CabinetData.pathCabinetOrder[i] =  @dt.Rows[i]["CmdPathName"].ToString().Trim() + @"\接收指令.txt";
                textBoxCmd[i].Text = dt.Rows[i]["CmdPathName"].ToString().Trim();
                CabinetData.sourcePath[i] = @dt.Rows[i]["DataPathName"].ToString().Trim();
                textBoxData[i].Text = @dt.Rows[i]["DataPathName"].ToString().Trim();

                string path = Path.GetDirectoryName(CabinetData.pathCabinetStatus[i]);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.GetDirectoryName(CabinetData.pathCabinetOrder[i]);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = CabinetData.sourcePath[i];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

            }         
        }
        private void ParaForm_Load(object sender, EventArgs e)
        {
            
        }
        void InitData()
        {
            for (int i = 0; i < Math.Min(cb.Length, DeviceCount.TestingBedCount); ++i)
            {
                int capOfProduct = TestingCabinets.getInstance(i).Type;
                cb[i].Items.Clear();
                cb[i].Items.Add("未定义");
                cb[i].Items.Add("A组件");
                cb[i].Items.Add("B组件");
                cb[i].Items.Add("2类组件");
                cb[i].Items.Add("AB组件");
                cb[i].Items.Add("C组件");
                cb[i].Items.Add("D组件");
                cb[i].Text = TestingBedCapOfProduct.sTestingBedCapOfProduct[capOfProduct].ShowName;
                str[i] = TestingBedCapOfProduct.sTestingBedCapOfProduct[capOfProduct].ProductType; ;
                chb[i].Checked = TestingCabinets.getInstance(i).Enable == TestingCabinet.ENABLE.Enable;
                bl[i] = chb[i].Checked;
            }
            rform.getGrab(str);
            rform.getStatus(bl);
        }

        private void para_btnWrite_Click(object sender, EventArgs e)
        {
            string strTmp="";
            byte[] prodType= new byte[1];
            int cabinetStatus=0;
            if (db.DBConnect())
            {
                for (int i = 0; i < cb.Length; ++i)
                {
                    try
                    {
                        int sel = cb[i].SelectedIndex;
                        if (sel == -1)
                        {
                            sel = 0;
                        }

                        strTmp = TestingBedCapOfProduct.sTestingBedCapOfProduct[sel].ProductType;
                        prodType[0] = TestingBedCapOfProduct.sTestingBedCapOfProduct[sel].PlcMode;
                        //db.DBUpdata("insert into CabinetData(number,sort,status) values('"+i+"','" + cb[i].SelectedItem.ToString() + "','" + chb[i].Checked + "')");
                        TestingCabinets.getInstance(i).Type = sel;
                        TestingCabinets.getInstance(i).Enable = chb[i].Checked ? TestingCabinet.ENABLE.Enable : TestingCabinet.ENABLE.Disable;

                        {
                            str[i] = cb[i].SelectedItem.ToString();
                            bl[i] = (bool)chb[i].Checked;
                            plc.DBWrite(PlcData.PlcWriteAddress, 21 + i, 1, prodType);
                            if (chb[i].Checked)
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
                    }
                    catch(Exception exp)
                    { }
                }
                prodType[0] = Convert.ToByte(cabinetStatus);
                plc.DBWrite(PlcData.PlcWriteAddress, 20, 1, prodType);
                rform.getGrab(str);
                rform.getStatus(bl);
            }
        }

        private void para_btnAdd_Click(object sender, EventArgs e)
        {
            if (Common.Account.user == "admin")
            {
                if (!sortAdd1.IsDisposed) { sortAdd1.Show(); }
                else { UserForms.SortAdd sortAdd2 = new UserForms.SortAdd(this);sortAdd2.Show(); }
            }
            else
            {
                MessageBox.Show("当前用户无此权限");
            }
        }

        public void getSort(string str)
        {
            for (int i = 0; i < cb.Length; ++i)
            {
                //db.DBUpdata("insert into CabinetData(number,sort,status) values('"+i+"','" + cb[i].SelectedItem.ToString() + "','" + chb[i].Checked + "')");
                cb[i].Items.Add(str);
            }
        }
        //机器人上电
        private void para_btnRobotPowerOn_Click(object sender, EventArgs e)
        {
            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeRobot,PlcData._writeLength1, new byte[] { 11 });
        }
        //回主程序
        private void para_btnRobotRun_Click(object sender, EventArgs e)
        {
            if(PlcData._robotStatus==12)
            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeRobot, PlcData._writeLength1, new byte[] { 12 });
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbLog = new FolderBrowserDialog();
            if (fbLog.ShowDialog() ==DialogResult.OK)
            {
                txtLog.Text = fbLog.SelectedPath;               
            }
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbLog = new FolderBrowserDialog();
            if (fbLog.ShowDialog() == DialogResult.OK)
            {
                txtCmd1.Text = fbLog.SelectedPath;
            }
        }

        private void btnSorce_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbLog = new FolderBrowserDialog();
            if (fbLog.ShowDialog() == DialogResult.OK)
            {
                txtSource1.Text = fbLog.SelectedPath;
            }
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbLog = new FolderBrowserDialog();
            if (fbLog.ShowDialog() == DialogResult.OK)
            {
                txtTarget.Text = fbLog.SelectedPath;
            }
        }

        private void btnLogSave_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCmdSave_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSourceSave_Click(object sender, EventArgs e)
        {
    
        }

        private void btnTargetSave_Click(object sender, EventArgs e)
        {
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd1.Text.Trim() + "'where PathID=" + 1);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource1.Text.Trim() + "'where PathID=" + 1);
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd2.Text.Trim() + "'where PathID=" + 2);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource2.Text.Trim() + "'where PathID=" + 2);
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd3.Text.Trim() + "'where PathID=" + 3);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource3.Text.Trim() + "'where PathID=" + 3);
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd4.Text.Trim() + "'where PathID=" + 4);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource4.Text.Trim() + "'where PathID=" + 4);
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd5.Text.Trim() + "'where PathID=" + 5);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource5.Text.Trim() + "'where PathID=" + 5);
            db.DBUpdate("update dbo.Path set CmdPathName='" + txtCmd6.Text.Trim() + "'where PathID=" + 6);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtSource6.Text.Trim() + "'where PathID=" + 6);

            db.DBUpdate("update dbo.Path set CmdPathName='" + txtLog.Text.Trim() + "'where PathID=" + 7);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtLog.Text.Trim() + "'where PathID=" + 7);
            DataBase.logPath = txtLog.Text.Trim();
            if (!Directory.Exists(DataBase.logPath))
            {
                Directory.CreateDirectory(DataBase.logPath);
            }

            db.DBUpdate("update dbo.Path set CmdPathName='" + txtTarget.Text.Trim() + "'where PathID=" + 8);
            db.DBUpdate("update dbo.Path set DataPathName='" + txtTarget.Text.Trim() + "'where PathID=" + 8);
            DataBase.targetPath = txtTarget.Text.Trim();
            if (!Directory.Exists(DataBase.targetPath))
            {
                Directory.CreateDirectory(DataBase.targetPath);
            }

            DataTable dt = db.DBQuery("select * from dbo.Path");
            for (int i = 0; i < 6; i++)
            {
                CabinetData.pathCabinetStatus[i] = @dt.Rows[i]["CmdPathName"].ToString().Trim() + @"\发送指令.txt";
                CabinetData.pathCabinetOrder[i] = @dt.Rows[i]["CmdPathName"].ToString().Trim() + @"\接收指令.txt";
                textBoxCmd[i].Text = dt.Rows[i]["CmdPathName"].ToString().Trim();
                CabinetData.sourcePath[i] = @dt.Rows[i]["DataPathName"].ToString().Trim();
                textBoxData[i].Text = @dt.Rows[i]["DataPathName"].ToString().Trim();
            }         
        }
    }
}
