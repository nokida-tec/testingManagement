using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.IO;
using XT_CETC23.DataManager;
using XT_CETC23_GK.Task;
using XT_CETC23.Common;
using XT_CETC23.Model;
using XT_CETC23.Instances;
using Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace XT_CETC23.DataCom
{
    class TaskCycle
    {
        // 
        [DllImport("user32.dll", EntryPoint = "FindWindowA", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowExA", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETTEXT = 0x000C;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int vbKeyReturn = 0x0D;

        Thread axlis2Task, axlis7Task, robotTask, cabinetTask;
        static TaskCycle taskCycle;
        static object lockTaskCycle = new object();
        static String[] prodType = new String[40];
        public static string feedBinScanDone = "";
        public static string actionType="";
        public static int MainStep;
        public static int PickStep;
        public static int PutStep;
        public static bool scanStatus = false;

        DataBase db;
        DataTable dt;
        Plc plc;
        Robot robot;
        MTR mtr;
        DataTable dt2, dt7, dtr;

        //Task task;
        XT_CETC23_GK.Task.RobotTask rTask = XT_CETC23_GK.Task.RobotTask.GetInstanse();

        public static TaskCycle GetInstanse()
        {
            if (taskCycle == null)
            {
                lock (lockTaskCycle)
                {
                    if (taskCycle == null)
                    {
                        taskCycle = new TaskCycle();
                    }
                }
            }
            return taskCycle;
        }

        TaskCycle()
        {
            db = DataBase.GetInstanse();
            plc = Plc.GetInstanse();
            robot = Robot.GetInstanse();
            mtr = MTR.GetIntanse();
            axlis2Task = new Thread(Axlis2Task);
            axlis2Task.Name = "2轴任务";
            //if (PlcData._plcMode == 25)
            //{
            if (!axlis2Task.IsAlive)
            {
                axlis2Task.Start();
            }
            axlis7Task = new Thread(Axlis7Task);
            axlis7Task.Name = "7轴任务";
            if (!axlis7Task.IsAlive)
            {
                axlis7Task.Start();
            }
            robotTask = new Thread(RobotTask);
            robotTask.Name = "机器人任务";
            if (!robotTask.IsAlive)
            {
                robotTask.Start();
            }
        }

        private void RobotTask()
        {
            string cordX = "";
            string cordY = "";
            string cordU = "";
            dtr = new DataTable();
            while (true)
            {
                Thread.Sleep(10);
                while (PlcData.clearTask)
                {
                    dtr.Rows.Clear();
                    dtr.Columns.Clear();
                    dtr = db.DBQuery("select * from dbo.TaskRobot");
                    if (dtr.Rows.Count == 1)
                    {
                        //rTask.Axlis7Pos = (int)dtr.Rows[0]["Axlis7Pos"];
                        rTask.OrderType = dtr.Rows[0]["OrderType"].ToString().Trim() ;
                        RobotData.Command = rTask.OrderType;
                        rTask.ProductType = dtr.Rows[0]["ProductType"].ToString().Trim();
                        rTask.Position = (int)dtr.Rows[0]["SalverLocation"];
                        cordX = dtr.Rows[0]["CordinatorX"].ToString().Trim();
                        cordY = dtr.Rows[0]["CordinatorY"].ToString().Trim();
                        cordU = dtr.Rows[0]["CordinatorU"].ToString().Trim();

                        if (rTask.OrderType == "GetProTray")
                        {
                            robot.sendDataToRobot(rTask.OrderType + "," + rTask.ProductType + "," + rTask.Position.ToString() + "," + cordX + "," + cordY + "," + cordU);

                            //等待机器人触发扫码
                            while (RobotData.Response != "ScanStart")
                            {
                                Thread.Sleep(100);
                            }

                            //通知Plc扫码
                            plc.DBWrite(PlcData.PlcWriteAddress, 3, 1, new Byte[] { 33 });

                            //等待Plc扫码完成
                            while ((PlcData._axlis2Status != 33) && (PlcData._axlis2Status != 38))
                            {
                                Thread.Sleep(100);
                            }

                            if (PlcData._axlis2Status  == 38)
                            {
                                scanStatus = false;
                            }
                            if (PlcData._axlis2Status == 33)
                            {
                                scanStatus = true;
                            }
                            RobotData.Response = "";
                            robot.sendDataToRobot("ScanDone");              //给机器人发送扫码完成消息 
                        }
                        else
                        {
                            robot.sendDataToRobot(rTask.OrderType + "," + rTask.ProductType + "," + rTask.Position.ToString());
                        }
                       
                        //等待机器人取料完成消息
                        String rspMsg = RobotData.Command.Trim() + "Done";
                        while (String.IsNullOrEmpty(RobotData.Response)) 
                        {
                            Thread.Sleep(100);
                        }
                        while (!RobotData.Response.Trim().Equals(RobotData.Command.Trim() + "Done"))
                        {
                            Thread.Sleep(100);
                        }
                        db.DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                        db.DBDelete("delete from dbo.TaskRobot");
                        Thread.Sleep(1000);
                        if (TaskCycle.actionType == "FrameToCabinet")
                        {
                            TaskCycle.PickStep = PickStep + 10;
                        }
                        if (TaskCycle.actionType == "CabinetToFrame")
                        {
                            TaskCycle.PutStep = PutStep + 10;
                        }
                        RobotData.Command = "";
                        RobotData.Response = "";
                    }
                    else if(dtr.Rows.Count>1)
                    {
                        MessageBox.Show("任务队列异常，请查看数据库表格TaskRoot，正常情况下该表格中最多只有一条任务记录！");
                    }
                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }
        }

        int a;
        private void Axlis7Task()
        {
            dt7 = new DataTable();
            while (true)
            {
                Thread.Sleep(10);
                while (PlcData.clearTask)
                {
                    dt7.Rows.Clear();
                    dt7.Columns.Clear();
                    dt7 = db.DBQuery("select * from dbo.TaskAxlis7");
                    if (dt7.Rows.Count == 1)
                    {
                        a = Convert.ToInt32(dt7.Rows[0]["Axlis7Pos"]);
                        plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis7Pos, PlcData._writeLength1, new byte[] { Convert.ToByte(a) });
                        while (PlcData._axlis7Status != (byte)55)
                        {
                            Thread.Sleep(100);
                        }
                        db.DBDelete("delete from dbo.TaskAxlis7 where Axlis7Pos=" + a + "");
                        Thread.Sleep(2000);
                        plc.DBWrite(100, 2, 1, new Byte[] { 0 });
                        if (TaskCycle.actionType == "FrameToCabinet")
                        {
                            TaskCycle.PickStep = TaskCycle.PickStep + 10;
                        }
                        if (TaskCycle.actionType == "CabinetToFrame")
                        {
                            TaskCycle.PutStep = TaskCycle.PutStep + 10;
                        }
                    }
                    else if(dtr.Rows.Count > 1)
                    {
                        MessageBox.Show("任务队列异常，请查看数据库表格TaskAxlis7，正常情况下该表格中最多只有一条任务记录！");
                    }
                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }            
        }

        private void Axlis2Task()
        {
            dt2 = new DataTable();
            while (true)
            {
                Thread.Sleep(10);
                while (PlcData.clearTask)
                {
                    dt2 = db.DBQuery("select * from dbo.TaskAxlis2");
                    DataTable dt3 = db.DBQuery("select * from dbo.SortData");
                    if (dt2.Rows.Count == 1)
                    {
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.ScanSort)
                        {
                            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.ScanSort });
                            while (PlcData._axlis2Status != (byte)EnumC.Frame.ScanSort)
                            {
                                Thread.Sleep(100);
                            }
                            db.DBDelete("delete from dbo.TaskAxlis2 where orderName=" + (short)EnumC.FrameW.ScanSort + "");
                            dt2.Rows.Clear();
                            dt2.Columns.Clear();

                            Byte[] mySort=new Byte[504];
                            mySort = plc.DbRead(104, 0, 504);
                            Thread.Sleep(2000);
                            plc.DBWrite(100, 3, 1, new Byte[] { 0 });
                        
                            for (int i = 0; i < 40; i++)
                            {
                                int realLen = Convert.ToInt32(mySort[(i+2) * 12 + 1]);
                                int numForType=0;                
                                prodType[i] = Encoding.Default .GetString(mySort, (i+2)*12+2,realLen).Trim();

                                for(int j=0;j<dt3.Rows.Count;j++)
                                {
                                    if (dt3.Rows[j]["sortname"].ToString().Trim().Equals(prodType[i]))
                                    {
                                        numForType = (int)dt3.Rows[j]["number"];
                                        break;
                                    }
                                }
                                String tmpText = "update dbo.FeedBin set Sort='" + prodType[i] + "',NumRemain=" + numForType + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + (i + 1);
                                db.DBUpdate("update dbo.FeedBin set Sort='"+ prodType[i]+ "',NumRemain="+ numForType+",ResultOK="+0+",ResultNG="+0+" where LayerID="+(i+1)) ;                                
                            }
                            TaskCycle.MainStep = TaskCycle.MainStep + 10;
                        }
                    }
                    
                    if (dt2.Rows.Count == 1)
                    {
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.GetPiece && (int)dt2.Rows[0]["FrameLocation"] > 0)
                        {
                            //int tmpInt=(int)dt2.Rows[0]["FrameLocation"];
                            //Convert.ToByte(tmpInt);
                            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Pos, PlcData._writeLength1, new byte[] { Convert.ToByte((int)dt2.Rows[0]["FrameLocation"]) });
                            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.GetPiece });

                            while (PlcData._axlis2Status != (byte)EnumC.Frame.GetPiece)
                            {
                                Thread.Sleep(100);
                            }
                            db.DBDelete("delete from dbo.TaskAxlis2 where orderName=" + (short)EnumC.Frame.GetPiece + "");                            
                            //更新basicID
                            mtr.updateFrameBasicID();
                            dt2.Rows.Clear();
                            dt2.Columns.Clear();
                            Thread.Sleep(2000);
                            plc.DBWrite(100, 3, 1, new Byte[] { 0 });
                            if (TaskCycle.actionType == "FrameToCabinet")
                            {
                                db.DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                                TaskCycle.PickStep = TaskCycle.PickStep + 10;
                            }
                            if (TaskCycle.actionType == "CabinetToFrame")
                            {
                                TaskCycle.PutStep = TaskCycle.PutStep + 10;
                            }
                        }
                    }

                    if (dt2.Rows.Count == 1)
                    { 
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.PutPiece && (int)dt2.Rows[0]["FrameLocation"] > 0)
                        {
                            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Pos, PlcData._writeLength1, new byte[] { Convert.ToByte((int)dt2.Rows[0]["FrameLocation"]) });
                            plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.PutPiece });

                            while (PlcData._axlis2Status != (byte)EnumC.Frame.PutPiece)
                            {
                                Thread.Sleep(100);
                            }
                            db.DBDelete("delete from dbo.TaskAxlis2 where orderName=" + (short)EnumC.Frame.PutPiece + "");                           
                            mtr.deleteFrameBasicID();
                            dt2.Rows.Clear();
                            dt2.Columns.Clear();
                            Thread.Sleep(2000);
                            plc.DBWrite(100, 3, 1, new Byte[] { 0 });
                            if (TaskCycle.actionType == "FrameToCabinet")
                            {
                                TaskCycle.PickStep = TaskCycle.PickStep + 10;
                            }
                            if (TaskCycle.actionType == "CabinetToFrame")
                            {
                                db.DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                                TaskCycle.PutStep = TaskCycle.PutStep + 10;
                            }
                        }
                    }
                    else if(dtr.Rows.Count > 1)
                    {
                        MessageBox.Show("任务队列异常，请查看数据库表格TaskAxlis2，正常情况下该表格中最多只有一条任务记录！");
                    }

                    Thread.Sleep(100);
                }
                Thread.Sleep(100);
            }
        }

        public static int sendToU8(string content)
        {
            IntPtr hwndWindow = FindWindow(null, "序列号专用解析方案"); // find u8 dialog

            if (hwndWindow != IntPtr.Zero)
            {
                IntPtr hwndInput = FindWindowEx(hwndWindow, 0, "ThunderRT6TextBox", null);
                if (hwndInput != IntPtr.Zero)
                {
                    IntPtr text = Marshal.StringToHGlobalAnsi(content);
                    int ret = SendMessage(hwndInput, WM_SETTEXT, IntPtr.Zero, text);
                    int errCode = Marshal.GetLastWin32Error();
                    Console.WriteLine(new System.ComponentModel.Win32Exception(errCode).Message);
                    Marshal.FreeCoTaskMem(text);
                    ret = PostMessage(hwndInput, WM_KEYDOWN, (IntPtr)vbKeyReturn, IntPtr.Zero);
                    ret = PostMessage(hwndInput, WM_KEYUP, (IntPtr)vbKeyReturn, IntPtr.Zero);
                    return 1;
                }
                else
                {
                    Console.WriteLine("请确认U8转移报工窗口打开");
                    return -2;
                }
            }
            else
            {
                Console.WriteLine("请确认U8转移报工窗口打开");
                return -1;
            }
            return 0;
        }

    }
}
