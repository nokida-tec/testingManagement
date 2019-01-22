﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XT_CETC23.INTransfer;
using XT_CETC23.DataCom;
using XT_CETC23.DataManager;
namespace XT_CETC23.UserForms
{
    public partial class UserLogin : Form
    {
        DataBase db;
        IUserForm iUserForm;
        DataTable dt;
        public UserLogin(IUserForm iUserForm)
        {
            InitializeComponent();
            this.iUserForm = iUserForm;
            db = DataBase.GetInstanse();
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
        private void UserLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        private void userLogin_btnLogin_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(userLogin_tbUserName.Text)&&!string.IsNullOrEmpty(userLogin_tbUserPassWord.Text))
            {
               dt= db.DBQuery(DBstr.QueryStr("Acount"));
                try
                {
                    for(int i=0;i<dt.Rows.Count;++i)
                    {
                        if (dt.Rows[i]["username"].ToString() == userLogin_tbUserName.Text&& dt.Rows[i]["password"].ToString() == userLogin_tbUserPassWord.Text)
                        {
                            iUserForm.getAcount(new string[] { userLogin_tbUserName.Text, dt.Rows[i]["power"].ToString() });
                            //Common.Account.user = userLogin_tbUserName.Text;
                            //Common.Account.power = dt.Rows[i]["power"].ToString();
                            MessageBox.Show("登录成功");
                            this.Close();
                            return;
                        }
                    }
                    MessageBox.Show("密码错误，请重新输入");
                }catch(DataException dex)
                {
                    MessageBox.Show(dex.Message.ToString());
                }
            }
        }

        private void userLogin_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
