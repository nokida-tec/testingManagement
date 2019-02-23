﻿using System;
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

        Thread axlis2Task, axlis7Task, cabinetTask;
        static TaskCycle taskCycle;
        static object lockTaskCycle = new object();
        public static string feedBinScanDone = "";
        public static string actionType="";
        public static int PickStep;
        public static int PutStep;
        public static bool scanStatus = false;

        DataBase db;
        Plc plc;
        MTR mtr;

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
            mtr = MTR.GetIntanse();
            axlis2Task = new Thread(Axlis2Task);
            axlis2Task.Name = "2轴任务";
            //if (PlcData._plcMode == 25)
            //{
            if (!axlis2Task.IsAlive)
            {
                axlis2Task.Start();
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
            DataTable dt = new DataTable();            
            while (true)
            {
                Thread.Sleep(10);
                while (PlcData.clearTask)
                {
                    Thread.Sleep(100);
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    dt = db.DBQuery("select * from dbo.TaskCabinet");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lock (dt)
                            {
                                int basicID = (int)Convert.ToDouble(dt.Rows[i]["BasicID"]);
                                if (basicID > 0)
                                {
                                    //TestingCabinets.getInstance(i).startTask();
                                    //if (order == "Stop" && threadCab[i].ThreadState == ThreadState.Running)
                                    //{
                                    //    threadCab[i].Abort();
                                    //}
                                    //string productType = dt.Rows[i]["ProductType"].ToString();

                                    //ArrayList paraCabTask = new ArrayList();
                                    //paraCabTask.Add(order);
                                    //paraCabTask.Add(i);
                                    //paraCabTask.Add(basicID);
                                    //paraCabTask.Add(productType);
                                    //threadCab[i] = new Thread(CabinetTest);
                                    //threadCab[i].Name = "CabinetTest" + i;
                                    //if (!order.Equals("Free") && threadCab[i].ThreadState != ThreadState.Running)
                                    //{
                                    //    threadCab[i].Start(paraCabTask);
                                    //}
                                    //else
                                    //{
                                    //    threadCab[i].Abort();
                                    //}
                                }
                         }
                        

                        Thread.Sleep(100);
                    }
                    

                    Thread.Sleep(100);
                }
            }
        }
        //private void CabinetTest(object list)
        //{
        //    ArrayList myList=(ArrayList)list;
        //    string order = (string) myList[0];
        //    int cabinetNo = (int)myList[1];
        //    int basicID = (int)myList[2];
        //    string productType = (string)myList[3];
        //    Console.WriteLine(DateTime.Now.ToString() + ":  [order]:" + order + " [cabinetNo]:" + cabinetNo + " [basicID]:" + basicID + " [productType]:" + productType);
        //    myList.Clear();

        //    Cabinet.GetInstanse().ResetData(cabinetNo);

        //    db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + EnumHelper.GetDescription(EnumC.CabinetW.Free) + "'where CabinetID=" + cabinetNo);
        //    if(order=="Start")
        //    {
        //        if (Config.Config.ENABLED_DEBUG == false)
        //        {
        //            // 1. 等待PLC允许测量
        //            if (Config.Config.ENABLED_PLC)
        //            {
        //                //通知PLC连接测试件，关闭测试柜
        //                plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 1 });

        //                while ((PlcData._cabinetStatus[cabinetNo] & 2) == 0)
        //                {
        //                    Thread.Sleep(100);
        //                }
        //                if (CabinetData.cabinetStatus[cabinetNo] != EnumC.Cabinet.Ready) 
        //                {
        //                    Cabinet.GetInstanse().ResetData(cabinetNo);
        //                }
        //            }

        //            db.DBUpdate("update dbo.MTR set StationSign= '" + false + "' where BasicID=" + basicID);

        //            if (Config.Config.ENABLED_PLC)
        //            {
        //                if (CabinetData.cabinetStatus[cabinetNo] == EnumC.Cabinet.Ready)
        //                {
        //                    DateTime currentTime = DateTime.Now;

        //                    string command = (productType == "B") ? "21" : "11";

        //                    //通知测试设备测试开始
        //                    cabinet.WriteData(cabinetNo, currentTime.ToString() + "\t" + command);
        //                    //通知PLC测试开始了
        //                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 2 });
        //                }

        //                // 等待测试begin   
        //                //while (CabinetData.cabinetStatus[cabinetNo] != EnumC.Cabinet.Testing))
        //                //{
        //                //    Thread.Sleep(100);
        //                //}
        //            }

        //            // 测试完成后，修改测试柜
        //            {
        //                if (Config.Config.ENABLED_PLC)
        //                {
        //                    while (CabinetData.cabinetStatus[cabinetNo] != EnumC.Cabinet.Finished)
        //                    {
        //                        Thread.Sleep(100);
        //                    }
        //                }
        //                //处理结果

        //                //获取测量结果的excel源文件
        //                String[] filePath = Directory.GetFiles(DataBase.sourcePath[cabinetNo]);
        //                string sourceFile = "";
        //                if (filePath != null)
        //                {
        //                    for (int i = 0; i < filePath.Length; i++)
        //                    {
        //                        if (Path.GetExtension(filePath[i]) == ".xls" || Path.GetExtension(filePath[i]) == ".xlsx")
        //                        {
        //                            sourceFile = filePath[i];
        //                        }
        //                        else
        //                        {
        //                            return;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    return;
        //                }

        //                //读取excel表格判断测试OK，NG

        //                bool testResult = true;
        //                if (Config.Config.ENABLED_PLC == true)
        //                {
        //                    testResult = excelOp.CheckTestResults(sourceFile);
        //                }

        //                db.DBUpdate("update dbo.MTR set ProductCheckResult= '" + EnumHelper.GetDescription(testResult ? EnumC.Cabinet.OK : EnumC.Cabinet.NG) + "' where BasicID= " + basicID);

        //                //生成目标文件名并把测量结果excel文件拷贝到目标目录，命名为生成的文件名
        //                dt = db.DBQuery("select * from dbo.MTR where BasicID=" + basicID);
        //                string productID = dt.Rows[0]["ProductID"].ToString().Trim();       // scan barcode
        //                //string productType = dt.Rows[0]["ProductType"].ToString().Trim();   // A,B,C,D

        //                dt = db.DBQuery("select * from dbo.ProductDef where Type= '" + productType + "'");
        //                string productName = dt.Rows[0]["Name"].ToString().Trim();          // 
        //                string productSerial = dt.Rows[0]["SerialNo"].ToString().Trim();    // 0103zt000149
        //                string[] strings = productID.Split(new char[2] { '$', '#' });

        //                string opName = "常温";
        //                try
        //                {
        //                    string defineID = strings[2] + strings[0].Substring(4);             // 1533-13090000010
        //                    DataBase dbOfU8 = DataBase.GetU8DBInstanse();
        //                    dt = dbOfU8.DBQuery("select max(opseq) from v_fc_optransformdetail where invcode = '"
        //                        + productSerial + "' and define22 = '" + defineID + "'");
        //                    int opMax = Convert.ToInt32(dt.Rows[0]["opseq"]);
        //                    dt = db.DBQuery("select * from dbo.OperateDef where OpSeq= '" + opMax + "'");
        //                    opName = dt.Rows[0]["Name"].ToString().Trim();
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine(e.Message);
        //                }

        //                string targetFileName = strings[0].Substring(4) + "_" + productName + "_" + opName;
        //                fileOp.FileCopy(targetFileName, sourceFile, DataBase.targetPath);

        //                //  record the scan barcode to logs file
        //                DateTime currentTime = DateTime.Now;
        //                StreamWriter sw = File.AppendText(DataBase.logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
        //                sw.WriteLine(currentTime.ToString() + " \t" + productID);
        //                sw.Flush();
        //                sw.Close();

        //                // send scancode to U8
        //                sendToU8(productID);

        //                //删除源文件          
        //                File.Delete(sourceFile);

        //                if (Config.Config.ENABLED_PLC)
        //                {
        //                    //通知PLC测试完成，打开测试柜
        //                    plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });
        //                    //等待PLC允许取料
        //                    while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
        //                    {
        //                        Thread.Sleep(100);
        //                    }
        //                }
        //                //设置MTR表格，指示测试完成
        //                db.DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + basicID);
        //                Cabinet.GetInstanse().ResetData(cabinetNo);
        //            }

        //        }
        //        else
        //        {
        //            //=================================================模拟测试过程,最终放入测试进程中========================================
        //            //通知PLC连接测试件，关闭测试柜                
        //            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 1 });

        //            //等待PLC允许测量
        //            while ((PlcData._cabinetStatus[cabinetNo] & 2) == 0)
        //            {
        //                Thread.Sleep(100);
        //            }

        //            //等待plc
        //            Thread.Sleep(20000);
        //            //通知PLC测试开始了
        //            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 2 });

        //            //模拟测试
        //            Thread.Sleep(20000);

        //            db.DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "OK" + "' where BasicID=" + basicID);

        //            //通知PLC测试完成，打开测试柜
        //            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });
        //            Thread.Sleep(100);

        //            //等待PLC允许取料
        //            while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
        //            {
        //                Thread.Sleep(100);
        //            }
        //            //设置MTR表格，指示测试完成
        //            db.DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + basicID);
        //            //=========================================================================================================================
        //        }
        //    }
        //    if(order=="Stop")
        //    {
        //        if (Config.Config.ENABLED_DEBUG == false)
        //        {
        //            if (!(CabinetData.cabinetStatus[cabinetNo] == EnumC.Cabinet.Ready
        //                    || CabinetData.cabinetStatus[cabinetNo] == EnumC.Cabinet.Finished))
        //            {
        //                cabinet.WriteData(cabinetNo, EnumHelper.GetDescription(EnumC.CabinetW.Stop));
        //            }
        //            while (CabinetData.cabinetStatus[cabinetNo] != EnumC.Cabinet.Finished)
        //            {
        //                Thread.Sleep(100);
        //            }
        //            db.DBUpdate("update dbo.TaskCabinet set OrderType= '" + EnumHelper.GetDescription(EnumC.CabinetW.Free) + "'where CabinetID=" + cabinetNo);
        //            db.DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + basicID );
        //        }
        //        else 
        //        {
        //            //=================================================模拟测试过程,最终放入测试进程中========================================

        //            Thread.Sleep(2000);
        //            db.DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "NG" + "' where BasicID=" + basicID);
        //            //通知PLC测试完成，打开测试柜
        //            plc.DBWrite(PlcData.PlcWriteAddress, (13 + cabinetNo), 1, new Byte[] { 4 });

        //            //等待PLC允许取料
        //            while ((PlcData._cabinetStatus[cabinetNo] & 8) == 0)
        //            {
        //                Thread.Sleep(100);
        //            }
        //            //设置MTR表格，指示测试完成
                
        //            db.DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + basicID);
        //            //=========================================================================================================================                    
        //        }
        //    }          
        //}
        
        private void Axlis2Task()
        {
            //db.DBDelete("delete from dbo.TaskAxlis2");
            while (true)
            {
                while (PlcData.clearTask)
                {
                    DataTable dt2 = db.DBQuery("select * from dbo.TaskAxlis2");
                    DataTable dt3 = db.DBQuery("select * from dbo.SortData");
                    if (dt2 !=null && dt2.Rows.Count == 1)
                    {
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.ScanSort)
                        {
                            Frame.getInstance().doScan();
                        }
                    }

                    if (dt2 != null && dt2.Rows.Count == 1)
                    {
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.GetPiece && (int)dt2.Rows[0]["FrameLocation"] > 0)
                        {
                            Frame.getInstance().doGet((int)dt2.Rows[0]["FrameLocation"]);
                        }
                    }

                    if (dt2 != null && dt2.Rows.Count == 1)
                    { 
                        if ((int)dt2.Rows[0]["orderName"] == (int)EnumC.FrameW.PutPiece && (int)dt2.Rows[0]["FrameLocation"] > 0)
                        {
                            Frame.getInstance().doPut((int)dt2.Rows[0]["FrameLocation"]);       
                        }
                    }
                    else if (dt2 != null && dt2.Rows.Count > 1)
                    {
                        Logger.WriteLine("任务队列异常，请查看数据库表格TaskAxlis7，正常情况下该表格中最多只有一条任务记录！");
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
                    Logger.WriteLine(new System.ComponentModel.Win32Exception(errCode).Message);
                    Marshal.FreeCoTaskMem(text);
                    ret = PostMessage(hwndInput, WM_KEYDOWN, (IntPtr)vbKeyReturn, IntPtr.Zero);
                    ret = PostMessage(hwndInput, WM_KEYUP, (IntPtr)vbKeyReturn, IntPtr.Zero);
                    return 1;
                }
                else
                {
                    Logger.WriteLine("请确认U8转移报工窗口打开");
                    return -2;
                }
            }
            else
            {
                Logger.WriteLine("请确认U8转移报工窗口打开");
                return -1;
            }
            return 0;
        }

    }
}
