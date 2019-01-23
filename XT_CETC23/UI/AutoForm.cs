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

namespace XT_CETC23.SonForm
{
    public partial class AutoForm : Form,IAutoForm
    {
        bool first = false;
        public DataBase db;
        Run run;
        TextBox[] tbSort,tbNum;
        public AutoForm()
        {
            InitializeComponent();
            #region
            tbSort = new TextBox[] { auto_tbSortRC11, auto_tbSortRC12, auto_tbSortRC13, auto_tbSortRC14, auto_tbSortRC15,
                                     auto_tbSortRC21, auto_tbSortRC22, auto_tbSortRC23, auto_tbSortRC24, auto_tbSortRC25,
                                     auto_tbSortRC31, auto_tbSortRC32, auto_tbSortRC33, auto_tbSortRC34, auto_tbSortRC35,
                                     auto_tbSortRC41, auto_tbSortRC42, auto_tbSortRC43, auto_tbSortRC44, auto_tbSortRC45,
                                     auto_tbSortRC51, auto_tbSortRC52, auto_tbSortRC53, auto_tbSortRC54, auto_tbSortRC55,
                                     auto_tbSortRC61, auto_tbSortRC62, auto_tbSortRC63, auto_tbSortRC64, auto_tbSortRC65,
                                     auto_tbSortRC71, auto_tbSortRC72, auto_tbSortRC73, auto_tbSortRC74, auto_tbSortRC75,   
                                     auto_tbSortRC81, auto_tbSortRC82, auto_tbSortRC83, auto_tbSortRC84, auto_tbSortRC85};
            tbNum = new TextBox[] { auto_tbNumRC11, auto_tbNumRC12, auto_tbNumRC13, auto_tbNumRC14, auto_tbNumRC15,
                                    auto_tbNumRC21, auto_tbNumRC22, auto_tbNumRC23, auto_tbNumRC24, auto_tbNumRC25,
                                    auto_tbNumRC31, auto_tbNumRC32, auto_tbNumRC33, auto_tbNumRC34, auto_tbNumRC35,
                                    auto_tbNumRC41, auto_tbNumRC42, auto_tbNumRC43, auto_tbNumRC44, auto_tbNumRC45,
                                    auto_tbNumRC51, auto_tbNumRC52, auto_tbNumRC53, auto_tbNumRC54, auto_tbNumRC55,
                                    auto_tbNumRC61, auto_tbNumRC62, auto_tbNumRC63, auto_tbNumRC64, auto_tbNumRC65,
                                    auto_tbNumRC71, auto_tbNumRC72, auto_tbNumRC73, auto_tbNumRC74, auto_tbNumRC75,
                                    auto_tbNumRC81, auto_tbNumRC82, auto_tbNumRC83, auto_tbNumRC84, auto_tbNumRC85};
            #endregion
            InitForm();
            this.run = run;
        }
        void InitForm()
        {
            db = DataBase.GetInstanse();
        }
        void initResize()
        {

        }
        private void AutoForm_Resize(object sender, EventArgs e)
        {
           
        }
        public void getSort(string[] str, string[] str1)
        {
            throw new NotImplementedException();
        }

        private void AutoForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            frameDataUpdate();
        }

        public void frameDataUpdate()
        {
            string sort;
            int numRemain, numOK, numNG;
            DataTable dtFeedBin = db.DBQuery("select * from dbo.FeedBin");
            for(int i=0;i<40;i++)
            {
                sort = dtFeedBin.Rows[i]["Sort"].ToString().Trim();
                numRemain = (int)Convert.ToDouble(dtFeedBin.Rows[i]["NumRemain"]);
                numOK = (int)Convert.ToDouble(dtFeedBin.Rows[i]["ResultOK"]);
                numNG = (int)Convert.ToDouble(dtFeedBin.Rows[i]["ResultNG"]);

                int colNo = i / 8;
                int rowNo = i % 8;
                int trayNo = (rowNo + 1) * 10 + (colNo + 1);

                string ctr1Name = "auto_tbSortRC" + trayNo.ToString().Trim();
                string ctr2Name = "auto_tbNumRC" + trayNo.ToString().Trim();
                foreach(Control con in auto_pnGrid.Controls)
                {
                    if(con.Name== ctr1Name)
                    {
                        con.Text = sort;
                    }
                    if(con.Name==ctr2Name)
                    {
                        string strTmp = numRemain.ToString().Trim()   + "/" + numOK.ToString().Trim() + "/" + numNG.ToString().Trim();
                        con.Text = strTmp;
                    }
                }
            }                           
        }
    }
}
