using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.DataCom;
using XT_CETC23.DataManager;
using XT_CETC23.INTransfer;
namespace XT_CETC23.UserForms
{
    public partial class SortAdd : Form
    {
        DataBase db;
        IParaForm iParaForm;
        delegate void getSort(string str);
        getSort GetSort;
        public SortAdd(IParaForm iParaForm)
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            this.iParaForm = iParaForm;
            GetSort = iParaForm.getSort;
        }
        private void sort_btnAdd_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(sort_tbSortName.Text)&&!string.IsNullOrEmpty(sort_tbSortNum.Text))
            {
                if(db.DBConnect())
                {
                    if (0 < db.DBInsert("insert into sortdata(sortname,number) values('" + sort_tbSortName.Text + "','" + sort_tbSortNum.Text + "')")) { GetSort(sort_tbSortName.Text); MessageBox.Show("数据插入成功"); }
                    else { MessageBox.Show("数据插入失败"); }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("数据库连接失败");
                }
            }
            else
            {
                MessageBox.Show("非法操作，信息不全");
            }
        }

        private void sort_btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
