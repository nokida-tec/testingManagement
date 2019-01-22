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
using XT_CETC23.UserForms;
namespace XT_CETC23.SonForm  
{
    public partial class UserForm : Form,IUserForm
    {
        DataBase db;
        UserLogin uLogin1;
        UserRegister uRegister1;
        UserChange uChange1;
        string accountTableName = "Acount";
        public delegate void getAccount(string[] str);
        public event getAccount GetAcount;
        public UserForm()
        {
            InitializeComponent();
            uLogin1 = new UserLogin(this);
            uRegister1 = new UserRegister();
            uChange1 = new UserChange();

            db = DataBase.GetInstanse();
        }
        private void user_btnQuery_Click(object sender, EventArgs e)
        {
            if (Common.Account.user == "admin")
            {

                if (db.DBConnect())
                {
                    dataClear();
                    user_dgvUser.DataSource = db.DBQuery(DataManager.DBstr.QueryStr(accountTableName));
                }
                else
                {
                    MessageBox.Show("数据库未连接");
                }
            }
            else
            {
                MessageBox.Show("当前用户无此权限");
            }
        }
        void dataClear()
        {
            if(user_dgvUser.DataSource!=null)
            {
                DataTable dt = (DataTable)user_dgvUser.DataSource;
                dt.Rows.Clear();
                user_dgvUser.DataSource = dt;
            }

        }
        public void getAcount(string[] str)
        {
            GetAcount(str);
            dataClear();
        }

        private void user_btnLogin_Click(object sender, EventArgs e)
        {
            dataClear();
            if (!uLogin1.IsDisposed)
            {
                uLogin1.Show();
            }
            else
            {
                UserLogin uLogin2 = new UserLogin(this);
                uLogin2.Show();
            }
        }
        private void user_btnRegister_Click(object sender, EventArgs e)
        {
            dataClear();
            if (Common.Account.user=="admin")
            {
                if(!uRegister1.IsDisposed)
                {
                    uRegister1.Show();
                }
                else
                {
                    UserRegister uRegister2 = new UserRegister();
                    uRegister2.Show();
                }
            }
            else
            {
                MessageBox.Show("当前用户无此权限");
            }
        }

        private void user_btnDelete_Click(object sender, EventArgs e)
        {
            if (Common.Account.user == "admin")
            {
                if (user_dgvUser.SelectedRows.Count>1)
                {
                    MessageBox.Show("请选择单行");
                }
                if (user_dgvUser.SelectedRows.Count ==1)
                {
                    if (user_dgvUser.SelectedRows[0].Cells["username"].Value.ToString() == "admin")
                    {
                        MessageBox.Show("非法操作");
                        return;
                    }
                    if (db.DBDelete(DBstr.DeleteStr("Acount", "username", user_dgvUser.SelectedRows[0].Cells["username"].Value.ToString())))
                        MessageBox.Show("删除成功");
                    else
                        MessageBox.Show("删除失败");
                }
                else
                {
                    MessageBox.Show("请选择预删除的行");
                }
                user_btnQuery_Click(null, null);
            }
            else
            {
                MessageBox.Show("当前用户无此权限");
            }          
        }
        private void user_btnChange_Click(object sender, EventArgs e)
        {
            if (Common.Account.user == "admin")
            {
                if (user_dgvUser.SelectedRows.Count == 1)
                {
                    if (!uChange1.IsDisposed)
                    {

                        uChange1.paraT(user_dgvUser.SelectedRows[0].Cells["username"].Value.ToString());
                        uChange1.Show();

                    }
                    else
                    {
                        UserChange uChange2 = new UserChange();
                        uChange2.paraT(user_dgvUser.SelectedRows[0].Cells["username"].Value.ToString());
                        uChange2.Show();
                    }
                }
                else
                {
                    MessageBox.Show("请选择预修改的行");
                }
            }
            else
            {
                MessageBox.Show("当前用户无此权限");
            }
        }
    }
}
