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
using System.Threading;
namespace XT_CETC23.SonForm
{
    public partial class ParaForm : Form,IParaForm
    {
        CameraForm cf = new CameraForm();
        Thread th1;
        DataBase db;
        DataTable dt;
        ComboBox[] cb;
        CheckBox[] chb;
        IRunForm rform;
        Run paraRun;
        Plc plc;
        UserForms.SortAdd sortAdd1;
        string[] str=new string[6];
        bool[] bl=new bool[6];
        public ParaForm(IRunForm iRunForm)
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            cb = new ComboBox[] { para_cbCabinet1, para_cbCabinet2, para_cbCabinet3, para_cbCabinet4, para_cbCabinet5, para_cbCabinet6 };
            chb = new CheckBox[] { para_chbService1, para_chbService2, para_chbService3, para_chbService4, para_chbService5, para_chbService6 };
            this.rform = iRunForm;
            InitData();
            sortAdd1 = new UserForms.SortAdd(this);
            plc = Plc.GetInstanse();

            db = DataBase.GetInstanse();
            dt = db.DBQuery("select * from dbo.Path");
            for (int i = 0; i < 4; i++)
            {
                if (dt.Rows[0]["pathName"].ToString().Trim() == null)
                {
                    MessageBox.Show("系统所需文件目录配置信息不完整，请通过参数配置页面配置完整！");
                    return;
                }
            }
            DataBase.logPath = dt.Rows[0]["pathName"].ToString().Trim();
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
            DataBase.cmdPath = dt.Rows[1]["pathName"].ToString().Trim();
            txtCmd.Text = DataBase.cmdPath;
            string cPath = DataBase.cmdPath + @"\Cabinet";
            if (!Directory.Exists(cPath))
            {
                Directory.CreateDirectory(DataBase.cmdPath);
                string tmpPath = cPath + @"\cabinet1\";
                Directory.CreateDirectory(tmpPath);
                string filePath = tmpPath + "status.txt";
                CabinetData.pathRead1 = filePath;
                File.Create(filePath);               
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite1 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet2\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead2 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite2 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet3\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead3 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite3 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet4\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead4 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite4 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet5\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead5 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite5 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet6\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead6 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite6 = filePath;
                File.Create(filePath);
            }            
            DataBase.sourcePath = dt.Rows[2]["pathName"].ToString().Trim();
            txtSource.Text = DataBase.sourcePath;
            if (!Directory.Exists(DataBase.sourcePath))
            {
                Directory.CreateDirectory(DataBase.sourcePath);
            }
            DataBase.targetPath = dt.Rows[3]["pathName"].ToString().Trim();
            txtTarget.Text = DataBase.targetPath;
            if (!Directory.Exists(DataBase.targetPath))
            {
                Directory.CreateDirectory(DataBase.targetPath);
            }
        }
        private void ParaForm_Load(object sender, EventArgs e)
        {
            
        }
        void InitData()
        {
            if(db.DBConnect())
            {
                dt = db.DBQuery(DBstr.QueryStr("CabinetData"));
                if(dt.Rows.Count>1)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        cb[i].Text = dt.Rows[i]["sort"].ToString().Trim();
                        str[i]= dt.Rows[i]["sort"].ToString().Trim();
                        //bool tmpBool= (bool)dt.Rows[i]["status"];
                        bool status = (int)Convert.ToDouble(dt.Rows[i]["status"]) != 0;
                        chb[i].Checked = status;
                        bl[i] = status;
                    }
                    rform.getGrab(str);
                    rform.getStatus(bl);
                }               
            }
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
                        switch (cb[i].SelectedItem.ToString().Trim())
                        {
                            case "A组件":
                                strTmp = "A";
                                prodType[0] = 1;
                                break;
                            case "B组件":
                                strTmp = "B";
                                prodType[0] = 2;
                                break;
                            case "2类组件":
                                strTmp = "E";
                                prodType[0] = 5;
                                break;
                            case "AB组件":
                                strTmp = "F";
                                prodType[0] = 6;
                                break;
                            case "C组件":
                                strTmp = "C";
                                prodType[0] = 3;
                                break;
                            case "D组件":
                                strTmp = "D";
                                prodType[0] = 4;
                                break;
                            default:
                                strTmp = "undefine";
                                break;
                        }
                        //db.DBUpdata("insert into CabinetData(number,sort,status) values('"+i+"','" + cb[i].SelectedItem.ToString() + "','" + chb[i].Checked + "')");
                        if (db.DBUpdate("update CabinetData set sort = '" + strTmp + "',status='" + chb[i].Checked + "' where number= " + i + ""))
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
                        else
                        {
                            MessageBox.Show("写入失败");
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
                txtCmd.Text = fbLog.SelectedPath;
            }
        }

        private void btnSorce_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbLog = new FolderBrowserDialog();
            if (fbLog.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fbLog.SelectedPath;
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
            db.DBUpdate("update dbo.Path set pathName='" + txtLog.Text.Trim() + "'where PathID=" + 1);
            DataBase.logPath = txtLog.Text.Trim();
            if (!Directory.Exists(DataBase.logPath))
            {
                Directory.CreateDirectory(DataBase.logPath);
                string filePath = DataBase.logPath + "log.txt";
                File.Create(filePath);
            }
            
        }

        private void btnCmdSave_Click(object sender, EventArgs e)
        {
            db.DBUpdate("update dbo.Path set pathName='" + txtCmd.Text.Trim() + "'where PathID=" + 2);
            DataBase.cmdPath = txtCmd.Text.Trim();
            string cPath=DataBase.cmdPath+@"\Cabinet";
            if (!Directory.Exists(cPath))
            {
                Directory.CreateDirectory(DataBase.cmdPath);
                string tmpPath = cPath + @"\cabinet1\";
                Directory.CreateDirectory(tmpPath);
                string filePath = tmpPath + "status.txt";
                CabinetData.pathRead1 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite1 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet2\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead2 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite2 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet3\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead3 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite3 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet4\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead4 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite4 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet5\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead5 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite5 = filePath;
                File.Create(filePath);
                tmpPath = cPath + @"\cabinet6\";
                Directory.CreateDirectory(tmpPath);
                filePath = tmpPath + "status.txt";
                CabinetData.pathRead6 = filePath;
                File.Create(filePath);
                filePath = tmpPath + "order.txt";
                CabinetData.pathWrite6 = filePath;
                File.Create(filePath);
            }                       
        }

        private void btnSourceSave_Click(object sender, EventArgs e)
        {
            db.DBUpdate("update dbo.Path set pathName='" + txtSource.Text.Trim() + "'where PathID=" + 3);
            DataBase.sourcePath = txtSource.Text.Trim();
            if (!Directory.Exists(DataBase.sourcePath))
            {
                Directory.CreateDirectory(DataBase.sourcePath);
            }           
        }

        private void btnTargetSave_Click(object sender, EventArgs e)
        {
            db.DBUpdate("update dbo.Path set pathName='" + txtTarget.Text.Trim() + "'where PathID=" + 4);
            DataBase.targetPath = txtTarget.Text.Trim();
            if (!Directory.Exists(DataBase.targetPath))
            {
                Directory.CreateDirectory(DataBase.targetPath);
            }
        }
    }
}
