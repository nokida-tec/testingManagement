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
        static public Object lockCabinet = new Object();

        public Cabinet(int ID)
            : base(ID)
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
                        Logger.WriteLine(e);
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
                Logger.WriteLine(e);
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
                Logger.WriteLine(e);
            }
            TestingCabinets.getInstance(this.ID).Status = TestingCabinet.STATUS.Ready;
            return false;
        }

        private Thread task;
        private Object mLock = new Object();
        private bool taskIsRunning = false;
        private bool taskExisting = false;
        public bool cmdStart(string productType, int taskId) 
        {
            Logger.WriteLine("  ***   cmdStart：" + this.ID + " Running:" + taskIsRunning + " productType：" + productType + " taskId：" + taskId);
            lock (this)
            {
                if (task != null)
                {
                    if (task.ThreadState == ThreadState.Stopped || task.ThreadState == ThreadState.Aborted)
                    {
                        Thread.Sleep(100);
                        task = null;
                    }
                    else 
                    {
                        Logger.WriteLine("重复任务，返回");
                        return false;
                    }
                }
                base.cmdStart(productType, taskId);
                return start();
            }
        }

        public bool start()
        {
            Logger.WriteLine("  ***   start：" + this.ID);
            lock (this)
            {
                if (task != null)
                {
                    taskExisting = true;
                    while (taskIsRunning && task.ThreadState != ThreadState.Stopped)
                    {   // 等待原有线程运行退出
                        taskExisting = true;
                        Logger.WriteLine("  ***   测试柜:" + this.ID + "在运行中 线程:" + task.ManagedThreadId + " 状态：" + task.ThreadState);
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(100);
                    task.Abort();
                    task = null;
                 }
                if (task == null)
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
                    ReturnCode ret = doOpenForGet();
                    if (ret != ReturnCode.OK)
                    {
                        return false;
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
            lock(mLock)
            {
                try
                {
                    Logger.WriteLine(DateTime.Now.ToString() + ":  [order]:" + Order + " [cabinetNo]:" + ID + " [basicID]:" + TaskID + " [productType]:" + ProductType);

                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = '" + Name + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                    //关闭测试柜
                    ReturnCode ret = doCloseForTesting();

                    if (Status != TestingCabinet.STATUS.Ready)
                    {
                        ResetData();
                    }

                    if (TestingCabinets.getInstance(this.ID).Status == TestingCabinet.STATUS.Ready)
                    {
                        string command = (ProductType == "B") ? "21" : "11";

                        //通知测试设备测试开始
                        WriteData(DateTime.Now.ToString() + "\t" + command);
                        //通知PLC测试开始了
                        Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 2 });
                    }

                    // 等待测试结束
                    while (Status != TestingCabinet.STATUS.Finished)
                    {
                        if (taskExisting == true)
                        {
                            return;
                        }
                        Thread.Sleep(100);
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
                        Logger.WriteLine(e);
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
                        Logger.WriteLine(e);
                    }

                    //  record the scan barcode to logs file
                    DateTime currentTime = DateTime.Now;
                    StreamWriter sw = File.AppendText(DataBase.logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
                    sw.WriteLine(currentTime.ToString() + " \t" + productID);
                    sw.Flush();
                    sw.Close();

                    // send scancode to U8
                    TaskCycle.sendToU8(productID);

                    ResetData();

                    doOpenForGet();
                    //设置MTR表格，指示测试完成
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);
                }
                catch (AbortException ae)
                {
                    Logger.WriteLine(ae);
                }
                catch (Exception e)
                {
                    doOpenForGet();
                    //设置MTR表格，指示测试完成
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);
                    Logger.WriteLine(e);
                }
                finally
                {
                    taskIsRunning = false;
                }
            }
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

        public bool Pause()
        {
            if (task != null && task.ThreadState == ThreadState.Running )
            {
                task.Suspend();
            }
            return true;
        }

        public bool Resume()
        {
            if (task != null && task.ThreadState == ThreadState.Suspended)
            {
                task.Resume();
            }
            return true;
        }

        public ReturnCode doOpenForGet()
        {
            lock (lockCabinet)
            {
                try
                {
                    Logger.WriteLine("为取料打开测试台: " + ID + " 开始");
                    //通知PLC连接测试件，打开测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });
                    ReturnCode ret = WaitCondition.waitCondition(canGet);
                    Logger.WriteLine("为取料打开测试台: " + ID + " 结束");

                    return ret;
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public ReturnCode doOpenForPut()
        {
            lock (lockCabinet)
            {
                try
                {
                    Logger.WriteLine("为放料打开测试台: " + ID + " 开始");
                    //通知PLC连接测试件，打开测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 4 });
                    ReturnCode ret = WaitCondition.waitCondition(canPut);
                    Logger.WriteLine("为放料打开测试台: " + ID + " 结束");

                    return ret;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public ReturnCode doCloseForTesting()
        {
            lock (lockCabinet)
            {
                try
                {
                    Logger.WriteLine("关闭测试台: " + ID + " 开始");
                    //通知PLC连接测试件，关闭测试柜
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 1 });
                    ReturnCode ret = WaitCondition.waitCondition(canTesting);
                    Logger.WriteLine("关闭测试台: " + ID + " 结束");

                    return ret;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public ReturnCode finishGet()
        {
            Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 8 });
            return ReturnCode.OK;
        }
        public ReturnCode finishPut()
        {
            Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 8 });
            return ReturnCode.OK;
        }

        public bool canGet()
        {
            return (PlcData._cabinetStatus[this.ID] & 8) != 0;
        }
        public bool canPut()
        {
            return (PlcData._cabinetStatus[this.ID] & 1) != 0;
        }
        public bool canTesting()
        {
            return (PlcData._cabinetStatus[this.ID] & 2) != 0;
        }
    }
}
