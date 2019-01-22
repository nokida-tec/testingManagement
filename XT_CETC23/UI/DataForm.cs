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
namespace XT_CETC23.SonForm
{
    public partial class DataForm : Form,IDatabaseForm
    {
        DataBase db;
        ExportExcel exportExcel;
        public delegate void getMessage(string message);
        public event getMessage GetMessage;
        public DataForm()
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            exportExcel = ExportExcel.GetInstanse(this);
        }

        public string[] getAcount()
        {
            throw new NotImplementedException();
        }

        public DataTable getRunData()
        {
            throw new NotImplementedException();
        }
        DataTable dt=new DataTable();
        private void data_btnQuery_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dt.Columns.Clear();
            if (data_cbQueryCondition.SelectedIndex > -1)
            {
                dt = db.DBQuery("select * from " + data_cbQueryCondition.SelectedItem.ToString() + " ");
                data_dgvQuery.DataSource = dt;
            }
            else
            { MessageBox.Show("请选择查询的表"); }
        }

        private void data_btnTable_Click(object sender, EventArgs e)
        {
            if (dt != null)
            {
                dt.TableName = data_cbQueryCondition.SelectedItem.ToString();
                exportExcel.ExportTable(dt, dt.TableName);
            }
        }
        public void getStatus(string str)
        {
            GetMessage(str);
        }
    }
}
