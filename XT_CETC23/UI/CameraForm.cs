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
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using System.Threading;
namespace XT_CETC23.SonForm
{
    public partial class CameraForm : Form,ICameraForm
    {
        Thread thLive; 
        public string X, Y;
        public int CCDDone;
        int i = 0;
        public CameraForm()
        {
            InitializeComponent();
            if (Config.Config.ENABLED_VISIONPRO == true)
            {
                Loadvpp();
            }
        }
        public void Loadvpp()
        {
            Camera.ccd1Path = AppDomain.CurrentDomain.BaseDirectory + "\\CCD1.vpp";
            Camera.tool1Path = AppDomain.CurrentDomain.BaseDirectory + "\\1.vpp";
            Camera.CCD = CogSerializer.LoadObjectFromFile(Camera.ccd1Path) as CogAcqFifoTool;
            Camera.Tool = CogSerializer.LoadObjectFromFile(Camera.tool1Path) as CogToolBlock;
        }

        private void ToolRun(ref CogToolBlock tool, int type, int number)
        {
            Camera.CCD.Run();
            if (Camera.CCD.RunStatus.Result == CogToolResultConstants.Accept)
            {
                tool.Inputs["InputImage"].Value = Camera.CCD.OutputImage;
            }
            else
            {                
                X = "-1";
                Y = "-1";
                CCDDone = -1;
                return;
            }
            tool.Inputs["ChooseNumA"].Value = (double)number;
            tool.Run();
            tool.Tools[0].Run();
            tool.Tools[type].Run();
            CogToolBlock _tool = tool.Tools[type] as CogToolBlock;
            if (tool.Tools[type].RunStatus.Result == CogToolResultConstants.Accept)
            {
                Camera.CCDResult.CCDDone = 1;
                Camera.CCDResult.XPos = Convert.ToDouble(_tool.Outputs["X"].Value);
                Camera.CCDResult.YPos = Convert.ToDouble(_tool.Outputs["Y"].Value);
                Camera.CCDResult.Angel = Convert.ToDouble(_tool.Outputs["Angle"].Value);
                X = Camera.CCDResult.XPos.ToString("0.000");
                Y = Camera.CCDResult.YPos.ToString("0.000");
                CCDDone = 1;
                this.BeginInvoke(new Action(() =>
                {
                    this.cogRecordDisplay1.Record = Camera.Tool.Tools[type].CreateLastRunRecord().SubRecords["CogPMAlignTool1.InputImage"];
                    this.cogRecordDisplay1.Fit(true);
                    this.XBox.Text = X;
                    this.YBox.Text = Y;
                }));
                i = 1;
            }
            else
            {
                Camera.CCDResult.CCDDone = -1;
                Camera.CCDResult.XPos = -1;
                Camera.CCDResult.YPos = -1;
                Camera.CCDResult.Angel = -1;
                X = "-1";
                Y = "-1";
                CCDDone = -1;
                cogRecordDisplay1.Invoke(new EventHandler(delegate
                {
                    this.cogRecordDisplay1.Record = Camera.Tool.Tools[type].CreateLastRunRecord().SubRecords["CogPMAlignTool1.InputImage"];
                    this.cogRecordDisplay1.Fit(true);
                }));
                    
                //this.Invoke(new Action(() =>
                //{
                //    this.cogRecordDisplay1.Record = Camera.Tool.Tools[type].CreateLastRunRecord().SubRecords["CogPMAlignTool1.InputImage"];
                //    this.cogRecordDisplay1.Fit(true);
                //    this.XBox.Text = X;
                //    this.YBox.Text = Y;
                //}));
                //i = 1;
            }
        }

        public bool CCDTrigger(int No, int Number)
        {
            Camera.CCDResult.CCDDone = 0;
            switch (No)
            {
                case 1:
                    switch (Number)
                    {
                        case 1:
                            ToolRun(ref Camera.Tool, 1, 1);
                            break;
                        case 2:
                            ToolRun(ref Camera.Tool, 1, 2);
                            break;
                    }
                    break;
                case 2:
                    switch (Number)
                    {
                        case 1:
                            ToolRun(ref Camera.Tool, 2, 1);
                            break;
                        case 2:
                            ToolRun(ref Camera.Tool, 2, 2);
                            break;
                    }
                    break;
                case 3:
                    switch (Number)
                    {
                        case 1:
                            ToolRun(ref Camera.Tool, 3, 1);
                            break;
                        case 2:
                            ToolRun(ref Camera.Tool, 3, 2);
                            break;
                    }
                    break;
                case 4:
                    switch (Number)
                    {
                        case 1:
                            ToolRun(ref Camera.Tool, 4, 1);
                            break;
                        case 2:
                            ToolRun(ref Camera.Tool, 4, 2);
                            break;
                        case 3:
                            ToolRun(ref Camera.Tool, 4, 3);
                            break;
                        case 4:
                            ToolRun(ref Camera.Tool, 4, 4);
                            break;
                        case 5:
                            ToolRun(ref Camera.Tool, 4, 5);
                            break;
                        case 6:
                            ToolRun(ref Camera.Tool, 4, 6);
                            break;
                        case 7:
                            ToolRun(ref Camera.Tool, 4, 7);
                            break;
                        case 8:
                            ToolRun(ref Camera.Tool, 4, 8);
                            break;
                        case 9:
                            ToolRun(ref Camera.Tool, 4, 9);
                            break;
                        case 10:
                            ToolRun(ref Camera.Tool, 4, 10);
                            break;
                        case 11:
                            ToolRun(ref Camera.Tool, 4, 11);
                            break;
                        case 12:
                            ToolRun(ref Camera.Tool, 4, 12);
                            break;
                        case 13:
                            ToolRun(ref Camera.Tool, 4, 13);
                            break;
                        case 14:
                            ToolRun(ref Camera.Tool, 4, 14);
                            break;
                        case 15:
                            ToolRun(ref Camera.Tool, 4, 15);
                            break;
                        case 16:
                            ToolRun(ref Camera.Tool, 4, 16);
                            break;
                        case 17:
                            ToolRun(ref Camera.Tool, 4, 17);
                            break;
                        case 18:
                            ToolRun(ref Camera.Tool, 4, 18);
                            break;
                        case 19:
                            ToolRun(ref Camera.Tool, 4, 19);
                            break;
                        case 20:
                            ToolRun(ref Camera.Tool, 4, 20);
                            break;
                    }
                    break;
                //case 5:
                //    ToolRun(ref Camera.Tool, 5, int Number);
                //    break;
            }
            return true;
        }

        private void TriggerButton_Click(object sender, EventArgs e)
        {
            if ((SelectcomboBox.SelectedItem != null && SelectcomboBox.SelectedItem.ToString() != "") && (SelectcomboBox1.SelectedItem != null && SelectcomboBox1.SelectedItem.ToString() != ""))
            {
                CCDTrigger(Convert.ToInt32(SelectcomboBox.SelectedItem.ToString()), Convert.ToInt32(SelectcomboBox1.SelectedItem.ToString()));
            }
        }
    }
}
