using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.DataManager;
using XT_CETC23.DataCom;
namespace XT_CETC23.UserForms
{
    public partial class UserChange : Form
    {
        string name;
        DataBase db;
        public UserChange()
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            userChange_cbPower.SelectedIndex = 1;
        }
        public void paraT(string str)
        {
            name = str;
            userChange_tbUserName.Text = str;
        }
        private void userChange_btnChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(userChange_tbPassWord.Text))
            {
                if(db.DBConnect())
                {
                    if (0 < db.DBUpdate("update Acount set password = '" + userChange_tbPassWord.Text + "',power='" + userChange_cbPower.SelectedItem.ToString()
                                        + "' where username = '" + userChange_tbUserName.Text + "'")) { MessageBox.Show("修改成功"); this.Close(); }
                    else { MessageBox.Show("修改失败"); }
                }
                else
                {
                    MessageBox.Show("数据库连接失败");
                }       
            }
            else
            {
                MessageBox.Show("非法操作，密码不能为空");
            }
        }
        #region 
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);
        int Radius = 30;
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetFormRoundRectRgn(this, Radius);
        }
        public static void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            int hRgn = 0;
            hRgn = CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            SetWindowRgn(form.Handle, hRgn, true);
            DeleteObject(hRgn);
        }
        #endregion
        #region 
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void UserChange_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        private void userChange_btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
