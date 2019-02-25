using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.DAL;
using XT_CETC23.DataManager;
using XT_CETC23.Model;
using System.Threading;
using XT_CETC23.Common;
using XT_CETC23.DataCom;
using System.Data;

namespace XT_CETC23
{
    class Frame
    {
        public class Lock
        {
            public enum State
            {
                [EnumDescription("Closed")]
                Closed = 1,
                [EnumDescription("Opened")]
                Opened = 0,
            }

            public enum Command
            {
                [EnumDescription("Close")]
                Close = 1,
                [EnumDescription("Open")]
                Open = 0,
            }

        }

        public class Location 
        {
            public String mProdType;
            public int mTray
            {
                get
                {
                    return mTray;
                }
                set 
                {
                }
            }
            public int mSlot
            {
                get
                {
                    return mSlot;
                }
                set
                {
                }
            }

            public string CordinatorX = "0";
            public string CordinatorY = "0";
            public string CordinatorU = "0";

            public Location(int tray, int slot)
            {
                mTray = tray;
                mSlot = slot;
            }
        }

        static private Frame mInstance;
        private static readonly object lockRoot = new object();
        private readonly object lockFrame = new object();

        static public Frame getInstance()
        {
            if (mInstance == null)
            {
                lock (lockRoot)
                {
                    if (mInstance == null)
                    {
                        mInstance = new Frame();
                    }
                }
            }
            return mInstance;
        }

