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
using XT_CETC23.DataCom;
using XT_CETC23.Instances;
using Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace XT_CETC23.DAL
{
    class Cabinet: TestingCabinet
    {
        public Cabinet(int ID) : base(ID)
        {
        }

        public void WriteData(string data)
        {
            lock (this)
            {
                string path = System.IO.Path.GetDirectoryName(CabinetData.pathCabinetOrder[this.ID]);
                if (!System.IO.Directory.Exists(path))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    catch (Exception e)
                    {
                        Logger.printException(e);
                    }
                }

                using (FileStream fs = new FileStream(CabinetData.pathCabinetOrder[this.ID], FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(data);
                        fs.Flush();
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        sw.Dispose();
                        fs.Dispose();
                    }
                }
            }
        }

        public TestingCabinet.STATUS ReadData()
        {
            try
            {
                if (TestingCabinets.getInstance(this.ID).Enable == TestingCabinet.ENABLE.Enable)
                {
                    FileStream fs = new FileStream(CabinetData.pathCabinetStatus[this.ID], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);
                    string line = null;
                    string lastline = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lastline = line;
                    }
                    fs.Flush();
                    sr.Close();
                    fs.Close();
                    sr.Dispose();
                    fs.Dispose();
                    if (lastline != null)
                    {
                        string[] orders = lastline.Split(new char[2] { ' ', '\t' });
                        switch (orders[orders.Length - 1])
                        {
                            case "30":
                                return TestingCabinet.STATUS.Ready;
                            case "31":
                                return TestingCabinet.STATUS.Testing;
                            case "32":
                                return TestingCabinet.STATUS.Fault_Config;
                            case "33":
                                return TestingCabinet.STATUS.Fault_Control;
                            case "34":
                                return TestingCabinet.STATUS.Fault_Report;
                            case "40":
                                return TestingCabinet.STATUS.Finished;
                            default:
                                return TestingCabinet.STATUS.Ready;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return TestingCabinet.STATUS.NG;
        }

        public bool ResetData()
        {
            try
            {
                string line = "时间\t指令字";
                // 清除指令文件
                FileStream fs = new FileStream(CabinetData.pathCabinetStatus[this.ID], FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                fs = new FileStream(CabinetData.pathCabinetStatus[this.ID], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(line);
                fs.Flush();
                sw.Flush();
                sw.Close();
                fs.Close();
                sw.Dispose();
                fs.Dispose();

                fs = new FileStream(CabinetData.pathCabinetOrder[this.ID], FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                fs = new FileStream(CabinetData.pathCabinetOrder[this.ID], FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                sw = new StreamWriter(fs);
                sw.WriteLine(line);
                fs.Flush();
                sw.Flush();
                sw.Close();
                fs.Close();
                sw.Dispose();
                fs.Dispose();

                // delete the excel源文件
                String[] filePath = Directory.GetFiles(CabinetData.sourcePath[this.ID]);
                if (filePath != null)
                {
                    for (int i = 0; i < filePath.Length; i++)
                    {
                        //if (Path.GetExtension(filePath[i]) == ".xls" || Path.GetExtension(filePath[i]) == ".xlsx")
                        {
                            File.Delete(filePath[i]);
                        }
                     }
                }

                Status = TestingCabinet.STATUS.Ready;

                return true;
            }
            catch (Exception e)
            {

            }
            TestingCabinets.getInstance(this.ID).Status = TestingCabinet.STATUS.Ready;
            return false;
        }

        private Thread task;
        private bool taskIsRunning = false;
        private bool taskExisting = false;
        public bool cmdStart(string productType, int taskId) 
        {
            Logger.WriteLine("  ***   cmdStart：" + this.ID);
            base.cmdStart(productType, taskId);
            start();
            return true;
        }

        public bool start()
        {
            Logger.WriteLine("  ***   start：" + this.ID);
            lock (this)
            {
                if (task != null)
                {
                    int count = 50;
                    taskExisting = true;
                    while (taskIsRunning && task.ThreadState != ThreadState.Stopped && count-- > 0)
                    {   // 等待原有线程运行退出
                        taskExisting = true;
                        Logger.WriteLine("  ***   测试柜:" + this.ID + "在运行中 线程:" + task.ManagedThreadId + " 状态：" + task.ThreadState);
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(100);
                    task.Abort();
                    task = null;
                 }
                if (task == null && taskIsRunning == false)
                {
                    taskExisting = false;
                    task = new Thread(CabinetTest);
                    task.Name = "测试柜" + this.ID + ": 启动线程";
                    taskIsRunning = true;
                    Logger.WriteLine("  ***   测试柜:" + this.ID + " 新启动线程:" + task.ManagedThreadId + " 状态：" + task.ThreadState);
                    task.Start();
                    return true;
                }

                try 
                {
                    throw new Exception("没有启动测试柜：" + this.ID + "线程");
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                return false;
            }
        }

        public bool cmdStop()
        {
            Logger.WriteLine("  ***   cmdStop：" + this.ID);
            base.cmdStop();
            stop();
            return true;
        }

        public bool stop()
        {
            Logger.WriteLine("  ***   stop：" + this.ID);
            lock (this)
            {
                if (task != null)
                {
                    Logger.WriteLine("  ***   测试柜:" + this.ID + " 停止线程:" + task.ManagedThreadId + " 状态：" + task.ThreadState);
                    taskExisting = true;
                    task.Abort();
                    task = null;
                }
                if (Config.Config.ENABLED_PLC)
                {
                    //通知PLC测试完成，打开测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });
                    //等待PLC允许取料
                    while ((PlcData._cabinetStatus[this.ID] & 8) == 0)
                    {
                        Thread.Sleep(100);
                    }
                }
                // 设置MTR表格，指示测试完成
                DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);

                // 标记测试结果NG
                DataBase.GetInstanse().DBUpdate("update dbo.MTR set "
                    + " ProductCheckResult = '" + EnumHelper.GetDescription(TestingCabinet.STATUS.NG) + "'"
                    + " ,EndTime = '" + DateTime.Now + "'"
                    + " where BasicID= " + this.TaskID);
                return true;
            }
        }

        public void startTask()
        {
            if (this.Order == ORDER.Start)
            {
                start ();
            }
            else if (this.Order == ORDER.Stop)
            {
                stop ();
            }
        }

        private void CabinetTest()
        {
            Logger.WriteLine(DateTime.Now.ToString() + ":  [order]:" + Order + " [cabinetNo]:" + ID + " [basicID]:" + TaskID + " [productType]:" + ProductType);

            if (Order == ORDER.Start)
            {
                if (Config.Config.ENABLED_DEBUG == false)
                {
                    try
                    {
                        // 1. 等待PLC允许测量
                        if (Config.Config.ENABLED_PLC)
                        {
                            //通知PLC连接测试件，关闭测试柜
                            Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 1 });

                            while ((PlcData._cabinetStatus[this.ID] & 2) == 0)
                            {
                                if (taskExisting == true)
                                {
                                    return;
                                }
                                Thread.Sleep(100);
                            }
                            if (Status != TestingCabinet.STATUS.Ready)
                            {
                                ResetData();
                            }
                        }

                        if (Config.Config.ENABLED_PLC)
                        {
                            if (TestingCabinets.getInstance(this.ID).Status == TestingCabinet.STATUS.Ready)
                            {
                                DateTime currentTime = DateTime.Now;

                                string command = (ProductType == "B") ? "21" : "11";

                                //通知测试设备测试开始
                                WriteData(currentTime.ToString() + "\t" + command);
                                //通知PLC测试开始了
                                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 2 });
                            }

                            // 等待测试begin   
                            //while (TestingCabinets.getInstance(this.ID).Status != TestingCabinet.STATUS.Testing))
                            //{
                            //    Thread.Sleep(100);
                            //}
                        }

                        // 测试完成后，修改测试柜
                        {
                            if (Config.Config.ENABLED_PLC)
                            {
                                while (Status != TestingCabinet.STATUS.Finished)
                                {
                                    if (taskExisting == true)
                                    {
                                        return;
                                    } 
                                    Thread.Sleep(100);
                                }
                            }
                            //处理结果

                            //获取测量结果的excel源文件
                            String[] filePath = Directory.GetFiles(CabinetData.sourcePath[this.ID]);
                            string sourceFile = null;
                            if (filePath != null)
                            {
                                for (int i = 0; i < filePath.Length; i++)
                                {
                                    if (Path.GetExtension(filePath[i]) == ".xls" || Path.GetExtension(filePath[i]) == ".xlsx")
                                    {
                                        sourceFile = filePath[i];
                                        break;
                                    }
                                }
                            }

                            //读取excel表格判断测试OK，NG

                            bool testResult = true;
                            if (sourceFile != null && sourceFile.Length > 0)
                            {
                                ExcelOperation excelOp = new ExcelOperation();
                                testResult = excelOp.CheckTestResults(sourceFile);
                            }

                            DataBase.GetInstanse().DBUpdate("update dbo.MTR set "
                                + " ProductCheckResult= '" + EnumHelper.GetDescription(testResult ? TestingCabinet.STATUS.OK : TestingCabinet.STATUS.NG) + "' "
                                + " ,EndTime = '" + DateTime.Now + "' "
                                + " where BasicID = " + this.TaskID);

                            //生成目标文件名并把测量结果excel文件拷贝到目标目录，命名为生成的文件名
                            DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.MTR where BasicID=" + TaskID);
                            string productID = (dt == null || dt.Rows.Count == 0) ? "UNKNOWN" : dt.Rows[0]["ProductID"].ToString().Trim();       // scan barcode
                            //string productType = dt.Rows[0]["ProductType"].ToString().Trim();   // A,B,C,D

                            dt = DataBase.GetInstanse().DBQuery("select * from dbo.ProductDef where Type= '" + ProductType + "'");
                            string productName = dt.Rows[0]["Name"].ToString().Trim();          // 
                            string productSerial = dt.Rows[0]["SerialNo"].ToString().Trim();    // 0103zt000149
                            string[] strings = productID.Split(new char[2] { '$', '#' });

                            string opName = "常温";
                            try
                            {
                                string defineID = strings[2] + strings[0].Substring(4);             // 1533-13090000010
                                DataBase dbOfU8 = DataBase.GetU8DBInstanse();
                                dt = dbOfU8.DBQuery("select max(opseq) from v_fc_optransformdetail where invcode = '"
                                    + productSerial + "' and define22 = '" + defineID + "'");
                                int opMax = Convert.ToInt32(dt.Rows[0]["opseq"]);
                                dt = DataBase.GetInstanse().DBQuery("select * from dbo.OperateDef where OpSeq= '" + opMax + "'");
                                opName = dt.Rows[0]["Name"].ToString().Trim();
                            }
                            catch (Exception e)
                            {
                                Logger.printException(e);
                            }

                            try
                            {
                                string targetFileName = strings[0].Substring(4) + "_" + productName + "_" + opName + ".xlsx";
                                FileOperation fileOp = new FileOperation();
                                fileOp.FileCopy(targetFileName, sourceFile, DataBase.targetPath);
                                //删除源文件          
                                File.Delete(sourceFile);
                            }
                            catch (Exception e)
                            {
                                Logger.printException(e);
                            }

                            //  record the scan barcode to logs file
                            DateTime currentTime = DateTime.Now;
                            StreamWriter sw = File.AppendText(DataBase.logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
                            sw.WriteLine(currentTime.ToString() + " \t" + productID);
                            sw.Flush();
                            sw.Close();

                            // send scancode to U8
                            TaskCycle.sendToU8(productID);

                            if (Config.Config.ENABLED_PLC)
                            {
                                //通知PLC测试完成，打开测试柜
                                Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });
                                //等待PLC允许取料
                                while ((PlcData._cabinetStatus[this.ID] & 8) == 0)
                                {
                                    if (taskExisting == true)
                                    {
                                        return;
                                    }
                                    Thread.Sleep(100);
                                }
                            }
                            //设置MTR表格，指示测试完成
                            DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);
                            ResetData();
                        }
                    } 
                    catch (Exception e1)
                    {
                        Logger.WriteLine(e1);
                        taskIsRunning = false;
                    }
                }
                else
                {
                    //=================================================模拟测试过程,最终放入测试进程中========================================
                    //通知PLC连接测试件，关闭测试柜                
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 1 });

                    //等待PLC允许测量
                    while ((PlcData._cabinetStatus[this.ID] & 2) == 0)
                    {
                        Thread.Sleep(100);
                    }

                    //等待plc
                    Thread.Sleep(20000);
                    //通知PLC测试开始了
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 2 });

                    //模拟测试
                    Thread.Sleep(20000);

                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "OK" + "' where BasicID=" + this.TaskID);

                    //通知PLC测试完成，打开测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });
                    Thread.Sleep(100);

                    //等待PLC允许取料
                    while ((PlcData._cabinetStatus[this.ID] & 8) == 0)
                    {
                        Thread.Sleep(100);
                    }
                    //设置MTR表格，指示测试完成
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + this.TaskID);
                    //=========================================================================================================================
                }
            }
            if (this.Order == ORDER.Stop)
            {
                if (Config.Config.ENABLED_DEBUG == false)
                {
                    if (!(TestingCabinets.getInstance(this.ID).Status == TestingCabinet.STATUS.Ready
                            || TestingCabinets.getInstance(this.ID).Status == TestingCabinet.STATUS.Finished))
                    {
                        WriteData(EnumHelper.GetDescription(TestingCabinet.ORDER.Stop));
                    }
                    while (TestingCabinets.getInstance(this.ID).Status != TestingCabinet.STATUS.Finished)
                    {
                        Thread.Sleep(100);
                    }
                    TestingCabinets.getInstance(this.ID).Order = TestingCabinet.ORDER.Undefined;
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + this.TaskID);
                }
                else
                {
                    //=================================================模拟测试过程,最终放入测试进程中========================================

                    Thread.Sleep(2000);
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "',ProductCheckResult = '" + "NG" + "' where BasicID=" + this.TaskID);
                    //通知PLC测试完成，打开测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });

                    //等待PLC允许取料
                    while ((PlcData._cabinetStatus[this.ID] & 8) == 0)
                    {
                        Thread.Sleep(100);
                    }
                    //设置MTR表格，指示测试完成

                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID= " + this.TaskID);
                    //=========================================================================================================================                    
                }
            }
            taskIsRunning = false;
        }

        public bool abort()
        {
            if (task != null)
            {
                task.Abort();
                task = null;
            }
            return true;
        }
    }
}
