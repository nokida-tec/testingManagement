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
        Cabinet cabinet;
        DataTable dt2, dt7, dtc, dtr;
        Task[] task = new Task[6];
        Thread[] threadCab= new Thread[6];
        ArrayList paraCabTask=new ArrayList();
        ExcelOperation excelOp = new ExcelOperation();
        FileOperation fileOp = new FileOperation();

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
            cabinet = Cabinet.GetInstanse();
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
            cabinetTask = new Thread(CabinetTask);
            cabinetTask.Name = "检测柜项目";
            if (!cabinetTask.IsAlive)
            {
                cabinetTask.Start();
            }
            //}
        }
        private void CabinetTask()
        {
            dtc = new DataTable();            
            while (true)
            {
                Thread.Sleep(10);
                while (PlcData.clearTask)
                {
                    Thread.Sleep(100);
                    dtc.Rows.Clear();
                    dtc.Columns.Clear();
                    dtc = db.DBQuery("select * from dbo.TaskCabinet");
                    for (int i = 0; i < dtc.Rows.Count; i++)
                    {
                        string order = dtc.Rows[i]["OrderType"].ToString().Trim();
                        if (order == "Stop" && threadCab[i].ThreadState == ThreadState.Running)
                        {
                            threadCab[i].Abort();
                        }
                        int basicID = (int)Convert.ToDouble(dtc.Rows[i]["BasicID"]);
                        string productType = dtc.Rows[i]["ProductType"].ToString();
                        paraCabTask.Add(order);
                        paraCabTask.Add(i);
                        paraCabTask.Add(basicID);
                        paraCabTask.Add(productType);
                        threadCab[i] = new Thread(CabinetTest);                        
                        if (!order.Equals("Free") && threadCab[i].ThreadState!=ThreadState.Running)
                        {                                                                                  
                            threadCab[i].Start(paraCabTask);
                        }
                        else
                        {
                            threadCab[i].Abort();
                        }
                        Thread.Sleep(100);
                        paraCabTask.Clear();
                    }
                    Thread.Sleep(100);
                }
            }
        }

        public void Test()
        {
            ArrayList paraCabTask = new ArrayList();
            paraCabTask.Add("Start");
            paraCabTask.Add(2);
            paraCabTask.Add(2000);
            paraCabTask.Add("A");
            CabinetTest(paraCabTask);
        }

        private void CabinetTest(object list)
        {
            ArrayList myList=(ArrayList)list;
            string order = (string) myList[0];
            int cabinetNo = (int)myList[1];
            int basicID = (int)myList[2];
            string productType = (string)myList[3];
            db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + EnumHelper.GetDescription(EnumC.CabinetW.Free) + "'where CabinetID=" + cabinetNo);
            if(order=="Start")
            {
                if (Config.Config.ENABLED_DEBUG == false)
                {
                    // 1. 等待PLC允许测量
                    if (Config.Config.ENABLED_PLC)
                    {
                        //通知PLC连接测试件，关闭测试柜
                        plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 1 });

                        while ((PlcData._cabinetStatus[cabinetNo] & 2) == 0)
                        {
                            Thread.Sleep(100);
                        }
                    }

                    db.DBUpdate("update dbo.MTR set ProductSign= '" + false + "' where BasicID=" + basicID);

                    if (Config.Config.ENABLED_PLC)
                    {
                        if (CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Ready))
                            || CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Finished)))
                        {
                            DateTime currentTime = DateTime.Now;

                            string command = (productType == "B") ? "21" : "11";

                            //通知测试设备测试开始
                            cabinet.WriteData(cabinetNo, currentTime.ToString() + "   " + command);
                            //通知PLC测试开始了
                            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 2 });
                            Thread.Sleep(2000);
                        }

                        // 等待测试begin   
                        while (!CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Testing)))
                        {
                            Thread.Sleep(100);
                        }
                    }

                    // 测试完成后，修改测试柜
                    {
                        //db.DBDelete("delete from dbo.TaskCabinet");
                        //task[i].Dispose();
                        //task[i] = null;
                        if (Config.Config.ENABLED_PLC)
                        {
                            while (!CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Finished).ToString()))
                            {
                                Thread.Sleep(100);
                            }
                        }
                        //处理结果

                        //获取测量结果的excel源文件
                        String[] filePath = Directory.GetFiles(DataBase.sourcePath[cabinetNo]);
                        string sourceFile = "";
                        if (filePath != null)
                        {
                            for (int i = 0; i < filePath.Length; i++)
                            {
                                if (Path.GetExtension(filePath[i]) == ".xls" || Path.GetExtension(filePath[i]) == ".xlsx")
                                {
                                    sourceFile = filePath[i];
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }

                        //读取excel表格判断测试OK，NG

                        bool testResult = true;
                        //if (Config.Config.ENABLED_PLC == true)
                        {
                            testResult = excelOp.CheckTestResults(sourceFile);
                        }

                        //if (CabinetData.cabinetStatus[cabinetNo] == EnumHelper.GetDescription(EnumC.Cabinet.Fault))
                        //{
                        //    db.DBUpdate("update dbo.MTR set ProductCheckResult= '" + EnumHelper.GetDescription(EnumC.Cabinet.Fault) + "'where BasicID= " + basicID);
                        //}
                        //if (CabinetData.cabinetStatus[cabinetNo] == EnumHelper.GetDescription(EnumC.Cabinet.NG))
                        db.DBUpdate("update dbo.MTR set ProductCheckResult= '" + EnumHelper.GetDescription(testResult ? EnumC.Cabinet.OK : EnumC.Cabinet.NG) + "'where BasicID= " + basicID);

                        //生成目标文件名并把测量结果excel文件拷贝到目标目录，命名为生成的文件名
                        dt = db.DBQuery("select * from dbo.MTR where BasicID=" + basicID);
                        string productID = dt.Rows[0]["ProductID"].ToString().Trim();       // scan barcode
                        //string productType = dt.Rows[0]["ProductType"].ToString().Trim();   // A,B,C,D

                        dt = db.DBQuery("select * from dbo.ProductDef where Type= '" + productType + "'");
                        string productName = dt.Rows[0]["Name"].ToString().Trim();          // 
                        string productSerial = dt.Rows[0]["SerialNo"].ToString().Trim();    // 0103zt000149

                        string[] strings = productID.Split(new char[2] { '$', '#' });
                        string defineID = strings[2] + strings[0].Substring(4);             // 1533-13090000010

                        string opName = "常温";
                        try
                        {
                            DataBase dbOfU8 = DataBase.GetU8DBInstanse();
                            dt = dbOfU8.DBQuery("select max(opseq) from v_fc_optransformdetail where invcode = '"
                                + productSerial + "' and define22 = '" + defineID + "'");
                            int opMax = Convert.ToInt32(dt.Rows[0]["opseq"]);
                            dt = db.DBQuery("select * from dbo.OperateDef where OpSeq= '" + opMax + "'");
                            opName = dt.Rows[0]["Name"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        string targetFileName = strings[0].Substring(4) + "_" + productName + "_" + opName;
                        fileOp.FileCopy(targetFileName, sourceFile, DataBase.targetPath);

                        //  record the scan barcode to logs file
                        DateTime currentTime = DateTime.Now;
                        StreamWriter sw = File.AppendText(DataBase.logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
                        sw.WriteLine(currentTime.ToString() + " \t" + productID);
                        sw.Flush();
                        sw.Close();

                        // send scancode to U8
                        sendToU8(productID);

                        //删除源文件          
                        File.Delete(sourceFile);

                        if (Config.Config.ENABLED_PLC)
                        {
                            //通知PLC测试完成，打开测试柜
                            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });
                            //等待PLC允许取料
                            while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
                            {
                                Thread.Sleep(100);
                            }
                        }
                        //设置MTR表格，指示测试完成
                        db.DBUpdate("update dbo.MTR set ProductSign= '" + true + "' where BasicID= " + basicID);
                    }

                }
                else
                {
                    //=================================================模拟测试过程,最终放入测试进程中========================================
                    //通知PLC连接测试件，关闭测试柜                
                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 1 });

                    //等待PLC允许测量
                    while ((PlcData._cabinetStatus[cabinetNo] & 2) == 0)
                    {
                        Thread.Sleep(100);
                    }

                    //等待plc
                    Thread.Sleep(20000);
                    //通知PLC测试开始了
                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 2 });

                    //模拟测试
                    Thread.Sleep(20000);

                    db.DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "OK" + "' where BasicID=" + basicID);

                    //通知PLC测试完成，打开测试柜
                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });
                    Thread.Sleep(100);

                    //等待PLC允许取料
                    while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
                    {
                        Thread.Sleep(100);
                    }
                    //设置MTR表格，指示测试完成
                    db.DBUpdate("update dbo.MTR set ProductSign= '" + true + "' where BasicID= " + basicID);
                    //=========================================================================================================================
                }
            }
            if(order=="Stop")
            {
                if (Config.Config.ENABLED_DEBUG == false)
                {
                    if (!(CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Ready))
                            || CabinetData.cabinetStatus[cabinetNo].Trim().Equals(EnumHelper.GetDescription(EnumC.Cabinet.Finished))))
                    {
                        cabinet.WriteData(cabinetNo, EnumHelper.GetDescription(EnumC.CabinetW.Stop));
                    }
                    while (CabinetData.cabinetStatus[cabinetNo] != EnumHelper.GetDescription(EnumC.Cabinet.Finished))
                    {
                        Thread.Sleep(100);
                    }
                    db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + EnumHelper.GetDescription(EnumC.CabinetW.Free) + "'where CabinetID=" + cabinetNo);
                    db.DBUpdate("update dbo.MTR set ProductSign= '" + true + "' where BasicID= " + basicID );
                }
                else 
                {
                    //=================================================模拟测试过程,最终放入测试进程中========================================

                    Thread.Sleep(2000);
                    db.DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "NG" + "' where BasicID=" + basicID);
                    //通知PLC测试完成，打开测试柜
                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });

                    //等待PLC允许取料
                    while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
                    {
                        Thread.Sleep(100);
                    }
                    //设置MTR表格，指示测试完成
                
                    db.DBUpdate("update dbo.MTR set ProductSign= '" + true + "' where BasicID= " + basicID);
                    //=========================================================================================================================                    
                }
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

        int sendToU8 (string content)
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
                    Console.WriteLine("can't find the input box of U8");
                    return -2;
                }
            }
            else
            {
                Console.WriteLine("can't find the windows of U8");
                return -1;
            }
            return 0;
        }
    }
}
