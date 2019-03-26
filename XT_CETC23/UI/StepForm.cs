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

        CameraForm cFrom;

        public StepForm(ManulForm mFrom, CameraForm cForm)
        {
            this.cFrom = cForm;
            InitializeComponent();

            string[] pos = new string[7];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = cbPos_Robot.Items[i].ToString();
            }
            PlcData.AddAxlis7Pos(pos);
            //manulForm.clearTask();
        }

        private void onSelectedChangedProductTypeOfRobot(object sender, EventArgs e)
        {
            if (cbProductType_Robot.SelectedIndex == -1)
            {
                return;
            }

            DataTable dt = DataBase.GetInstanse().DBQuery("select * from sortdata where sortname='" + cbProductType_Robot.SelectedItem.ToString() + "'");
            if ((int)dt.Rows[0]["number"] >= 1)
            {
                cbSlotNo_Robot.Enabled = true;
                cbSlotNo_Robot.Items.Clear();
                for (int i = 1; i <= (int)dt.Rows[0]["number"]; ++i)
                {
                    cbSlotNo_Robot.Items.Add(i.ToString() + "号位");
                }
            }
            else
            {
                cbSlotNo_Robot.Enabled = false;
            }

            onSelectedChangedPosOfRobot(sender, e);
        }

        private void onSelectedChangedPosOfRobot(object sender, EventArgs e)
        {
            if (cbPos_Robot.SelectedIndex == -1 || cbProductType_Robot.SelectedIndex == -1)
            {
                cbSlotNo_Robot.Enabled = false;
                return;
            }
            if (cbPos_Robot.SelectedItem.ToString() == "料架位")
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from sortdata where sortname='" + cbProductType_Robot.SelectedItem.ToString() + "'");
                if ((int)dt.Rows[0]["number"] >= 1)
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
                    || cbSlotNo_Robot.Enabled && cbSlotNo_Robot.SelectedIndex == -1
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
                    TestingSystem.GetInstance().doStepProxy("Rail", "Move", pos);
                    return;
                }
                else 
                {
                    string order = cbOrder_Robot.SelectedItem.ToString();
                    string productType = cbProductType_Robot.SelectedItem.ToString(); 
                    String slot = Convert.ToString(cbSlotNo_Robot.SelectedIndex + 1);
                    TestingSystem.GetInstance().doStepProxy("Robot", order, pos, productType, slot);
                }
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
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
                TestingSystem.GetInstance().doStepProxy("Frame", commandNo, layerNo);
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
            
        }

        //插入扫码任务
        private void onClick_Scan(object sender, EventArgs e)
        {
            if (TestingSystem.GetInstance().isReadyForStep())
            {
                if (MessageBox.Show("危险操作，请确认选择的参数！", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                
                TestingSystem.GetInstance().doStepProxy("Frame", "Scan");
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
                    TestingSystem.GetInstance().doStepProxy("Cabinet", "Stop", Convert.ToString(cabinetNo), prodType);
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
                    TestingSystem.GetInstance().doStepProxy("Cabinet", "Start", Convert.ToString(cabinetNo), prodType);
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
                TestingSystem.GetInstance().doStepProxy("Loop", "Take", productType, trayNo, slotNo, cabinetNo);
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }          
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

                TestingSystem.GetInstance().doStepProxy("Loop", "Take", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
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

                TestingSystem.GetInstance().doStepProxy("Loop", "StopTest", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
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

                TestingSystem.GetInstance().doStepProxy("Loop", "PutBack", productType, trayNo, slotNo, cbCabinetNo_Loop.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("自动流程还未完成，请耐心等待！", "Information");
            }
        }
    }
}
