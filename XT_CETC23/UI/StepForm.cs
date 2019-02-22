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
using XT_CETC23_GK.Task;
using System.Threading;
using XT_CETC23.Model;
using XT_CETC23.Instances;

namespace XT_CETC23.SonForm
{
    public partial class StepForm : Form
    {
        public delegate void tansMessage(string str);
        public event tansMessage TransMessage;
        Queue<string> mQueue = new System.Collections.Generic.Queue<string>();
        delegate void mCycle(Queue<string> mQueue);
        IAsyncResult result;

        mCycle MCycle;
        DataTable dt = new DataTable();
        DataBase db;
        Plc plc;
        CameraForm cFrom;
        ManulForm manulForm;

        byte[] writeByte = new byte[1];
        byte[] writeByte1 = new byte[1];
        byte[] writeByte2 = new byte[1];
        byte[] writeByte3 = new byte[1];
        string[] pos = new string[7];

        public StepForm(ManulForm mFrom, CameraForm cForm)
        {
            this.manulForm = mFrom;
            this.cFrom = cForm;
            InitializeComponent();
            MCycle = manulCycle;
            db= DataBase.GetInstanse();
            plc = Plc.GetInstanse();
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = manul_cbGoalPos.Items[i].ToString();
            }
            PlcData.AddAxlis7Pos(pos);
            //manulForm.clearTask();
        }

