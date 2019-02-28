using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.Common;
using XT_CETC23.INTransfer;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.DAL;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using XT_CETC23.Instances;

namespace XT_CETC23.SonForm
{
    public partial class TestingResultForm : Form, IDatabaseForm
    {
        public TestingResultForm()
        {
            InitializeComponent();
        }

        private void TestingResultForm_Load(object sender, EventArgs e)
        {
            // TODO:  这行代码将数据加载到表“dB23DataSet.ActualData”中。您可以根据需要移动或删除它。
            this.actualDataTableAdapter.Fill(this.dB23DataSet.ActualData);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.actualDataTableAdapter.FillBy(this.dB23DataSet.ActualData);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = ExportExcel.GetInstanse(this).ToDataTable(dataGridView);
            String fileName = "export";
            ExportExcel.GetInstanse(this).ExportTable(dt, fileName);
        }

        public DataTable getRunData()
        {
            throw new NotImplementedException();
        }

        public void getStatus(string str)
        {
         //   GetMessage(str);
        }

        private static String sComboAll = "全部";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comboFrameBatch.SelectedText == sComboAll 
                || comboFrameBatch.SelectedText == "")
            {  // 全部
                String collects = "";
                bool bFirst = true;

                for (int i = 1; i < comboFrameBatch.Items.Count; i ++)
                {
                    if(bFirst)
                    {
                        collects += comboFrameBatch.Items[i].ToString();
                    } 
                    else 
                    {
                        collects += "," + comboFrameBatch.Items[i].ToString();
                    }
                    bFirst = false;
                }
                if (collects != "")
                {
                    bindingSource1.Filter = "(BatchID is NULL AND [BeginTime] >= '"
                        + dateTimePickerToday.Value.ToString("yyyy-MM-dd")
                        + "' AND [BeginTime] < '"
                        + dateTimePickerToday.Value.AddDays(1).ToString("yyyy-MM-dd")
                        + "') OR BatchID IN ("
                        + collects
                        + ")";
                }
                else
                {
                    bindingSource1.Filter = "(BatchID is NULL AND [BeginTime] >= '"
                        + dateTimePickerToday.Value.ToString("yyyy-MM-dd")
                        + "' AND [BeginTime] < '"
                        + dateTimePickerToday.Value.AddDays(1).ToString("yyyy-MM-dd")
                        + "')";
                }
            }
            else
            {
                bindingSource1.Filter = "[BatchID] >= '"
                    + comboFrameBatch.SelectedText
                    + "'";
            }
        }

        private void dateTimePickerToday_ValueChanged(object sender, EventArgs e)
        {
            comboFrameBatch.Items.Clear();
            DataTable dt = DataBase.GetInstanse().DBQuery(
                "select ID from dbo.Batch where " 
                + "[BeginTime] >= '"
                + dateTimePickerToday.Value.ToString("yyyy-MM-dd")
                + "' AND [BeginTime] < '"
                + dateTimePickerToday.Value.AddDays(1).ToString("yyyy-MM-dd")
                + "'");
            comboFrameBatch.Items.Add(sComboAll);
            if(dt == null)
            {
                return;
            }
            for(int i = 0; i < dt.Rows.Count; i ++)
            {
                String batchID = dt.Rows[i]["ID"].ToString();
                comboFrameBatch.Items.Add(batchID);
            }
        }
    }
}
