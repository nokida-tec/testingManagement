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
using XT_CETC23.Model;

namespace XT_CETC23.SonForm
{
    public partial class StepForm : Form
    {
        Queue<string> mQueue = new System.Collections.Generic.Queue<string>();
        delegate void StepAction(Queue<string> mQueue);
        IAsyncResult result;

        StepAction mStepAction;
        CameraForm cFrom;

        public StepForm(ManulForm mFrom, CameraForm cForm)
        {
            this.cFrom = cForm;
            InitializeComponent();

            mStepAction = doStep;
            string[] pos = new string[7];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = cbPos_Robot.Items[i].ToString();
            }
            PlcData.AddAxlis7Pos(pos);
            //manulForm.clearTask();
        }

        private void manul_cbProductSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from sortdata where sortname='" + cbProductType_Robot.SelectedItem.ToString() + "'");
                if ((int)dt.Rows[0]["number"] > 1)
                {
                    //manul_cbProductNum.Enabled = true;
                    cbSlotNo_Robot.Items.Clear();
                    for (int i = 1; i <= (int)dt.Rows[0]["number"]; ++i)
                    {
                        cbSlotNo_Robot.Items.Add(i.ToString() + "号位");
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

        private void onSelectedChangedPosOfRobot(object sender, EventArgs e)
        {
            if (cbPos_Robot.SelectedItem.ToString() == "料架位")
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from sortdata where sortname='" + cbProductType_Robot.SelectedItem.ToString() + "'");
                if ((int)dt.Rows[0]["number"] > 1)
                {
                    cbSlotNo_Robot.Enabled = true;
                }
                else
                {
                    cbSlotNo_Robot.Enabled = false;
                }
            }
            else
            {
                cbSlotNo_Robot.Enabled = false;
            }
        }

        private void onClick_RobotAction(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbPos_Robot.SelectedIndex == -1
                    || cbOrder_Robot.SelectedIndex == -1
                    || cbSlotNo_Robot.SelectedIndex == -1
                    || cbProductType_Robot.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;

                String pos = cbPos_Robot.SelectedItem.ToString();

                if (ckbAxis7Alone.Checked) // 轨道独立运动
                {
                    InsertStep("Rail", "Move", pos);
                    return;
                }
                else 
                {
                    string order = cbOrder_Robot.SelectedItem.ToString();
                    string productType = cbProductType_Robot.SelectedItem.ToString(); 
                    String slot = Convert.ToString(cbSlotNo_Robot.SelectedIndex);
                    InsertStep("Robot", order, pos, productType, slot);
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }            
        }

        void doStep(Queue<string> mQueue)
        {
            lock (TestingSystem.lockStep)
            {
                // Thread.Sleep(2000);
                if (Config.Config.ENABLED_DEBUG == true || Plc.GetInstanse().isConnected)
                {
                    string module = mQueue.Dequeue();
                    string order = mQueue.Dequeue();
                    byte[] buffer = new byte[1];
                    switch (module)
                    {
                        case "Frame":
                            switch (order)
                            {
                                case "Scan":
                                    Frame.getInstance().doScan();
                                    break;
                                case "取料":
                                    {
                                        string pos = mQueue.Dequeue();
                                        buffer[0] = Frame.getInstance().convertFrameLocationToByte(pos);
                                        Frame.getInstance().doGet((int)buffer[0]);
                                    }
                                    break;
                                case "放料":
                                    {
                                        string pos = mQueue.Dequeue();
                                        buffer[0] = Frame.getInstance().convertFrameLocationToByte(pos);
                                        Frame.getInstance().doPut((int)buffer[0]);
                                    }
                                    break;
                            }
                            break;
                        case "Robot":
                            {
                                string pos = mQueue.Dequeue();
                                string productType = mQueue.Dequeue();
                                string slot = mQueue.Dequeue();

                                switch (order)
                                {
                                    case "取料":
                                        {
                                            buffer[0] = Frame.getInstance().convertFrameLocationToByte(pos);
                                            Frame.getInstance().doGet((int)buffer[0]);
                                        }
                                        break;
                                    case "放料":
                                        {
                                            buffer[0] = Frame.getInstance().convertFrameLocationToByte(pos);
                                            Frame.getInstance().doPut((int)buffer[0]);
                                        }
                                        break;
                                }
                            }
                            break;
                        case "Rail":
                            {
                                string pos = mQueue.Dequeue();
                                Robot.GetInstanse().doStepRailMove(pos);
                            }
                            break;
                        case "Cabinet":
                            break;
                        case "Loop":
                            break;
                    }

                    mQueue.Clear();
                }
                else
                {
                    return;
                }
            }
        }

        private void onClick_FrameAction(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbGoalPos_Frame.SelectedIndex == -1 
                    || cbOrder_Frame.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                String layerNo = cbGoalPos_Frame.SelectedItem.ToString();
                String commandNo = cbOrder_Frame.SelectedItem.ToString();
                InsertStep("Frame", commandNo, layerNo);
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
            
        }

        private void InsertPickTtay(String layer, String command)
        {
            // lock (TestingSystem.lockStep)
            {
                if (Config.Config.ENABLED_DEBUG == true || Plc.GetInstanse().isConnected)
                {
                    if (!String.IsNullOrEmpty(layer) && !String.IsNullOrEmpty(command))
                    {
                        mQueue.Enqueue(layer);
                        mQueue.Enqueue(command);
                        mQueue.Enqueue("料架");
                        result = mStepAction.BeginInvoke(mQueue, null, null);
                    }
                    else
                    {
                        MessageBox.Show("非法操作,信息不全"); 
                    }
                }
                else
                { 
                    MessageBox.Show("PLC未连接");
                }
            }
        }

        private void InsertStep(String moudle, String command, String param1 = null, String param2 = null, String param3 = null, String param4 = null)
        {
            // lock (TestingSystem.lockStep)
            {
                if (Config.Config.ENABLED_DEBUG == true || Plc.GetInstanse().isConnected)
                {
                    if (String.IsNullOrEmpty(moudle))
                        return;
                    mQueue.Enqueue(moudle);

                    if (String.IsNullOrEmpty(command))
                        return;
                    mQueue.Enqueue(command);

                    if (!String.IsNullOrEmpty(param1))
                    {
                        mQueue.Enqueue(param1);
                    }
                    if (!String.IsNullOrEmpty(param2))
                    {
                        mQueue.Enqueue(param2);
                    }
                    if (!String.IsNullOrEmpty(param3))
                    {
                        mQueue.Enqueue(param3);
                    }
                    if (!String.IsNullOrEmpty(param4))
                    {
                        mQueue.Enqueue(param4);
                    } 
                    result = mStepAction.BeginInvoke(mQueue, null, null);
                }
                else
                {
                    MessageBox.Show("PLC未连接");
                }
            }
        }

        //插入扫码任务
        private void onClick_Scan(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                
                InsertStep("Frame", "Scan");
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
        }

        private void onClick_SingleTestStop(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                if (cbCabinetNo_Cabinet.SelectedIndex > -1)
                {
                    String prodType = cbProductType_Cabinet.SelectedItem.ToString();
                    int cabinetNo = cbCabinetNo_Cabinet.SelectedIndex;
                    InsertStep("Cabinet", "Stop", Convert.ToString(cabinetNo), prodType);
                    //InsertTest("Stop", prodType, cabinetNo);
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

        private void onClick_SingleTestStart(object sender, EventArgs e)
        {            
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                if (cbCabinetNo_Cabinet.SelectedIndex > -1)
                {
                    String prodType = cbProductType_Cabinet.SelectedItem.ToString();
                    int cabinetNo = cbCabinetNo_Cabinet.SelectedIndex;
                    InsertStep("Cabinet", "Start", Convert.ToString(cabinetNo), prodType);
                    //InsertTest("Start", prodType, cabinetNo);
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
                //if (DataBase.GetInstanse().DBInsert("insert into dbo.TaskCabinet(EquipmentName,OrderType) values('" + manul_cbCabineit.SelectedItem.ToString() + "','" + EnumHelper.GetDescription(EnumC.CabinetW.Start) + "')"))
                //if (DataBase.GetInstanse().DBUpdate("update dbo.TaskCabinet set OrderType= '" + CabinetData.getType(manul_cbCabineitType.SelectedItem.ToString())  + "',ProductType='"+ manul_cbCabineitType.SelectedItem.ToString()+ "' where EquipmentName='" + manul_cbCabineit.SelectedItem.ToString().Trim() + "'"))
                if(order== "Start")
                    TestingCabinets.getInstance(cabinet).cmdStart(prod, cabinet);
                if (order == "Stop")
                    TestingCabinets.getInstance(cabinet).cmdStop();
                //DataBase.GetInstanse().DBUpdate("update dbo.TaskCabinet set OrderType= '" + order + "',ProductType='" + prod + "'," + "BasicID=" + 2998 + "where CabinetID=" + cabinet);
                //if (DataBase.GetInstanse().DBUpdate("update dbo.TaskCabinet set OrderType= '" + "Start" + "',ProductType='" + prod + "' where CabinetID='" + cabinet + "'"))
                //    TransMessage(manul_cbCabineit.SelectedItem.ToString() + "任务手动插入成功");
                //else
                //    TransMessage(manul_cbCabineit.SelectedItem.ToString() + "任务手动插入失败");
            }
            else
            {
                MessageBox.Show("请先选择设备");
            }
        }

        private void onClickProductTypeOfCabinet(object sender, MouseEventArgs e)
        {
            cbProductType_Cabinet.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                cbProductType_Cabinet.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        private void onClickProductTypeOfRobot(object sender, EventArgs e)
        {
            cbProductType_Robot.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                cbProductType_Robot.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        private void onSelectedChangedProductTypeOfLoop(object sender, EventArgs e)
        {
            DataTable dt = DataBase.GetInstanse().DBQuery("select * from sortdata where sortname='" + cbProductType_Loop.SelectedItem.ToString() + "'");
            if ((int)dt.Rows[0]["number"] > 1)
            {
                cbSlotNo_Loop.Enabled = true;
                cbSlotNo_Loop.Items.Clear();
                for (int i = 1; i <= (int)dt.Rows[0]["number"]; ++i)
                {
                    cbSlotNo_Loop.Items.Add(i.ToString() + "号位");
                }
            }
            else
            {
                cbSlotNo_Loop.Enabled = false;
            }
        }

        private void onClickProductTypeOfLoop(object sender, MouseEventArgs e)
        {
            cbProductType_Loop.Items.Clear();
            for (int i = 0; i < MaterielData.grabType.Rows.Count; ++i)
            {
                cbProductType_Loop.Items.Add(MaterielData.grabType.Rows[i]["sortname"].ToString());
            }
        }

        int stepCycle = 0;
        private void onClick_Take(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbProductType_Loop.SelectedIndex == -1
                    || cbTrayNo_Loop.SelectedIndex == -1
                    || cbSlotNo_Loop.SelectedIndex == -1
                    || cbCabinetNo_Loop.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                String productType = cbProductType_Loop.SelectedItem.ToString();
                String trayNo = cbTrayNo_Loop.SelectedItem.ToString();
                String slotNo = cbSlotNo_Loop.SelectedItem.ToString();
                String cabinetNo = cbCabinetNo_Loop.SelectedItem.ToString();
                InsertStep("Loop", "Take", productType, trayNo, slotNo, cabinetNo);
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
            DataTable dtSortData = DataBase.GetInstanse().DBQuery("select * from dbo.SortData");

            String cabinetType = "";
            String prodCode = "";
            int pieceNo = 0;
            MTR mtr = MTR.GetIntanse();

            string prodType = cbProductType_Loop.SelectedItem.ToString().Trim();

            int tmpIndex = cbTrayNo_Loop.SelectedIndex+1;
            int colNo = (tmpIndex - 1) / 5;
            int rowNo = (tmpIndex - 1) % 5;
            int trayNo = (colNo + 1) * 10 + (rowNo + 1);
            pieceNo = 1;

            if (cbSlotNo_Loop.Enabled)
            {
                pieceNo = cbSlotNo_Loop.SelectedIndex + 1;
            }
            int cabinetNo = cbCabinetNo_Loop.SelectedIndex;
            cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

            if (cabinetType != prodType)
            {
                MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                return;
            }

            dtFeedBin = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin");
            int aIndex = rowNo * 8 + colNo;
            string trayType = dtFeedBin.Rows[aIndex]["Sort"].ToString().Trim();
            if (trayType != prodType)
            {
                MessageBox.Show("料盘类型与选择的产品类型不匹配！");
                return;
            }

            DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where CabinetID=" + cabinetNo);
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
                    DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + basicID);
                }
            }

            //TaskCycle.actionType = "FrameToCabinet";
            //int numRemain = 0;
            int layerID = trayNo;
            Batch batch = new Batch();
            batch.LoadUnfinished();
            int ret = mtr.InsertBasicID("0", 0, 0, prodType, "FeedBin", false, "0", batch.ID, cabinetNo);
            if (ret < 0)
            {
                MessageBox.Show("测试柜" + cabinetNo + "已有测试任务");
                return;
            }
            MTR.globalBasicID = ret;

            //插入机器人轨道到料架任务
            //判断机器人是否在原点
            Robot.GetInstanse().doMoveToZeroPos();

            DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            DataBase.GetInstanse().DBUpdate("update dbo.MTR set FrameLocation = " + trayNo + "," + "SalverLocation=" + pieceNo + " where BasicID=" + MTR.globalBasicID);
            //查FeedBin表，确定料盘位置和物料在料盘中的位置，插于取料盘任务
            Frame.getInstance().doGet(trayNo);

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

                DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
                //插入放回料盘任务
                Frame.getInstance().doPut(trayNo);
                MessageBox.Show("视觉识别失败", "Information");
                return;
            }

            if (prodType == "D")
            {
                CordinatorX = cFrom.X;
                CordinatorY = cFrom.Y;
            }

            //插入机器人取料任务                         
            DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = 'Robot',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            ReturnCode returnCode = Robot.GetInstanse().doGetProductFromFrame(prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);

            if (returnCode == ReturnCode.ScanFailed)      //读码失败
            {
                //DataBase.GetInstanse().DBUpdate("update dbo.MTR set ProductID = '" + "0" + "'where BasicID=" + MTR.globalBasicID);
                //DataBase.GetInstanse().DBInsert("insert into dbo.ActualData(ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,CheckResult)values('" + "Failed" + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "NG" + "')");
                //DataBase.GetInstanse().DBInsert("insert into dbo.FrameData(BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,CheckResult)values(" + MTR.globalBasicID + ",'" + "Failed" + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "NG" + "')");
                DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);

                //插入机器人放料到料架任务
                Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo, CordinatorX, CordinatorY, CordinatorU);
                Plc.GetInstanse().DBWrite(PlcData.PlcStatusAddress, 3, 1, new Byte[] { 0 });
                //FrameDataUpdate();

                //插入放回料盘任务
                Frame.getInstance().doPut(trayNo);
                MessageBox.Show("产品码识别失败", "Information");
                return;
            }

