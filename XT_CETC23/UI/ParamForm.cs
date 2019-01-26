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
        }

        private void btnSaveProdStatus_Click(object sender, EventArgs e)
        {
       //     this.operateDefTableAdapter.Update(this.dB23DataSet_OperateDef.OperateDef);
        }

        private void btnSaveProdCode_Click(object sender, EventArgs e)
        {
       //     this.productDefTableAdapter.Update(this._23DataSet.ProductDef);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            groupBox1.Width = this.Width / 2 - 8;
            groupBox2.Width = this.Width / 2 - 8;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }  

    }
}
