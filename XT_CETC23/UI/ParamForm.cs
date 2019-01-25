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
            // TODO:  这行代码将数据加载到表“dB23DataSet_OperateDef.OperateDef”中。您可以根据需要移动或删除它。
            this.operateDefTableAdapter.Fill(this.dB23DataSet_OperateDef.OperateDef);
            // TODO:  这行代码将数据加载到表“_23DataSet.ProductDef”中。您可以根据需要移动或删除它。
            this.productDefTableAdapter.Fill(this._23DataSet.ProductDef);

        }
    }
}