            //读码成功            
            Byte[] myCode = Plc.GetInstanse().DbRead(104, 0, 556);
            Thread.Sleep(2000);
            Plc.GetInstanse().DBWrite(PlcData.PlcStatusAddress, 3, 1, new Byte[] { 0 });
            int strLen = Convert.ToInt32(myCode[504]);
            int realLen = Convert.ToInt32(myCode[505]);
            prodCode = Encoding.Default.GetString(myCode, 506, realLen).Trim();

            DataBase.GetInstanse().DBUpdate("update dbo.MTR set ProductID = '" + prodCode + "'where BasicID=" + MTR.globalBasicID);

            //插入放回料盘任务
            Frame.getInstance().doPut(trayNo);

            //插入机器人放料任务
            DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            Robot.GetInstanse().doPutProductToCabinet(prodType, cabinetNo);

            //更新MTR表格
            DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = '" + TestingCabinets.getInstance(cabinetNo).Name + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            stepCycle = 50;
            MessageBox.Show("操作完成！", "Information");
        }

        private void onClick_TestStart(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbProductType_Loop.SelectedIndex == -1
                    || cbTrayNo_Loop.SelectedIndex == -1
                    || cbSlotNo_Loop.SelectedIndex == -1
                    || cbCabinetNo_Loop.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                int cabinetNo = Convert.ToInt32(cbCabinetNo_Loop.SelectedIndex);
                String productType = cbProductType_Loop.SelectedItem.ToString();
                String trayNo = cbTrayNo_Loop.SelectedItem.ToString();
                String slotNo = cbSlotNo_Loop.SelectedItem.ToString();

                String cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;
                if (cabinetType != productType)
                {
                    MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;

                InsertStep("Loop", "Take", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
        }

        private void onClick_TestStop(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbProductType_Loop.SelectedIndex == -1
                    || cbTrayNo_Loop.SelectedIndex == -1
                    || cbSlotNo_Loop.SelectedIndex == -1
                    || cbCabinetNo_Loop.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                int cabinetNo = Convert.ToInt32(cbCabinetNo_Loop.SelectedIndex);
                String productType = cbProductType_Loop.SelectedItem.ToString();
                String trayNo = cbTrayNo_Loop.SelectedItem.ToString();
                String slotNo = cbSlotNo_Loop.SelectedItem.ToString();

                String cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;
                if (cabinetType != productType)
                {
                    MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;

                InsertStep("Loop", "StopTest", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
        }

        private void onClick_PutBack(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (cbProductType_Loop.SelectedIndex == -1
                    || cbTrayNo_Loop.SelectedIndex == -1
                    || cbSlotNo_Loop.SelectedIndex == -1
                    || cbCabinetNo_Loop.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("请选择参数!");
                    return;
                }

                int cabinetNo = Convert.ToInt32(cbCabinetNo_Loop.SelectedIndex);
                String productType = cbProductType_Loop.SelectedItem.ToString();
                String trayNo = cbTrayNo_Loop.SelectedItem.ToString();
                String slotNo = cbSlotNo_Loop.SelectedItem.ToString();

                String cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;
                if (cabinetType != productType)
                {
                    MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                    return;
                }

                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;

                InsertStep("Loop", "PutBack", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
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
            DataTable dtSortData = DataBase.GetInstanse().DBQuery("select * from dbo.SortData");

            String cabinetType = "";
            String prodCode = "";
            string checkResult = "";
            int pieceNo = 0;
            MTR mtr = MTR.GetIntanse();

            if (dtMTR !=null)
            {
                dtMTR = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID=" + MTR.globalBasicID);
            }

            if (dtMTR.Rows.Count !=0)
            {
                prodCode=dtMTR.Rows[0]["ProductID"].ToString().Trim();
                checkResult = dtMTR.Rows[0]["ProductCheckResult"].ToString().Trim();
            }
            

            string prodType = cbProductType_Loop.SelectedItem.ToString();

            int tmpIndex = cbTrayNo_Loop.SelectedIndex + 1;
            int colNo = (tmpIndex - 1) / 5;
            int rowNo = (tmpIndex - 1) % 5;
            int trayNo = (colNo + 1) * 10 + (rowNo + 1);
            pieceNo = 1;

            if (cbSlotNo_Loop.Enabled)
            {
                pieceNo = cbSlotNo_Loop.SelectedIndex + 1;
            }
            int cabinetNo = Convert.ToInt32(cbCabinetNo_Loop.SelectedIndex);
            string cabinetName = TestingCabinets.getInstance(cabinetNo).Name;
            cabinetType = TestingBedCapOfProduct.sTestingBedCapOfProduct[TestingCabinets.getInstance(cabinetNo).Type].ProductType;

            if (cabinetType != prodType)
            {
                MessageBox.Show("目标测试柜类型与选择的产品类型不匹配！");
                return;
            }

            dtFeedBin = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin");
            int aIndex = rowNo * 8 + colNo;
            string trayType = dtFeedBin.Rows[aIndex]["Sort"].ToString().Trim();
            if (trayType != prodType)
            {
                MessageBox.Show("料盘类型与选择的产品类型不匹配！");
                return;
            }

            //TaskCycle.actionType = "CabinetToFrame";           
            //插入机器人轨道任务到测试柜
            //判断机器人是否在原点
            //插入机器人从测试柜的取料任务；
            Robot.GetInstanse().doGetProductFromCabinet(prodType, cabinetNo);

            //插入机器人轨道到料架任务
            //判断机器人是否在原点
            Robot.GetInstanse().doMoveToZeroPos();

            //插入料架取料任务，取出托盘（要区分取出和放入）
            Frame.getInstance().doGet(trayNo);

            //插入机器人回料任务
            Robot.GetInstanse().doPutProductToFrame(prodType, pieceNo);


            //插入料架放料任务，放回托盘；（要区分取出和放入）
            DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = 'FeedBin',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);
            Frame.getInstance().doPut(trayNo);

            //根据结果更新FeedBin表格                                                      
            int layerID = tmpIndex;
            dtFeedBin = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin where LayerID=" + layerID);
            if (checkResult == "OK")
            {
                int okNum = (int)dtFeedBin.Rows[0]["ResultOK"] + 1;
                int ngNum = (int)dtFeedBin.Rows[0]["ResultNG"] - 1;
                DataBase.GetInstanse().DBUpdate("update dbo.FeedBin set ResultOK = " + okNum + "," + "ResultNG =" + ngNum + "where LayerID=" + layerID);
            }
            //FrameDataUpdate();

            //测试结果插入测试统计表格；
            DataBase.GetInstanse().DBInsert("insert into dbo.ActualData(ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinetA,CheckCabinetB,CheckDate,CheckTime,CheckBatch,CheckResult)values('" + prodCode + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + checkResult + "')");
            DataBase.GetInstanse().DBInsert("insert into dbo.FrameData(BasicID,ProductID,ProductType,FrameLocation,SalverLocation,CheckCabinet,CheckResult)values(" + MTR.globalBasicID + ",'" + prodCode + "','" + prodType + "'," + trayNo + "," + pieceNo + ",'" + cabinetName + "','" + checkResult + "')");
            DataBase.GetInstanse().DBDelete("delete from dbo.MTR where BasicID = " + MTR.globalBasicID);
            stepCycle = 0;
            MessageBox.Show("操作完成！", "Information");
        }        
    }
}
