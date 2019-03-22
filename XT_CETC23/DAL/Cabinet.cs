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
using XT_CETC23.Common;
using XT_CETC23.Model;
using XT_CETC23.DataCom;
using Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace XT_CETC23
{
    class Cabinet: TestingCabinet
    {
        static public Object lockCabinet = new Object();

        public Cabinet(int ID)
            : base(ID)
        {
            Plc.GetInstanse().RegistryDelegate(onPlcDataChanged);
        }

        public String[] getCap()
        {
            return new String[] { ProductType };
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
                return TestingCabinet.STATUS.Fault_Config;
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
				Status = TestingCabinet.STATUS.Fault_Control;
            	return false;
            }
            Status = TestingCabinet.STATUS.NG;
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
                    // 1. 等待PLC允许测量

                    //DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = '" + Name + "',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                    //关闭测试柜
                    ReturnCode ret = doCloseForTesting();

                    if (Status != TestingCabinet.STATUS.Ready)
                    {
                        ResetData();
                    }

                    DateTime currentTime = DateTime.Now;

                    if (TestingCabinets.getInstance(this.ID).Status == TestingCabinet.STATUS.Ready)
                    {

                                    string command = (ProductType == "B") ? "21" : "11";

                                    // 通知PLC测试开始了
                                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, (13 + this.ID), 1, new Byte[] { 2 });

                                    //通知测试设备测试开始
                                    WriteData(currentTime.ToString() + "\t" + command);
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
                                    if (Config.Config.getInstance().enableU8)
                                    {
                                        string defineID = strings[2] + strings[0].Substring(4);             // 1533-13090000010
                                        DataBase dbOfU8 = DataBase.GetU8DBInstanse();
                                        dt = dbOfU8.DBQuery("select max(opseq) from v_fc_optransformdetail where invcode = '"
                                            + productSerial + "' and define22 = '" + defineID + "'");
                                        int opMax = Convert.ToInt32(dt.Rows[0]["opseq"]);
                                        dt = DataBase.GetInstanse().DBQuery("select * from dbo.OperateDef where OpSeq= '" + opMax + "'");
                                        opName = dt.Rows[0]["Name"].ToString().Trim();
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logger.WriteLine(e);
                                    MessageBox.Show("U8数据库未连接！请确保网络连接");
                                }

                    try
                    {
                        string targetFileName = strings[0].Substring(4) + "_" + productName + "_" + opName + ".xlsx";
                        FileOperation fileOp = new FileOperation();
                        fileOp.FileCopy(targetFileName, sourceFile, Config.Config.getInstance().targetPath);
                        //删除源文件          
                        File.Delete(sourceFile);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(e);
                    }

                    //  record the scan barcode to logs file
                    StreamWriter sw = File.AppendText(Config.Config.getInstance().logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
                    sw.WriteLine(currentTime.ToString() + " \t" + productID);
                    sw.Flush();
                    sw.Close();

                    // send scancode to U8
					if (Config.Config.getInstance().enableU8)
					{
                       U8Connector.sendToU8(productID);
			 		}

                    doOpenForGet();
                    //设置MTR表格，指示测试完成
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);
                    ResetData();
         		}
                catch(IOException e1)
                {
                     Logger.WriteLine(e1);
                            Status = STATUS.Fault_Config;
                            MessageBox.Show("网络异常! 请确保测试柜网络是否可以访问。网络正常后请复位启动！");
                }
				catch (AbortException ae)
                {
                    Logger.WriteLine(ae);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                            DataBase.GetInstanse().DBUpdate("update dbo.MTR set "
                                + " ProductCheckResult= '" + EnumHelper.GetDescription(TestingCabinet.STATUS.NG) + "' "
                                + " ,EndTime = '" + DateTime.Now + "' "
                                + " where BasicID = " + this.TaskID);
								
                    doOpenForGet();
                    //设置MTR表格，指示测试完成
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign= '" + true + "' where BasicID=" + this.TaskID);
                    Logger.WriteLine(e);
                }
                finally
                {
                    taskIsRunning = false;
                    Logger.WriteLine("Finish:  [order]:" + Order + " [cabinetNo]:" + ID + " [basicID]:" + TaskID + " [productType]:" + ProductType);
               }
            }
        }

        public void doTest ()
        {
            CabinetTest();
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
                        fileOp.FileCopy(targetFileName, sourceFile, Config.Config.getInstance().targetPath);
                        //删除源文件          
                        File.Delete(sourceFile);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(e);
                    }

                    //  record the scan barcode to logs file
                    DateTime currentTime = DateTime.Now;
                    StreamWriter sw = File.AppendText(Config.Config.getInstance().logPath + "\\barcode_" + currentTime.ToString("yyyyMMdd") + ".log");
                    sw.WriteLine(currentTime.ToString() + " \t" + productID);
                    sw.Flush();
                    sw.Close();

                    // send scancode to U8
                    U8Connector.sendToU8(productID);

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

        public delegate void delegateShowMessage(int cabinetNo, string message);
        private delegateShowMessage mDelegateOfShow = null;

        public void RegistryDelegate(delegateShowMessage delegateOfShow)
        {
            mDelegateOfShow = delegateOfShow;
        }

        public void UnregistryDelegate(delegateShowMessage delegateOfShow)
        {
            mDelegateOfShow = null;
        }

        Thread readCabinetTh = null;
        private Object lockMonitor = new Object();
        public void StartMonitor() 
        {
            if (Enable == ENABLE.Enable)
            {
                readCabinetTh = new Thread(ReadCabinet);
                readCabinetTh.Name = "读取测试柜[" + ID + "]状态";
                readCabinetTh.Start();
            }
        }

        public void StopMonitor()
        {
            try
            {
                if (readCabinetTh != null && readCabinetTh.IsAlive)
                {
                    readCabinetTh.Abort();
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
            }
            finally
            {
                if (readCabinetTh != null)
                {
                    readCabinetTh = null;
                }
            }
        }

        private void ReadCabinet()
        {
            lock (lockMonitor)
            {
                while (true)
                {
                    try
                    {
                        if (CabinetData.pathCabinetStatus != null)
                        {
                            TestingCabinet.STATUS cabinetStatus = TestingCabinets.getInstance(ID).ReadData();
                            if (mDelegateOfShow != null)
                            {
                                mDelegateOfShow(ID, EnumHelper.GetDescription(cabinetStatus));
                            }
                            TestingCabinets.getInstance(ID).Status = cabinetStatus;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(e);
                    }
                    finally
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        public enum BedStatus
        {
            [EnumDescription("离线")]
            Unkown = 0,
            [EnumDescription("准备中")]
            Ready = 1,
            [EnumDescription("可放料")]
            CanPut = 2,
            [EnumDescription("可测试")]
            CanTest = 3,
            [EnumDescription("测试中")]
            Testing = 4,
            [EnumDescription("可取料")]
            CanGet = 5,
        }
        private BedStatus mStatus = BedStatus.Unkown;
        private void onPlcDataChanged ()
        {
            BedStatus newStatus = BedStatus.Unkown;
            if ((PlcData._cabinetStatus[ID] & 1) != 0)
            {
                newStatus = BedStatus.CanPut;
            }
            else if ((PlcData._cabinetStatus[ID] & 2) != 0)
            {
                newStatus = BedStatus.CanTest;
            }
            else if ((PlcData._cabinetStatus[ID] & 4) != 0)
            {
                newStatus = BedStatus.Testing;
            }
            else if ((PlcData._cabinetStatus[ID] & 8) != 0)
            {
                newStatus = BedStatus.CanGet;
            }
            else
            {
                newStatus = BedStatus.Ready;
            }
            doStatusChanged(newStatus);
        }

        public delegate bool onStatusChanged(int cabinetID, BedStatus status);
        private onStatusChanged mDelegateStatusChanged;
        public void RegistryDelegate(onStatusChanged delegateStatusChanged)
        {
            mDelegateStatusChanged = delegateStatusChanged;
        }
        public void UnregistryDelegate(onStatusChanged delegateStatusChanged)
        {
            mDelegateStatusChanged = null;
        }

        private void doStatusChanged(BedStatus newStatus)
        {
            lock (this)
            {
                try
                {
                    if (mStatus == newStatus)
                    {
                        return;
                    }
                    Logger.WriteLine("测试台[" + ID + "]：状态改变：" + mStatus + " ===> " + newStatus);

                    if (mDelegateStatusChanged != null)
                    {
                        mDelegateStatusChanged(ID, newStatus);
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                    mStatus = newStatus;
                }
            }
        }

        private TestingCabinet.STATUS mResult;
        public delegate bool onResultChanged(int cabinetID, TestingCabinet.STATUS result);
        private onResultChanged mDelegateResultChanged;
        public void RegistryDelegate(onResultChanged delegateResultChanged)
        {
            mDelegateResultChanged = delegateResultChanged;
        }
        public void UnregistryDelegate(onResultChanged delegateResultChanged)
        {
            mDelegateResultChanged = null;
        }

        private void doResultChanged(TestingCabinet.STATUS newStatus)
        {
            lock (this)
            {
                try
                {
                    if (mResult == newStatus)
                    {
                        return;
                    }
                    Logger.WriteLine("测试台[" + ID + "]：测试结果改变：" + mResult + " ===> " + newStatus);

                    if (mDelegateStatusChanged != null)
                    {
                        mDelegateResultChanged(ID, newStatus);
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                    mResult = newStatus;
                }
            }
        }

        public delegate bool onConfigChanged(int cabinetID);
        private onConfigChanged mDelegateConfigChanged;
        public void RegistryDelegate(onConfigChanged delegateConfigChanged)
        {
            mDelegateConfigChanged = delegateConfigChanged;
        }
        public void UnregistryDelegate(onConfigChanged delegateConfigChanged)
        {
            mDelegateConfigChanged = null;
        }

        //TestingCabinet mOldCabinet;
        public void doConfigChanged()
        {
            lock (this)
            {
                try
                {
                    Logger.WriteLine("测试台[" + ID + "]：配置改变：");

                    if (mDelegateConfigChanged != null)
                    {
                        mDelegateConfigChanged(ID);
                    }
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                }
                finally
                {
                }
            }
        }
    }
}
