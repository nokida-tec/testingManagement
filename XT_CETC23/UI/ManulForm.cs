using System;
using System.IO;
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
using System.Threading;
using XT_CETC23.Model;
using XT_CETC23.Instances;

namespace XT_CETC23.SonForm
{
    public partial class ManulForm : Form, IManulForm
    {
        DataBase db;
        Queue<string> mQueue = new System.Collections.Generic.Queue<string>();
        delegate void mCycle(Queue<string> mQueue);
        public delegate void tansMessage(string str);
        public event tansMessage TransMessage;
        
        DataTable dt = new DataTable();
        IAsyncResult result;
        Plc plc;
        Robot robot;
        MTR mtr;
        Thread limitRead;        

        byte[] writeByte = new byte[1];
        byte[] writeByte1=new byte[1];
        byte[] writeByte2 = new byte[1];
        byte[] writeByte3 = new byte[1];
        
        public ManulForm()
        {
            InitializeComponent();
            db = DataBase.GetInstanse();
            mtr = MTR.GetIntanse();
            
            plc = Plc.GetInstanse();
            robot = Robot.GetInstanse();
            
            InitBtnEvent();

            //clearTask();
            limitRead = new Thread(LimitRead);
            //if (!limitRead.IsAlive)
            //{
            //    limitRead.Start();
            //}
        }