        private void manul_cbProductSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                dt = db.DBQuery("select * from sortdata where sortname='" + manul_cbProductSort.SelectedItem.ToString() + "'");
                if ((int)dt.Rows[0]["number"] > 1)
                {
                    //manul_cbProductNum.Enabled = true;
                    manul_cbProductNum.Items.Clear();
                    for (int i = 1; i <= (int)dt.Rows[0]["number"]; ++i)
                    {
                        manul_cbProductNum.Items.Add(i.ToString() + "号位");
                    }
                }
                else
                {
                    //manul_cbProductNum.Enabled = false;
                }   
            }
            catch (Exception e1)
            {
                Logger.WriteLine(e1);
            }
        }

        private void manul_cbProductSort_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void manul_cbGoalPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (manul_cbGoalPos.SelectedItem.ToString() == "料架位")
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                dt = db.DBQuery("select * from sortdata where sortname='" + manul_cbProductSort.SelectedItem.ToString() + "'");
                if ((int)dt.Rows[0]["number"] > 1)
                {
                    manul_cbProductNum.Enabled = true;
                }
                else
                {
                    manul_cbProductNum.Enabled = false;
                }
            }
            else
            {
                manul_cbProductNum.Enabled = false;
            }
        }

        private void manul_btnStart1_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (!ckbAxis7Alone.Checked)                 //机器人和轨道联动
                {
                    if (manul_cbProductSort.SelectedIndex > -1 && manul_cbGoalPos.SelectedIndex > -1 && manul_cbCommand.SelectedIndex > -1)
                    {
                        if (manul_cbGoalPos.SelectedItem.ToString() == "料架位")
                        {
                            if (manul_cbProductNum.Enabled)
                            {
                                if (manul_cbProductNum.SelectedIndex > -1)
                                {
                                    Thread robotTh = new Thread(RobotOp);
                                    if (!robotTh.IsAlive)
                                    {
                                        robotTh.Start();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("输入信息不全！", "Information");
                                }
                            }
                            else
                            {                               
                                Thread robotTh = new Thread(RobotOp);
                                if (!robotTh.IsAlive)
                                {
                                    robotTh.Start();
                                }                                
                            }
                        }
                        else
                        {
                            Thread robotTh = new Thread(RobotOp);
                            if (!robotTh.IsAlive)
                            {
                                robotTh.Start();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("输入信息不全！", "Information");
                    }
                }
                else                        //轨道独立运动
                {
                    if(manul_cbGoalPos.SelectedIndex > -1)
                    {
                        Thread robotTh = new Thread(RobotOp);
                        if (!robotTh.IsAlive)
                        {
                            robotTh.Start();
                        }
                    }
                    else
                    {
                        MessageBox.Show("输入信息不全！", "Information");
                    }
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }            
        }

        private void RobotOp()
        {            
            Robot robot=Robot.GetInstanse();
            DataTable dt = new DataTable();
            //if (Common.Account.power == "system" || Common.Account.power == "operator")
            //{

            if (!ckbAxis7Alone.Checked)
            {
                #region 机器人和轨道联动
                
                int Axlis7Pos = PlcData.getAxlis7Pos(manul_cbGoalPos.SelectedItem.ToString());
                string order = "";
                if (manul_cbCommand.SelectedItem.ToString() == "取料" && manul_cbGoalPos.SelectedItem.ToString() == "料架位")
                    order = "GetProTray";
                if (manul_cbCommand.SelectedItem.ToString() == "放料" && manul_cbGoalPos.SelectedItem.ToString() == "料架位")
                    order = "PutProTray";
                if (manul_cbCommand.SelectedItem.ToString() == "取料" && manul_cbGoalPos.SelectedItem.ToString() != "料架位")
                    order = "GetProTest";
                if (manul_cbCommand.SelectedItem.ToString() == "放料" && manul_cbGoalPos.SelectedItem.ToString() != "料架位")
                    order = "PutProTest";
                string prodType = manul_cbProductSort.SelectedItem.ToString();
                String rspMsg = order + "Done";

                //判断机器人是否在原点
                //机器人轨道移动机器人到动作位置
                TaskCycle.actionType = "FrameToCabinet";
                TaskCycle.PickStep = 0;


                db.DBUpdate("insert into dbo.TaskAxlis7(Axlis7Pos)values(" + PlcData.getAxlis7Pos(manul_cbGoalPos.SelectedItem.ToString()) + ")");
                
                //等待机器人轨道到位
                do
                {
                    Thread.Sleep(100);
                } while (TaskCycle.PickStep != 10); 
                TaskCycle.PickStep = 0;

                //根据位置和命令类型选择不同的操作
                if (manul_cbGoalPos.SelectedItem.ToString() == "料架位")
                {                        
                    #region 料架位
                    int pieceNo = 1;
                    
                    if (manul_cbProductNum.Enabled)
                    {
                        pieceNo = manul_cbProductNum.SelectedIndex + 1;
                    }                                            
                        
                                            
                    if (order == "GetProTray")  //机器人取料
                    {
                        //拍照
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

                        int shootTimes = 0;
                    ShootAgain:
                        string CordinatorX = "0";
                        string CordinatorY = "0";
                        string CordinatorU = "0";
                        cFrom.CCDTrigger(prodNumber, pieceNo);
                        shootTimes = shootTimes + 1;

                        if (cFrom.CCDDone == -1)
                        {
                            Thread.Sleep(200);
                            if (shootTimes < 4)
                            {
                                goto ShootAgain;
                            }
                            MessageBox.Show("视觉识别失败", "Information");
                            return;
                        }

                        if (prodType == "D")
                        {
                            CordinatorX = cFrom.X;
                            CordinatorY = cFrom.Y;
                        }

                        Robot.GetInstanse().doGetProductFromFrame(prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);
                    }
                    else if(order == "PutProTray")           //机器人放料
                    {
                        Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo);
                    }                                           
                    #endregion                    
                }                          
                else
                {
                    #region 测试台
                    int cabinetNo = manul_cbGoalPos.SelectedIndex;
                    Robot.GetInstanse().doPutProductToCabinet(prodType, cabinetNo);  // Todo: 是否从0开始,需要确认
                    #endregion
                }

                //等待机器人取料完成消息                    
                while (String.IsNullOrEmpty(RobotData.Response))
                {
                    Thread.Sleep(100);
                }
                while (!RobotData.Response.Trim().Equals(rspMsg))
                {
                    Thread.Sleep(100);
                }
                RobotData.Command = "";
                RobotData.Response = "";                
            }
            #endregion

            else        //轨道独立运动
            {
                TaskCycle.actionType = "FrameToCabinet";
                TaskCycle.PickStep = 0;
                //插入机器人轨道移动任务
                db.DBInsert("insert into dbo.TaskAxlis7(Axlis7Pos)values(" + (int)PlcData.getAxlis7Pos(manul_cbGoalPos.SelectedItem.ToString()) + ")");
                do
                {
                    Thread.Sleep(100);
                } while (TaskCycle.PickStep != 10);
                TaskCycle.PickStep = 0;
            }
            MessageBox.Show("操作完成！", "Information");
            //}
            //else
            //{
            //    MessageBox.Show("当前用户无此权限");
            //}                                                                 
        }

        void manulCycle(Queue<string> mQueue)
        {
            //Thread.Sleep(5000);
            if (plc.plcConnected)
            {
                string pos = mQueue.Dequeue();
                if (pos.Equals("A1"))
                    writeByte2[0] = 11;
                if (pos.Equals("A2"))
                    writeByte2[0] = 12;
                if (pos.Equals("A3"))
                    writeByte2[0] = 13;
                if (pos.Equals("A4"))
                    writeByte2[0] = 14;
                if (pos.Equals("A5"))
                    writeByte2[0] = 15;

                if (pos.Equals("B1"))
                    writeByte2[0] = 21;
                if (pos.Equals("B2"))
                    writeByte2[0] = 22;
                if (pos.Equals("B3"))
                    writeByte2[0] = 23;
                if (pos.Equals("B4"))
                    writeByte2[0] = 24;
                if (pos.Equals("B5"))
                    writeByte2[0] = 25;

                if (pos.Equals("C1"))
                    writeByte2[0] = 31;
                if (pos.Equals("C2"))
                    writeByte2[0] = 32;
                if (pos.Equals("C3"))
                    writeByte2[0] = 33;
                if (pos.Equals("C4"))
                    writeByte2[0] = 34;
                if (pos.Equals("C5"))
                    writeByte2[0] = 35;

                if (pos.Equals("D1"))
                    writeByte2[0] = 41;
                if (pos.Equals("D2"))
                    writeByte2[0] = 42;
                if (pos.Equals("D3"))
                    writeByte2[0] = 43;
                if (pos.Equals("D4"))
                    writeByte2[0] = 44;
                if (pos.Equals("D5"))
                    writeByte2[0] = 45;

                if (pos.Equals("E1"))
                    writeByte2[0] = 51;
                if (pos.Equals("E2"))
                    writeByte2[0] = 52;
                if (pos.Equals("E3"))
                    writeByte2[0] = 53;
                if (pos.Equals("E4"))
                    writeByte2[0] = 54;
                if (pos.Equals("E5"))
                    writeByte2[0] = 55;

                if (pos.Equals("F1"))
                    writeByte2[0] = 61;
                if (pos.Equals("F2"))
                    writeByte2[0] = 62;
                if (pos.Equals("F3"))
                    writeByte2[0] = 63;
                if (pos.Equals("F4"))
                    writeByte2[0] = 64;
                if (pos.Equals("F5"))
                    writeByte2[0] = 65;

                if (pos.Equals("G1"))
                    writeByte2[0] = 71;
                if (pos.Equals("G2"))
                    writeByte2[0] = 72;
                if (pos.Equals("G3"))
                    writeByte2[0] = 73;
                if (pos.Equals("G4"))
                    writeByte2[0] = 74;
                if (pos.Equals("G5"))
                    writeByte2[0] = 75;

                if (pos.Equals("H1"))
                    writeByte2[0] = 81;
                if (pos.Equals("H2"))
                    writeByte2[0] = 82;
                if (pos.Equals("H3"))
                    writeByte2[0] = 83;
                if (pos.Equals("H4"))
                    writeByte2[0] = 84;
                if (pos.Equals("H5"))
                    writeByte2[0] = 85;
                string order = mQueue.Dequeue();
                if (order.Equals("取料"))
                {
                    //if (MaterielData.FrameHavePiece)
                    //{ MessageBox.Show("货架区有料");return; }
                    db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.GetPiece + "," + (int)writeByte2[0] + ")");
                }
                else if (order.Equals("放料"))
                {
                    //if (!MaterielData.FrameHavePiece)
                    //{ MessageBox.Show("货架区无料"); return; }
                    db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + (int)writeByte2[0] + ")");
                }
                else
                    mQueue.Clear();
            }
            else
            {
                return;
            }
        }

        private void manul_btnStart2_Click(object sender, EventArgs e)
        {         
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                String layerNo = manul_cbGoalPos2.SelectedItem.ToString();
                String commandNo = manul_cbCommand2.SelectedItem.ToString();
                InsertPickTtay(layerNo, commandNo);
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
            
        }

        private void InsertPickTtay(String layer, String command)
        {
            using (dt = new DataTable())
            {
                dt = db.DBQuery("select * from dbo.TaskAxlis2");
                //设备只能有一条实时任务
                if (!(dt.Rows.Count > 0))
                    if (plc.plcConnected)
                    {
                        //if (Common.Account.power == "system" || Common.Account.power == "operator")
                        //{
                        if (!String.IsNullOrEmpty(layer) && !String.IsNullOrEmpty(command))
                        {
                            //插入任务，进行排队操作
                            //if (!((int)PlcData._axlis2Status == (int)EnumC.Frame.Home))
                            //{ MessageBox.Show("取料机构不在初始位"); return; }
                            mQueue.Enqueue(layer);
                            mQueue.Enqueue(command);
                            result = MCycle.BeginInvoke(mQueue, null, null);
                        }
                        else { MessageBox.Show("非法操作,信息不全"); }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("当前用户无此权限");
                        //}
                    }
                    else
                    { MessageBox.Show("PLC未连接"); }
                else { MessageBox.Show("当前任务未完成"); }
                dt.Dispose();
            }
        }

        //插入扫码任务
        private void manul_btnStartScan_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                InsertScan();
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
        }

        public void InsertScan()
        {
            using (dt = new DataTable())
            {
                dt = db.DBQuery("select * from dbo.TaskAxlis2");
                //设备只能有一条实时任务
                if (!(dt.Rows.Count > 0))
                    if (plc.plcConnected)
                    {
                        //if (Common.Account.power == "system" || Common.Account.power == "operator")
                        //{
                        string tmpText = "insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.ScanSort + ",0)";
                        db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.ScanSort + ",0)");
                        //if (db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.ScanSort + ",0)"))
                        //TransMessage("扫码任务手动插入成功");
                        //else
                        //TransMessage("扫码任务手动插入失败");
                        //}
                        //else
                        //{ MessageBox.Show("当前用户无此权限"); }
                    }
                    else
                    { MessageBox.Show("PLC未连接"); }
                else { MessageBox.Show("当前任务未完成"); }
                dt.Dispose();
            }
        }

        private void manul_btnStopT_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (manul_cbCabineit.SelectedIndex > -1)
                {
                    String prodType = manul_cbCabineitType.SelectedItem.ToString();
                    int cabinetNo = manul_cbCabineit.SelectedIndex;
                    InsertTest("Stop", prodType, cabinetNo);
                }
                else
                {
                    MessageBox.Show("请先选择设备");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }           
        }

        private void manul_btnStartT_Click(object sender, EventArgs e)
        {            
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (manul_cbCabineit.SelectedIndex > -1)
                {
                    String prodType = manul_cbCabineitType.SelectedItem.ToString();
                    int cabinetNo = manul_cbCabineit.SelectedIndex;
                    InsertTest("Start", prodType, cabinetNo);
                }
                else
                {
                    MessageBox.Show("请先选择设备");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }            
        }

        private void InsertTest(string order,string prod, int cabinet)
        {
            if (!String.IsNullOrEmpty(prod) && cabinet > -1)
            {
                //if (db.DBInsert("insert into dbo.TaskCabinet(EquipmentName,OrderType) values('" + manul_cbCabineit.SelectedItem.ToString() + "','" + EnumHelper.GetDescription(EnumC.CabinetW.Start) + "')"))
                //if (db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + CabinetData.getType(manul_cbCabineitType.SelectedItem.ToString())  + "',ProductType='"+ manul_cbCabineitType.SelectedItem.ToString()+ "' where EquipmentName='" + manul_cbCabineit.SelectedItem.ToString().Trim() + "'"))
                if(order== "Start")
                    TestingCabinets.getInstance(cabinet).cmdStart(prod, cabinet);
                if (order == "Stop")
                    TestingCabinets.getInstance(cabinet).cmdStop();
                //db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + order + "',ProductType='" + prod + "'," + "BasicID=" + 2998 + "where CabinetID=" + cabinet);
                //if (db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + "Start" + "',ProductType='" + prod + "' where CabinetID='" + cabinet + "'"))
                //    TransMessage(manul_cbCabineit.SelectedItem.ToString() + "任务手动插入成功");
                //else
                //    TransMessage(manul_cbCabineit.SelectedItem.ToString() + "任务手动插入失败");
            }
            else
            {
                MessageBox.Show("请先选择设备");
            }
        }

        private void manul_cbCabineitType_MouseClick(object sender, MouseEventArgs e)
        {
            manul_cbCabineitType.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                manul_cbCabineitType.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        private void manul_cbProductSort_Click(object sender, EventArgs e)
        {
            manul_cbProductSort.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                manul_cbProductSort.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        private void step_cbProductSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dt.Columns.Clear();
            dt = db.DBQuery("select * from sortdata where sortname='" + step_cbProductSort.SelectedItem.ToString() + "'");
            if ((int)dt.Rows[0]["number"] > 1)
            {
                step_cbProductNo.Enabled = true;
                step_cbProductNo.Items.Clear();
                for (int i = 1; i <= (int)dt.Rows[0]["number"]; ++i)
                {
                    step_cbProductNo.Items.Add(i.ToString() + "号位");
                }
            }
            else
            {
                step_cbProductNo.Enabled = false;
            }
        }

        private void step_cbProductSort_MouseClick(object sender, MouseEventArgs e)
        {
            step_cbProductSort.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                step_cbProductSort.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        int stepCycle = 0;
        private void step_btnTake_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (step_cbProductSort.SelectedIndex > -1 && step_cbCabinetNo.SelectedIndex > -1 && step_cbTrayNo.SelectedIndex > -1)
                {
                    Thread takeTh = new Thread(FrameToCabinet);
                    takeTh.Start();                    
                }
                else
                {
                    MessageBox.Show("输入信息不全！", "Information");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
                       
        }

        private void FrameToCabinet()
        {
            DataTable dtMTR = new DataTable();
            DataTable dtFeedBin = new DataTable();
            DataTable dtSortData = db.DBQuery("select * from dbo.SortData");

            String cabinetType = "";
            String prodCode = "";
            int pieceNo = 0;
            MTR mtr = MTR.GetIntanse();

            string prodType = step_cbProductSort.SelectedItem.ToString().Trim();

            int tmpIndex = step_cbTrayNo.SelectedIndex+1;
            int colNo = (tmpIndex - 1) / 5;
            int rowNo = (tmpIndex - 1) % 5;
            int trayNo = (colNo + 1) * 10 + (rowNo + 1);
            pieceNo = 1;

            if (step_cbProductNo.Enabled)
            {
                pieceNo = step_cbProductNo.SelectedIndex + 1;
            }
            int cabinetNo = step_cbCabinetNo.SelectedIndex;
            cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

            if (cabinetType != prodType)
            {
                MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                return;
            }

            dtFeedBin = db.DBQuery("select * from dbo.FeedBin");
            int aIndex = rowNo * 8 + colNo;
            string trayType = dtFeedBin.Rows[aIndex]["Sort"].ToString().Trim();
            if (trayType != prodType)
            {
                MessageBox.Show("料盘类型与选择的产品类型不匹配！");
                return;
            }

            dt.Rows.Clear();
            dt.Columns.Clear();
            dt = db.DBQuery("select * from dbo.MTR where CabinetID=" + cabinetNo);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string curStation = dt.Rows[i]["CurrentStation"].ToString().Trim().Substring(2);
                int basicID = (int)dt.Rows[i]["BasicID"];
                if (curStation.Equals("号机台"))
                {
                    MessageBox.Show("目标测试柜已有产品！");
                    return;
                }
                else if (curStation.Equals("Robot"))
                {
                    MessageBox.Show("机器人忙！");
                    return;
                }
                else
                {
                    db.DBDelete("delete from dbo.MTR where BasicID = " + basicID);
                }
            }

            TaskCycle.actionType = "FrameToCabinet";
            //int numRemain = 0;
            int layerID = trayNo;
            int ret = mtr.InsertBasicID("0", 0, 0, prodType, "FeedBin", false, "0", cabinetNo);
            if (ret < 0)
            {
                MessageBox.Show("测试柜" + cabinetNo + "已有测试任务");
                return;
            }
            MTR.globalBasicID = ret;

            //插入机器人轨道到料架任务
            TaskCycle.PickStep = 0;
            //判断机器人是否在原点
            db.DBInsert("insert into dbo.TaskAxlis7(Axlis7Pos)values(" + (int)PlcData.getAxlis7Pos("料架位") + ")");

            //等待机器人轨道到位
            do
            {
                Thread.Sleep(100);
            } while (TaskCycle.PickStep != 10);

            //查FeedBin表，确定料盘位置和物料在料盘中的位置，插于取料盘任务
            db.DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            db.DBUpdate("update dbo.MTR set FrameLocation = " + trayNo + "," + "SalverLocation=" + pieceNo + " where BasicID=" + MTR.globalBasicID);
            db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.GetPiece + "," + trayNo + ")");

            //等待料架取料盘完成
            do
            {
                Thread.Sleep(100);
            } while (TaskCycle.PickStep != 20);

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

            int shootTimes = 0;
        ShootAgain:
            string CordinatorX = "0";
            string CordinatorY = "0";
            string CordinatorU = "0";
            cFrom.CCDTrigger(prodNumber, pieceNo);
            shootTimes = shootTimes + 1;

            if (cFrom.CCDDone == -1)
            {
                Thread.Sleep(200);
                if (shootTimes < 4)
                {
                    goto ShootAgain;
                }

                db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                //插入放回料盘任务
                db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + trayNo + ")");
                do
                {
                    Thread.Sleep(100);
                } while (TaskCycle.PickStep != 30);
                MessageBox.Show("视觉识别失败", "Information");
                return;
            }

            if (prodType == "D")
            {
                CordinatorX = cFrom.X;
                CordinatorY = cFrom.Y;
            }

            //插入机器人取料任务                         
            db.DBUpdate("update dbo.MTR set CurrentStation = 'Robot',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            Robot.GetInstanse().doGetProductFromFrame(prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);

            //等待机器人取料完成
            do
            {
                Thread.Sleep(100);
            } while (TaskCycle.PickStep != 30);

            if (!TaskCycle.scanStatus)      //读码失败
            {
                //db.DBUpdate("update dbo.MTR set ProductID = '" + "0" + "'where BasicID=" + MTR.globalBasicID);
                //db.DBInsert("insert into dbo.ActualData(ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,CheckResult)values('" + "Failed" + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "NG" + "')");
                //db.DBInsert("insert into dbo.FrameData(BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,CheckResult)values(" + MTR.globalBasicID + ",'" + "Failed" + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "NG" + "')");
                db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);

                //插入机器人放料到料架任务
                Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);
                plc.DBWrite(PlcData.PlcStatusAddress, 3, 1, new Byte[] { 0 });
                //FrameDataUpdate();

                //等待放料任务完成
                do
                {
                    Thread.Sleep(100);
                } while (TaskCycle.PickStep != 40);

                //插入放回料盘任务
                db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + trayNo + ")");
                do
                {
                    Thread.Sleep(100);
                } while (TaskCycle.PickStep != 50);
                MessageBox.Show("产品码识别失败", "Information");
                return;
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
            db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + trayNo + ")");

            //插入机器人放料任务
            db.DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            Robot.GetInstanse().doPutProductToCabinet(prodType, cabinetNo);

            //更新MTR表格
            db.DBUpdate("update dbo.MTR set CurrentStation = '" + TestingCabinets.getInstance(cabinetNo).Name + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            stepCycle = 50;
            MessageBox.Show("操作完成！", "Information");
        }

        private void step_btnTestStart_Click(object sender, EventArgs e)
        {
            bool testStarted = false;
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (stepCycle == 50 && testStarted==false)
                {
                    if (step_cbProductSort.SelectedIndex > -1 && step_cbCabinetNo.SelectedIndex > -1 && step_cbTrayNo.SelectedIndex > -1)
                    {
                    
                        DataTable dtMTR = new DataTable();
                        DataTable dtFeedBin = new DataTable();
                        DataTable dtSortData = db.DBQuery("select * from dbo.SortData");

                        String cabinetType = "";
                        int pieceNo = 0;
                        MTR mtr = MTR.GetIntanse();

                        string prodType = step_cbProductSort.SelectedItem.ToString();
                        if (step_cbProductNo.Enabled)
                        {
                            pieceNo = step_cbProductNo.SelectedIndex+1;
                        }
                        int cabinetNo = Convert.ToInt32(step_cbCabinetNo.SelectedIndex);
                        string cabinetName = TestingCabinets.getInstance(cabinetNo).Name;
                        cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

                        if (cabinetType != prodType)
                        {
                            MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                            return;
                        }
                        testStarted = true;
                        stepCycle = 60;
                        //插入测试开始任务
                        TestingCabinets.getInstance(cabinetNo).cmdStart(prodType, MTR.globalBasicID);
                    }
                    else
                    {
                        MessageBox.Show("输入信息不全！", "Information");
                    }
                }                
                else
                {
                    MessageBox.Show("请先从料架取产品放入测试柜或者测试进行中！", "Information");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                if (stepCycle == 60)
                {
                    if (step_cbProductSort.SelectedIndex > -1 && step_cbCabinetNo.SelectedIndex > -1 && step_cbTrayNo.SelectedIndex > -1)
                    {
                    
                        DataTable dtMTR = new DataTable();
                        DataTable dtFeedBin = new DataTable();
                        DataTable dtSortData = db.DBQuery("select * from dbo.SortData");

                        String cabinetType = "";
                        int pieceNo = 0;
                        MTR mtr = MTR.GetIntanse();

                        string prodType = step_cbProductSort.SelectedItem.ToString();
                        if (step_cbProductNo.Enabled)
                        {
                            pieceNo = Convert.ToInt32(step_cbProductNo.SelectedItem.ToString());
                        }
                        int cabinetNo = Convert.ToInt32(step_cbCabinetNo.SelectedIndex);
                        string cabinetName = TestingCabinets.getInstance(cabinetNo).Name;
                        cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

                        if (cabinetType != prodType)
                        {
                            MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                            return;
                        }
                        stepCycle = 70;
                        //插入测试停止任务
                        TestingCabinets.getInstance(cabinetNo).cmdStop();
                    }
                    else
                    {
                        MessageBox.Show("输入信息不全！", "Information");
                    }
                }                
                else
                {
                    MessageBox.Show("没有产品正在测试！");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }            
        }

        private void step_btnFetch_Click(object sender, EventArgs e)
        {
            if (Run.stepEnable == true && Run.readyForStep == true)
            {
                bool testState=false;
                DataTable dtMTR = new DataTable();
                dtMTR = db.DBQuery("select * from dbo.MTR where BasicID=" + MTR.globalBasicID);
                if (dtMTR !=null && dtMTR.Rows.Count !=0)
                {
                    testState = (bool)dtMTR.Rows[0]["StationSign"];
                }
                if ((stepCycle == 70) || ((stepCycle == 60) && testState))
                {                   
                    if (step_cbProductSort.SelectedIndex > -1 && step_cbCabinetNo.SelectedIndex > -1 && step_cbTrayNo.SelectedIndex > -1)
                    {
                        Thread fetchTh = new Thread(CabinetToFrame);
                        fetchTh.Start();
                    }
                    else
                    {
                        MessageBox.Show("输入信息不全！", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("测试还没有完成！");
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }            
        }

        private void CabinetToFrame()
        {
            DataTable dtMTR = new DataTable();
            DataTable dtFeedBin = new DataTable();
            DataTable dtSortData = db.DBQuery("select * from dbo.SortData");

            String cabinetType = "";
            String prodCode = "";
            string checkResult = "";
            int pieceNo = 0;
            MTR mtr = MTR.GetIntanse();

            if (dtMTR !=null)
            {
                dtMTR = db.DBQuery("select * from dbo.MTR where BasicID=" + MTR.globalBasicID);
            }

            if (dtMTR.Rows.Count !=0)
            {
                prodCode=dtMTR.Rows[0]["ProductID"].ToString().Trim();
                checkResult = dtMTR.Rows[0]["ProductCheckResult"].ToString().Trim();
            }
            

            string prodType = step_cbProductSort.SelectedItem.ToString();

            int tmpIndex = step_cbTrayNo.SelectedIndex + 1;
            int colNo = (tmpIndex - 1) / 5;
            int rowNo = (tmpIndex - 1) % 5;
            int trayNo = (colNo + 1) * 10 + (rowNo + 1);
            pieceNo = 1;

            if (step_cbProductNo.Enabled)
            {
                pieceNo = step_cbProductNo.SelectedIndex + 1;
            }
            int cabinetNo = Convert.ToInt32(step_cbCabinetNo.SelectedIndex);
            string cabinetName = TestingCabinets.getInstance(cabinetNo).Name;
            cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

            if (cabinetType != prodType)
            {
                MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                return;
            }

            dtFeedBin = db.DBQuery("select * from dbo.FeedBin");
            int aIndex = rowNo * 8 + colNo;
            string trayType = dtFeedBin.Rows[aIndex]["Sort"].ToString().Trim();
            if (trayType != prodType)
            {
                MessageBox.Show("料盘类型与选择的产品类型不匹配！");
                return;
            }

            TaskCycle.actionType = "CabinetToFrame";           
            TaskCycle.PutStep = 0;
            //插入机器人轨道任务到测试柜
            //判断机器人是否在原点
            //插入机器人从测试柜的取料任务；
            Robot.GetInstanse().doGetProductFromCabinet(prodType, cabinetNo);

            //插入机器人轨道到料架任务
            //判断机器人是否在原点
            db.DBInsert("insert into dbo.TaskAxlis7(Axlis7Pos)values(" + (int)PlcData.getAxlis7Pos("料架位") + ")");

            //插入料架取料任务，取出托盘（要区分取出和放入）
            db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.GetPiece + "," + trayNo + ")");

            //等待和机器人轨道到位和料架取出托盘完成
            do
            {
                Thread.Sleep(100);
            } while (TaskCycle.PutStep != 40);

            //插入机器人回料任务
            Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo);


            //插入料架放料任务，放回托盘；（要区分取出和放入）
            db.DBUpdate("update dbo.MTR set CurrentStation = 'FeedBin',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            db.DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + trayNo + ")");

            //等待料架放回托盘完成
            do
            {
                Thread.Sleep(100);
            } while (TaskCycle.PutStep != 60);

            //根据结果更新FeedBin表格                                                      
            int layerID = tmpIndex;
            dtFeedBin = db.DBQuery("select * from dbo.FeedBin where LayerID=" + layerID);
            if (checkResult == "OK")
            {
                int okNum = (int)dtFeedBin.Rows[0]["ResultOK"] + 1;
                int ngNum = (int)dtFeedBin.Rows[0]["ResultNG"] - 1;
                db.DBUpdate("update dbo.FeedBin set ResultOK = " + okNum + "," + "ResultNG =" + ngNum + "where LayerID=" + layerID);
            }
            //FrameDataUpdate();

            //测试结果插入测试统计表格；
            db.DBInsert("insert into dbo.ActualData(ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,CheckResult)values('" + prodCode + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + checkResult + "')");
            db.DBInsert("insert into dbo.FrameData(BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,CheckResult)values(" + MTR.globalBasicID + ",'" + prodCode + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + checkResult + "')");
            db.DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
            TaskCycle.PutStep = 0;
            stepCycle = 0;
            MessageBox.Show("操作完成！", "Information");
        }        
    }
}
