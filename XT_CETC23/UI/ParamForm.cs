using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XT_CETC23.SonForm
{
    public partial class ParamForm : Form
    {
        public ParamForm()
        {
            InitializeComponent();
        }

        private void ParamForm_Load(object sender, EventArgs e)
        {
            // TODO:  这行代码将数据加载到表“dB23DataSet.ProductDef”中。您可以根据需要移动或删除它。
            this.productDefTableAdapter.Fill(this.dB23DataSet.ProductDef);
            // TODO:  这行代码将数据加载到表“dB23DataSet.OperateDef”中。您可以根据需要移动或删除它。
            this.operateDefTableAdapter.Fill(this.dB23DataSet.OperateDef);
        }

        private void btnSaveProdCode_Click(object sender, EventArgs e)
        {
            this.productDefTableAdapter.Update(this.dB23DataSet.ProductDef);
        }

        private void btnSaveProdStatus_Click(object sender, EventArgs e)
        {
            this.operateDefTableAdapter.Update(this.dB23DataSet.OperateDef);
        }

    }
}
