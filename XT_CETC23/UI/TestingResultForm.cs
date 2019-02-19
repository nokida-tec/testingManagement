using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XT_CETC23.UI
{
    public partial class TestingResultForm : Form
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

    }
}
