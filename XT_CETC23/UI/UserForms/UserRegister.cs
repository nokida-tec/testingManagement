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
using XT_CETC23.DataCom;
using XT_CETC23.DataManager;
namespace XT_CETC23.UserForms
{
    public partial class UserRegister : Form
    {
        DataBase db;
        DataTable dt;
        public UserRegister()
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            userChange_cbPower.SelectedIndex = 1;
        }

        private void userChange_btnInsert_Click(object sender, EventArgs e)
        {
            if(db.DBConnect())
            {
                if(!string.IsNullOrEmpty(userChange_tbUserName.Text)&&!string.IsNullOrEmpty(userChange_tbPassWord.Text))
                {
                   dt= db.DBQuery(DBstr.QueryStr("Acount"));
                    for(int i=0;i<dt.Rows.Count;++i)
                    {
                        if(dt.Rows[i]["username"].ToString()== userChange_tbUserName.Text)
                        {
                            MessageBox.Show("帐户已经存在");
                            return;
                        }
                    }
                    try
                    {
                        db.DBInsert(DBstr.InsertStrUser("Acount", new string[] { "username", "password", "power" }, new string[] { userChange_tbUserName.Text, userChange_tbPassWord.Text, userChange_cbPower.SelectedItem.ToString() }));
                        MessageBox.Show("帐户添加成功");
                        this.Close();
                    }
                    catch(DataException dex)
                    {
                        MessageBox.Show(dex.Message.ToString());
                    }
                  
                }
            }
        }
        private void userChange_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void UserRegister_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion


    }
}