        public bool excuteCommand(Lock.Command command)
        {
            byte[] myByte = new byte[1];
            switch (command)
            {
                case Lock.Command.Open:
                    myByte[0] = (byte)39;
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, PlcData._writeLength1, myByte);
                    break;
                case Lock.Command.Close:
                    myByte[0] = (byte)40;
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, 3, PlcData._writeLength1, myByte);
                    break;
            }
            return false;
        }

        public ReturnCode doScan ()
        {
            lock (lockFrame)
            {
                try
                {
                    // 启动扫描
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.ScanSort });
                    //byte [] myByte=plc.DbRead(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1);
                    //while (myByte[0] != (byte)EnumC.FrameW.ScanSort)
                    //{                                
                    //    Thread.Sleep(100);
                    //    plc.DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.ScanSort });
                    //}
                    WaitCondition.waitCondition(isScanDone);

                    Byte[] mySort = new Byte[504];
                    mySort = Plc.GetInstanse().DbRead(104, 0, 504);
                    Thread.Sleep(2000); // Todo?
                    Plc.GetInstanse().DBWrite(100, 3, 1, new Byte[] { 0 });

                    String[] prodType = new String[40];
                    DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.SortData");

                    for (int i = 0; i < 40; i++)
                    {
                        int realLen = Convert.ToInt32(mySort[(i + 2) * 12 + 1]);
                        int numForType = 0;
                        prodType[i] = Encoding.Default.GetString(mySort, (i + 2) * 12 + 2, realLen).Trim();

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["sortname"].ToString().Trim().Equals(prodType[i]))
                            {
                                numForType = (int)dt.Rows[j]["number"];
                                break;
                            }
                        }
                        String tmpText = "update dbo.FeedBin set Sort='" + prodType[i] + "',NumRemain=" + numForType + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + (i + 1);
                        DataBase.GetInstanse().DBUpdate("update dbo.FeedBin set Sort='" + prodType[i] + "',NumRemain=" + numForType + ",ResultOK=" + 0 + ",ResultNG=" + 0 + " where LayerID=" + (i + 1));
                    }
                    return ReturnCode.OK;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
            }
        }

        public void doAsyncGet(int FrameLocation)
        {
            DataBase.GetInstanse().DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.GetPiece + "," + FrameLocation + ")");
        }

        public ReturnCode doGet(int FrameLocation)
        {
            lock (lockFrame)
            {
                try
                {
                    //int tmpInt=(int)dt2.Rows[0]["FrameLocation"];
                    //Convert.ToByte(tmpInt);
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Pos, PlcData._writeLength1, new byte[] { (byte)FrameLocation });
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.GetPiece });

                    WaitCondition.waitCondition(this.canGetProduct);

                    Plc.GetInstanse().DBWrite(100, 3, 1, new Byte[] { 0 });
                    if (TaskCycle.actionType == "FrameToCabinet")
                    {
                        DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                        // TaskCycle.PickStep = TaskCycle.PickStep + 10;
                    }
                    if (TaskCycle.actionType == "CabinetToFrame")
                    {
                        // TaskCycle.PutStep = TaskCycle.PutStep + 10;
                    }
                    return ReturnCode.OK;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
                finally
                {
                    DataBase.GetInstanse().DBDelete("delete from dbo.TaskAxlis2 where orderName=" + (short)EnumC.Frame.GetPiece + "");
                }
            }
        }

        public void doAsyncPut(int FrameLocation)
        {
            DataBase.GetInstanse().DBInsert("insert into dbo.TaskAxlis2(orderName,FrameLocation)values(" + (int)EnumC.FrameW.PutPiece + "," + FrameLocation + ")");
        }

        public ReturnCode doPut(int FrameLocation)
        {
            lock (lockFrame)
            {
                try
                {
                    DataBase.GetInstanse().DBUpdate("update dbo.MTR set CurrentStation = 'FeedBin',StationSign = '" + false + "' where BasicID=" + MTR.globalBasicID);

                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Pos, PlcData._writeLength1, new byte[] { (byte)FrameLocation });
                    Plc.GetInstanse().DBWrite(PlcData.PlcWriteAddress, PlcData._writeAxlis2Order, PlcData._writeLength1, new byte[] { (byte)EnumC.FrameW.PutPiece });

                    WaitCondition.waitCondition(canPutProduct);
         
                    Plc.GetInstanse().DBWrite(100, 3, 1, new Byte[] { 0 });
                    if (TaskCycle.actionType == "FrameToCabinet")
                    {
                        // TaskCycle.PickStep = TaskCycle.PickStep + 10;
                    }
                    if (TaskCycle.actionType == "CabinetToFrame")
                    {
                        DataBase.GetInstanse().DBUpdate("update dbo.MTR set StationSign = '" + true + "' where BasicID=" + MTR.globalBasicID);
                        // TaskCycle.PutStep = TaskCycle.PutStep + 10;
                    }

                    return ReturnCode.OK;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
                finally
                {
                    DataBase.GetInstanse().DBDelete("delete from dbo.TaskAxlis2 where orderName=" + (short)EnumC.Frame.PutPiece + "");
                }
            }
        }

        private int remainProducts(int trayNo)
        {
            try
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin where LayerID='" + trayNo + "'");
                int remain = (int)dt.Rows[0]["NumRemain"];
                return remain;
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                return 0;
            }
            return 0;
        }

        private Location findProduct(String productType)
        {
            try
            {
                DataTable dt = DataBase.GetInstanse().DBQuery("select * from dbo.FeedBin where sort='" + productType + "'");
                for (int j = 0; j < dt.Rows.Count; j ++)
                {
                    int remain = (int)dt.Rows[j]["NumRemain"];
                    if (remain != 0)
                    {
                        int layerID = (int)dt.Rows[j]["LayerID"];
                        int colNo = (layerID - 1) / 8;
                        int rowNo = (layerID - 1) % 8;
                        int trayNo = (rowNo + 1) * 10 + (colNo + 1);

                        DataTable dtSort = DataBase.GetInstanse().DBQuery("select * from dbo.SortData where sortname='" + productType + "'");
                        int pieceNo = (int)dtSort.Rows[0]["number"] - remain + 1;       //从0开始编号
                        Location location = new Location(trayNo, pieceNo);
                        return location;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                return null;
            }
            return null;
        }

        private int markProduct(Location location, int status)
        {
            try
            {
                int remain = remainProducts(location.mTray);
                if (remain > 0)
                {
                    DataBase.GetInstanse().DBUpdate("update dbo.FeedBin set NumRemain = NumRemain - 1 where LayerID = " + location.mTray);
                    return remain - 1;
                }
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
                return 0;
            }
            return 0;
        }

        private int shoot(ref Location location)
        {
            try
            {
                int prodNumber = 0;
                switch (location.mProdType)
                {
                    case "A":
                        prodNumber = 1;
                        break;
                    case "B":
                        prodNumber = 2;
                        break;
                    case "C":
                        prodNumber = 3;
                        break;
                    case "D":
                        prodNumber = 4;
                        break;
                    case "E":
                        prodNumber = 5;
                        break;
                    case "F":
                        prodNumber = 6;
                        break;
                }
                //D产品不对第4列进行拍照的处理
                //=====================================================
                if (prodNumber == 4)
                {
                    if (location.mSlot % 4 == 0)
                    {
                        return (int)ReturnCode.NoProduct;
                    }
                }

                //触发拍照
                MainForm.cForm.CCDTrigger(prodNumber, location.mSlot);

                if (MainForm.cForm.CCDDone == -1)     //拍照失败
                {
                    return (int)ReturnCode.NoProduct;
                }

                if (location.mProdType == "D")
                {
                    location.CordinatorX = MainForm.cForm.X;
                    location.CordinatorY = MainForm.cForm.Y;
                }
            } 
            catch(Exception e)
            {
                Logger.WriteLine(e);
                return (int)ReturnCode.Exception;
            }
            return (int)ReturnCode.OK;
        }

        public ReturnCode doGetProduct(String productType)
        {
            lock (lockFrame)
            {
                try
                {
                    do
                    {
                        // 找到一个没有测试的产品（可能不存在，需要视觉识别）
                        Location location = findProduct(productType);
                        if (location == null)
                        {
                            return ReturnCode.NoProduct;
                        }
                        // DataBase.GetInstanse().DBUpdate("update dbo.MTR set FrameLocation = " + location.mTray + "," + "SalverLocation=" + location.mSlot + " where BasicID=" + MTR.globalBasicID);
                        doGet(location.mTray);

                        shoot(ref location);
                    } while (true);
                }
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    throw e;
                }
                return ReturnCode.OK;
            }
        }

        public bool canPutProduct()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.PutPiece;
        }

        public bool canGetProduct()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.GetPiece;
        }

        public bool isScanDone()
        {
            return PlcData._axlis2Status == (byte)EnumC.Frame.ScanSort;
        }
    }
}