        private void LimitRead()
        {
           while(true)
            {
                if (this.IsHandleCreated)
                {
                    Thread.Sleep(100);
                    Cy1Limit();
                    Cy2Limit();
                    Cy3Limit();
                    Cy4Limit();
                    Cy5Limit();
                    Cy6Limit();
                    CyFrame();
                }
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 1#工位
        /// </summary>
        void Cy1Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack1 & 0x01) == 1)
                manul_btnChK1Cy1.Invoke(new Action(() => { manul_btnChK1Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy1.Invoke(new Action(() => { manul_btnChK1Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack1 & 0x02) == 2)
                manul_btnChK1Cy11.Invoke(new Action(() => { manul_btnChK1Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy11.Invoke(new Action(() => { manul_btnChK1Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack1 & 0x04) == 4)
                manul_btnChK1Cy2.Invoke(new Action(() => { manul_btnChK1Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy2.Invoke(new Action(() => { manul_btnChK1Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack1 & 0x08) == 8)
                manul_btnChK1Cy21.Invoke(new Action(() => { manul_btnChK1Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy21.Invoke(new Action(() => { manul_btnChK1Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack1 & 0x10) == 16)
                manul_btnChK1Cy3.Invoke(new Action(() => { manul_btnChK1Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy3.Invoke(new Action(() => { manul_btnChK1Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack1 & 0x20) == 32)
                manul_btnChK1Cy31.Invoke(new Action(() => { manul_btnChK1Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy31.Invoke(new Action(() => { manul_btnChK1Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack1 & 0x40) == 64)
                manul_btnChK1Cy4.Invoke(new Action(() => { manul_btnChK1Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy4.Invoke(new Action(() => { manul_btnChK1Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack1 & 0x80) == 128)
                manul_btnChK1Cy41.Invoke(new Action(() => { manul_btnChK1Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK1Cy41.Invoke(new Action(() => { manul_btnChK1Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 2#工位
        /// </summary>
        void Cy2Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack2 & 0x01) == 1)
                manul_btnChK2Cy1.Invoke(new Action(() => { manul_btnChK2Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy1.Invoke(new Action(() => { manul_btnChK2Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack2 & 0x02) == 2)
                manul_btnChK2Cy11.Invoke(new Action(() => { manul_btnChK2Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy11.Invoke(new Action(() => { manul_btnChK2Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack2 & 0x04) == 4)
                manul_btnChK2Cy2.Invoke(new Action(() => { manul_btnChK2Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy2.Invoke(new Action(() => { manul_btnChK2Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack2 & 0x08) == 8)
                manul_btnChK2Cy21.Invoke(new Action(() => { manul_btnChK2Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy21.Invoke(new Action(() => { manul_btnChK2Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack2 & 0x10) == 16)
                manul_btnChK2Cy3.Invoke(new Action(() => { manul_btnChK2Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy3.Invoke(new Action(() => { manul_btnChK2Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack2 & 0x20) == 32)
                manul_btnChK2Cy31.Invoke(new Action(() => { manul_btnChK2Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy31.Invoke(new Action(() => { manul_btnChK2Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack2 & 0x40) == 64)
                manul_btnChK2Cy4.Invoke(new Action(() => { manul_btnChK2Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy4.Invoke(new Action(() => { manul_btnChK2Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack2 & 0x80) == 128)
                manul_btnChK2Cy41.Invoke(new Action(() => { manul_btnChK2Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK2Cy41.Invoke(new Action(() => { manul_btnChK2Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 3#工位
        /// </summary>
        void Cy3Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack3 & 0x01) == 1)
                manul_btnChK3Cy1.Invoke(new Action(() => { manul_btnChK3Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy1.Invoke(new Action(() => { manul_btnChK3Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack3 & 0x02) == 2)
                manul_btnChK3Cy11.Invoke(new Action(() => { manul_btnChK3Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy11.Invoke(new Action(() => { manul_btnChK3Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack3 & 0x04) == 4)
                manul_btnChK3Cy2.Invoke(new Action(() => { manul_btnChK3Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy2.Invoke(new Action(() => { manul_btnChK3Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack3 & 0x08) == 8)
                manul_btnChK3Cy21.Invoke(new Action(() => { manul_btnChK3Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy21.Invoke(new Action(() => { manul_btnChK3Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack3 & 0x10) == 16)
                manul_btnChK3Cy3.Invoke(new Action(() => { manul_btnChK3Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy3.Invoke(new Action(() => { manul_btnChK3Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack3 & 0x20) == 32)
                manul_btnChK3Cy31.Invoke(new Action(() => { manul_btnChK3Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy31.Invoke(new Action(() => { manul_btnChK3Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack3 & 0x40) == 64)
                manul_btnChK3Cy4.Invoke(new Action(() => { manul_btnChK3Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy4.Invoke(new Action(() => { manul_btnChK3Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack3 & 0x80) == 128)
                manul_btnChK3Cy41.Invoke(new Action(() => { manul_btnChK3Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK3Cy41.Invoke(new Action(() => { manul_btnChK3Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 4#工位
        /// </summary>
        void Cy4Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack4 & 0x01) == 1)
                manul_btnChK4Cy1.Invoke(new Action(() => { manul_btnChK4Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy1.Invoke(new Action(() => { manul_btnChK4Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack4 & 0x02) == 2)
                manul_btnChK4Cy11.Invoke(new Action(() => { manul_btnChK4Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy11.Invoke(new Action(() => { manul_btnChK4Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack4 & 0x04) == 4)
                manul_btnChK4Cy2.Invoke(new Action(() => { manul_btnChK4Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy2.Invoke(new Action(() => { manul_btnChK4Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack4 & 0x08) == 8)
                manul_btnChK4Cy21.Invoke(new Action(() => { manul_btnChK4Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy21.Invoke(new Action(() => { manul_btnChK4Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack4 & 0x10) == 16)
                manul_btnChK4Cy3.Invoke(new Action(() => { manul_btnChK4Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy3.Invoke(new Action(() => { manul_btnChK4Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack4 & 0x20) == 32)
                manul_btnChK4Cy31.Invoke(new Action(() => { manul_btnChK4Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy31.Invoke(new Action(() => { manul_btnChK4Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack4 & 0x40) == 64)
                manul_btnChK4Cy4.Invoke(new Action(() => { manul_btnChK4Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy4.Invoke(new Action(() => { manul_btnChK4Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack4 & 0x80) == 128)
                manul_btnChK4Cy41.Invoke(new Action(() => { manul_btnChK4Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK4Cy41.Invoke(new Action(() => { manul_btnChK4Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 5#工位
        /// </summary>
        void Cy5Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack5 & 0x01) == 1)
                manul_btnChK5Cy1.Invoke(new Action(() => { manul_btnChK5Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy1.Invoke(new Action(() => { manul_btnChK5Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack5 & 0x02) == 2)
                manul_btnChK5Cy11.Invoke(new Action(() => { manul_btnChK5Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy11.Invoke(new Action(() => { manul_btnChK5Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack5 & 0x04) == 4)
                manul_btnChK5Cy2.Invoke(new Action(() => { manul_btnChK5Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy2.Invoke(new Action(() => { manul_btnChK5Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack5 & 0x08) == 8)
                manul_btnChK5Cy21.Invoke(new Action(() => { manul_btnChK5Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy21.Invoke(new Action(() => { manul_btnChK5Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack5 & 0x10) == 16)
                manul_btnChK5Cy3.Invoke(new Action(() => { manul_btnChK5Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy3.Invoke(new Action(() => { manul_btnChK5Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack5 & 0x20) == 32)
                manul_btnChK5Cy31.Invoke(new Action(() => { manul_btnChK5Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy31.Invoke(new Action(() => { manul_btnChK5Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack5 & 0x40) == 64)
                manul_btnChK5Cy4.Invoke(new Action(() => { manul_btnChK5Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy4.Invoke(new Action(() => { manul_btnChK5Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack5 & 0x80) == 128)
                manul_btnChK5Cy41.Invoke(new Action(() => { manul_btnChK5Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK5Cy41.Invoke(new Action(() => { manul_btnChK5Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 6#工位
        /// </summary>
        void Cy6Limit()
        {
            //1#气缸
            if ((PlcData._limitFeedBack6 & 0x01) == 1)
                manul_btnChK6Cy1.Invoke(new Action(() => { manul_btnChK6Cy1.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy1.Invoke(new Action(() => { manul_btnChK6Cy1.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack6 & 0x02) == 2)
                manul_btnChK6Cy11.Invoke(new Action(() => { manul_btnChK6Cy11.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy11.Invoke(new Action(() => { manul_btnChK6Cy11.BackColor = Color.PowderBlue; }));
            //2#气缸
            if ((PlcData._limitFeedBack6 & 0x04) == 4)
                manul_btnChK6Cy2.Invoke(new Action(() => { manul_btnChK6Cy2.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy2.Invoke(new Action(() => { manul_btnChK6Cy2.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack5 & 0x08) == 8)
                manul_btnChK6Cy21.Invoke(new Action(() => { manul_btnChK6Cy21.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy21.Invoke(new Action(() => { manul_btnChK6Cy21.BackColor = Color.PowderBlue; }));
            //3#气缸
            if ((PlcData._limitFeedBack6 & 0x10) == 16)
                manul_btnChK6Cy3.Invoke(new Action(() => { manul_btnChK6Cy3.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy3.Invoke(new Action(() => { manul_btnChK6Cy3.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack6 & 0x20) == 32)
                manul_btnChK6Cy31.Invoke(new Action(() => { manul_btnChK6Cy31.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy31.Invoke(new Action(() => { manul_btnChK6Cy31.BackColor = Color.PowderBlue; }));
            //4#气缸
            if ((PlcData._limitFeedBack6 & 0x40) == 64)
                manul_btnChK6Cy4.Invoke(new Action(() => { manul_btnChK6Cy4.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy4.Invoke(new Action(() => { manul_btnChK6Cy4.BackColor = Color.PowderBlue; }));
            if ((PlcData._limitFeedBack6 & 0x80) == 128)
                manul_btnChK6Cy41.Invoke(new Action(() => { manul_btnChK6Cy41.BackColor = Color.ForestGreen; }));
            else
                manul_btnChK6Cy41.Invoke(new Action(() => { manul_btnChK6Cy41.BackColor = Color.PowderBlue; }));
        }

        /// <summary>
        /// 料架气缸
        /// </summary>
        void CyFrame()
        {
            //夹紧气缸，两个气缸并联检测
            if ((PlcData._frameFeedBack & 0x02) == 2 && (PlcData._frameFeedBack & 0x08) == 8)
                manul_btnFrameGrab1.Invoke(new Action(() => { manul_btnFrameGrab1.BackColor = Color.ForestGreen; }));
            else
                manul_btnFrameGrab1.Invoke(new Action(() => { manul_btnFrameGrab1.BackColor = Color.PowderBlue; }));
            if ((PlcData._frameFeedBack & 0x04) == 4 && (PlcData._frameFeedBack & 0x10) == 16)
                manul_btnFrameRealese1.Invoke(new Action(() => { manul_btnFrameRealese1.BackColor = Color.ForestGreen; }));
            else
                manul_btnFrameRealese1.Invoke(new Action(() => { manul_btnFrameRealese1.BackColor = Color.PowderBlue; }));
            //取料气缸
            if ((PlcData._frameFeedBack & 0x20) == 32)
                manul_btnFrameGrab2.Invoke(new Action(() => { manul_btnFrameGrab2.BackColor = Color.ForestGreen; }));
            else
                manul_btnFrameGrab2.Invoke(new Action(() => { manul_btnFrameGrab2.BackColor = Color.PowderBlue; }));
            if ((PlcData._frameFeedBack & 0x40) == 64)
                manul_btnFrameRealese2.Invoke(new Action(() => { manul_btnFrameRealese2.BackColor = Color.ForestGreen; }));
            else
                manul_btnFrameRealese2.Invoke(new Action(() => { manul_btnFrameRealese2.BackColor = Color.PowderBlue; }));
        }

        void InitBtnEvent()                 //按钮事件注册
        {
            manul_btnAxlis7Forward.MouseDown += Manul_btn_MouseDown;
            manul_btnAxlis7Reverse.MouseDown += Manul_btn_MouseDown;
            manul_btnFrameForward.MouseDown += Manul_btn_MouseDown;
            manul_btnFrameReverse.MouseDown += Manul_btn_MouseDown;
            manul_btnFrameUp.MouseDown += Manul_btn_MouseDown;
            manul_btnFrameDown.MouseDown += Manul_btn_MouseDown;


            manul_btnAxlis7Forward.MouseUp += Manul_btn_MouseUp;
            manul_btnAxlis7Reverse.MouseUp += Manul_btn_MouseUp;
            manul_btnFrameForward.MouseUp += Manul_btn_MouseUp;
            manul_btnFrameReverse.MouseUp += Manul_btn_MouseUp;
            manul_btnFrameUp.MouseUp += Manul_btn_MouseUp;
            manul_btnFrameDown.MouseUp += Manul_btn_MouseUp;


            manul_btnAxlis7Home.Click += Manul_btn_Click;
            manul_btnFrameHome1.Click += Manul_btn_Click;
            manul_btnFrameHome2.Click += Manul_btn_Click;
            manul_btnFrameGrab1.Click += Manul_btn_Click;
            manul_btnFrameRealese1.Click += Manul_btn_Click;
            manul_btnFrameGrab2.Click += Manul_btn_Click;
            manul_btnFrameRealese2.Click += Manul_btn_Click;

            manul_btnChK1Cy1.Click += Manul_btn_Click;
            manul_btnChK1Cy2.Click += Manul_btn_Click;
            manul_btnChK1Cy3.Click += Manul_btn_Click;
            manul_btnChK1Cy4.Click += Manul_btn_Click;
            manul_btnChK2Cy1.Click += Manul_btn_Click;
            manul_btnChK2Cy2.Click += Manul_btn_Click;
            manul_btnChK2Cy3.Click += Manul_btn_Click;
            manul_btnChK2Cy4.Click += Manul_btn_Click;
            manul_btnChK3Cy1.Click += Manul_btn_Click;
            manul_btnChK3Cy2.Click += Manul_btn_Click;
            manul_btnChK3Cy3.Click += Manul_btn_Click;
            manul_btnChK3Cy4.Click += Manul_btn_Click;
            manul_btnChK4Cy1.Click += Manul_btn_Click;
            manul_btnChK4Cy2.Click += Manul_btn_Click;
            manul_btnChK4Cy3.Click += Manul_btn_Click;
            manul_btnChK4Cy4.Click += Manul_btn_Click;
            manul_btnChK5Cy1.Click += Manul_btn_Click;
            manul_btnChK5Cy2.Click += Manul_btn_Click;
            manul_btnChK5Cy3.Click += Manul_btn_Click;
            manul_btnChK5Cy4.Click += Manul_btn_Click;
            manul_btnChK6Cy1.Click += Manul_btn_Click;
            manul_btnChK6Cy2.Click += Manul_btn_Click;
            manul_btnChK6Cy3.Click += Manul_btn_Click;
            manul_btnChK6Cy4.Click += Manul_btn_Click;

            manul_btnChK1Cy11.Click += Manul_btn_Click;
            manul_btnChK1Cy21.Click += Manul_btn_Click;
            manul_btnChK1Cy31.Click += Manul_btn_Click;
            manul_btnChK1Cy41.Click += Manul_btn_Click;
            manul_btnChK2Cy11.Click += Manul_btn_Click;
            manul_btnChK2Cy21.Click += Manul_btn_Click;
            manul_btnChK2Cy31.Click += Manul_btn_Click;
            manul_btnChK2Cy41.Click += Manul_btn_Click;
            manul_btnChK3Cy11.Click += Manul_btn_Click;
            manul_btnChK3Cy31.Click += Manul_btn_Click;
            manul_btnChK3Cy21.Click += Manul_btn_Click;
            manul_btnChK3Cy41.Click += Manul_btn_Click;
            manul_btnChK4Cy11.Click += Manul_btn_Click;
            manul_btnChK4Cy21.Click += Manul_btn_Click;
            manul_btnChK4Cy31.Click += Manul_btn_Click;
            manul_btnChK4Cy41.Click += Manul_btn_Click;
            manul_btnChK5Cy11.Click += Manul_btn_Click;
            manul_btnChK5Cy21.Click += Manul_btn_Click;
            manul_btnChK5Cy31.Click += Manul_btn_Click;
            manul_btnChK5Cy41.Click += Manul_btn_Click;
            manul_btnChK6Cy11.Click += Manul_btn_Click;
            manul_btnChK6Cy21.Click += Manul_btn_Click;
            manul_btnChK6Cy31.Click += Manul_btn_Click;
            manul_btnChK6Cy41.Click += Manul_btn_Click;


            //manul_btnFrameRealese.Click += Manul_btn_Click;
            //manul_btnFrameHome2.Click += Manul_btn_Click;
            //manul_btnRbSukerV1.Click += Manul_btn_Click;
            //manul_btnFrameRealese2.Click += Manul_btn_Click;
            //manul_btnFrameReverse.Click += Manul_btn_Click;
            ////manul_btnRbSukerV.Click += Manul_btn_Click;
            //manul_btnFrameHome1.Click += Manul_btn_Click;
            //manul_btnRbSukerB1.Click += Manul_btn_Click;
            //manul_btnRbSukerV2.Click += Manul_btn_Click;
            //manul_btnRbSukerB2.Click += Manul_btn_Click;
            //manul_btnSpare5.Click += Manul_btn_Click;
            ////manul_btnStartT.Click += Manul_btnCabinet_Click;
            ////manul_btnStopT.Click += Manul_btnCabinet_Click;
        }

        private void Manul_btn_MouseUp(object sender, MouseEventArgs e)                 //伺服运动控制
        {
            //PLC已经被初始化
            if (plc.plcConnected)
            {
                //if (PlcData._plcMode == 22 || PlcData._plcMode == 23)
                //{
                    switch ((sender as Button).Name)
                    {
                        case "manul_btnAxlis7Forward":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnAxlis7Reverse":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameForward":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameReverse":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameUp":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameDown":
                            writeByte[0] = 0;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            break;
                    }
                //}
                //else
                //{
                //    MessageBox.Show("请选择PLC模式为手动");
                //}
            }
            else
            {
                MessageBox.Show("请先初始化PLC");
            }
        }

        private void Manul_btn_MouseDown(object sender, MouseEventArgs e)           //伺服运动控制
        { 
            //PLC已经被初始化
            if (plc.plcConnected)
            {              
                //if (PlcData._plcMode == 22 || PlcData._plcMode == 23)
                //{
                    switch ((sender as Button).Name)
                    {
                        case "manul_btnAxlis7Forward":
                            writeByte[0] = (byte)PlcData._writeAxlis7Forward;
                           manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnAxlis7Reverse":
                            writeByte[0] = (byte)PlcData._writeAxlis7Backward;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameForward":
                            writeByte[0] = (byte)PlcData._writeFrameForward;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameReverse":
                            writeByte[0] = (byte)PlcData._writeFrameReverse;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameUp":
                            writeByte[0] = (byte)PlcData._writeFrameUp;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameDown":
                            writeByte[0] = (byte)PlcData._writeFrameDown;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            break;
                    }
                //}
                //else
                //{
                //    MessageBox.Show("请选择PLC模式为手动");
                //}
            }
            else
            {
                MessageBox.Show("请先初始化PLC");
            }
        }

        private void Manul_btnCabinet_Click(object sender, EventArgs e)
        {
            //检测柜信号，文本写入
            throw new NotImplementedException();
        }

        private void Manul_btn_Click(object sender, EventArgs e)
        {
            //PLC模式手动
            if (plc.plcConnected)
            {
            //    if (PlcData._plcMode==22|| PlcData._plcMode == 23)
            //{
                //PLC已经被初始化

                   switch ((sender as Button).Name)
                    {                     
                        case "manul_btnAxlis7Home"://7轴回原点
                            writeByte[0] = (byte)PlcData._writeAxlis7Home;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        //case "manul_btnRbSukerV"://吸盘真空
                        //    //writeByte[0] = (byte)PlcData._writeRobotVaccu;
                        //    manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                        //    break;
                        //case "manul_btnRbSukerB"://吸盘吹气
                        //    //writeByte[0] = (byte)PlcData._writeRobotBlow;
                        //    manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                        //    break;
                   
                        case "manul_btnFrameHome1"://横移回原点
                            writeByte[0] = (byte)PlcData._writeFrameHome1;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                      
                        case "manul_btnFrameHome2"://升降回原点
                            writeByte[0] = (byte)PlcData._writeFrameHome2;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder1, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameGrab1"://固定气缸伸出
                            writeByte[0] = (byte)PlcData._writeFrameGrab1;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            writeByte1[0] = (byte)PlcData._writeFrameGrab3;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte1);
                            break;
                        case "manul_btnFrameRealese1"://固定气缸缩回
                            writeByte[0] = (byte)PlcData._writeFrameRealese1;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            writeByte1[0] = (byte)PlcData._writeFrameRealese3;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte1);
                            break;
                        case "manul_btnFrameGrab2"://取料气缸伸出
                            writeByte[0] = (byte)PlcData._writeFrameGrab2;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            break;
                        case "manul_btnFrameRealese2"://取料气缸缩回
                            writeByte[0] = (byte)PlcData._writeFrameRealese2;
                            manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder2, PlcData._writeLength1, writeByte);
                            break;
                    }
                    #region 1#气缸操作
                    if ((sender as Button).Tag== "1#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                        this.manul_btnChK1Cy1.BackColor = Color.Green;
                    }
                    if ((sender as Button).Tag == "1#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "1#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "1#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "1#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "1#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "1#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "1#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet1CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder3, PlcData._writeLength1, writeByte1);
                    }
                    #endregion
                    #region 2#气缸操作
                    if ((sender as Button).Tag == "2#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                        manul_btnChK2Cy1.BackColor = Color.Green;
                        manul_btnChK2Cy11.BackColor = Color.PowderBlue;
                    }
                    if ((sender as Button).Tag == "2#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                        manul_btnChK2Cy1.BackColor = Color.PowderBlue;
                        manul_btnChK2Cy11.BackColor = Color.Green;
                    }

                    if ((sender as Button).Text == "2#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "2#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "2#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "2#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "2#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "2#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet2CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder4, PlcData._writeLength1, writeByte1);
                    }
                    #endregion
                    #region 3#气缸操作
                    if ((sender as Button).Tag == "3#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Tag == "3#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "3#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "3#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "3#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "3#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "3#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "3#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet3CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder5, PlcData._writeLength1, writeByte1);
                        
                    }
                    #endregion
                    #region 4#气缸操作
                    if ((sender as Button).Tag == "4#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Tag == "4#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "4#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "4#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "4#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "4#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "4#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "4#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet4CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder6, PlcData._writeLength1, writeByte1);
                    }
                    #endregion
                    #region 5#气缸操作
                    if ((sender as Button).Tag == "5#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Tag == "5#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "5#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "5#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "5#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "5#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                         Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "5#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "5#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet5CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder7, PlcData._writeLength1, writeByte1);
                    }
                    #endregion
                    #region 6#气缸操作
                    if ((sender as Button).Tag == "6#位1#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY1Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Tag == "6#位1#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY1Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "6#位2#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY2Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "6#位2#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY2Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }

                    if ((sender as Button).Text == "6#位3#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY3Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "6#位3#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY3Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "6#位4#气缸伸出")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY4Extend;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    if ((sender as Button).Text == "6#位4#气缸缩回")
                    {
                        writeByte[0] = (byte)PlcData._writeCabinet6CY4Back;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte);
                        Thread.Sleep(500);
                        writeByte1[0] = 0;
                        manulWrite(PlcData.PlcWriteAddress, PlcData._writeManulOrder8, PlcData._writeLength1, writeByte1);
                    }
                    #endregion
                //}
                //else
                //{ MessageBox.Show("请选择手动模式"); }
            }
            else
            { MessageBox.Show("请连接PLC");  }
                    
        }



        bool manulWrite(int DBAddress,int Start,int Size,byte[] Data)
        {
            return plc.DBWrite(DBAddress, Start, Size, Data);
        }

        //private void manul_tab_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    //标签背景填充颜色
        //    SolidBrush BackBrush = new SolidBrush(Color.LightBlue);
        //    //标签文字填充颜色
        //    SolidBrush FrontBrush = new SolidBrush(Color.Black);
        //    StringFormat StringF = new StringFormat();
        //    //设置文字对齐方式
        //    StringF.Alignment = StringAlignment.Center;
        //    StringF.LineAlignment = StringAlignment.Center;

        //    for (int i = 0; i < manul_tab.TabPages.Count; i++)
        //    {
        //        //获取标签头工作区域
        //        Rectangle Rec = manul_tab.GetTabRect(i);
        //        //绘制标签头背景颜色
        //        e.Graphics.FillRectangle(BackBrush, Rec);
        //        e.Graphics.DrawString(manul_tab.TabPages[i].Text, new Font("宋体", 12), FrontBrush, Rec, StringF);
        //    }
        //}
      
        public void clearTask()
        {
            try
            {
                PlcData.clearTask = true;
                //TransMessage("任务清除完成");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                PlcData.clearTask = false;
                TransMessage("任务清除失败");
            }
        }

        private void ManulForm_Enter(object sender, EventArgs e)
        {
            if (!limitRead.IsAlive && limitRead.ThreadState==ThreadState.Unstarted)
            {
                limitRead.Start();
            }
            if (limitRead.IsAlive && limitRead.ThreadState==ThreadState.Suspended)
            {
                limitRead.Resume();
            }
        }

        private void ManulForm_Leave(object sender, EventArgs e)
        {
            if(limitRead.IsAlive && limitRead.ThreadState == ThreadState.Running)
            {
                limitRead.Suspend();
            }
        }

        private void ManulForm_Load(object sender, EventArgs e)
        {
            limitRead.Start();
        }
    }
}
