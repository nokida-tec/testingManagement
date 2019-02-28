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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindingSource1.Filter = "[BeginTime] >= '"
                + dateTimePickerToday.Value.ToString("yyyy-MM-dd")
                + "' AND [BeginTime] < '"
                + dateTimePickerToday.Value.AddDays(1).ToString("yyyy-MM-dd")
                + "'";
        }
    }
}
