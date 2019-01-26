using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.DataManager;
using System.Data;

namespace XT_CETC23.DataCom
{
    class Cabinet
    {
        static Cabinet cabinet;
        FileStream[] fsR=new FileStream[6];
        FileStream[] fsW=new FileStream[6];
        StreamReader[] sread=new StreamReader[6];
        StreamWriter[] swrite=new StreamWriter[6];
        object lockWrite = new object();

        public static Cabinet GetInstanse()
        {
            if(cabinet==null)
            {
                cabinet = new Cabinet();
            }
            return cabinet;
        }

        public void WriteData(int i, string data)
        {
            lock (lockWrite)
            {
                string path = System.IO.Path.GetDirectoryName(CabinetData.pathCabinetOrder[i]);
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                using (fsW[i] = new FileStream(CabinetData.pathCabinetOrder[i], FileMode.Append))
                {
                    using (swrite[i] = new StreamWriter(fsW[i]))
                    {
                        swrite[i].WriteLine(data);
                        fsW[i].Flush();
                        swrite[i].Flush();
                        swrite[i].Close();
                        fsW[i].Close();
                        swrite[i].Dispose();
                        fsW[i].Dispose();
                    }
                }
            }
        }

        public EnumC.Cabinet ReadData(int i)
        {
            try
            {
                DataBase db = DataBase.GetInstanse();
                DataTable dt = db.DBQuery("select * from dbo.CabinetData where number = " + i);
                bool enabled = (bool)dt.Rows[0]["status"];
                FileStream fs = new FileStream(CabinetData.pathCabinetStatus[i], FileMode.Open, FileAccess.Read);
                if (enabled)
                {
                    StreamReader sr = new StreamReader(fs);
                    string line = null;
                    string lastline = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lastline = line;
                    }
                    if (lastline != null)
                    {
                        string[] orders = lastline.Split(new char[2] { ' ', '\t' });
                        switch (orders[orders.Length - 1])
                        {
                            case "30":
                                return EnumC.Cabinet.Ready;
                            case "31":
                                return EnumC.Cabinet.Testing;
                            case "32":
                                return EnumC.Cabinet.Fault_Config;
                            case "33":
                                return EnumC.Cabinet.Fault_Control;
                            case "34":
                                return EnumC.Cabinet.Fault_Report;
                            case "40":
                                return EnumC.Cabinet.Finished;
                            default:
                                return EnumC.Cabinet.Ready;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return EnumC.Cabinet.NG;
        }

        public bool ResetData(int cabinetNo)
        {
            try
            {
                string line = "时间\t指令字";
                // 清除指令文件
                FileStream fs = new FileStream(CabinetData.pathCabinetStatus[cabinetNo], FileMode.Truncate, FileAccess.ReadWrite);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                fs = new FileStream(CabinetData.pathCabinetStatus[cabinetNo], FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(line);
                fs.Flush();
                sw.Flush();
                sw.Close();
                fs.Close();
                sw.Dispose();
                fs.Dispose();


                fs = new FileStream(CabinetData.pathCabinetOrder[cabinetNo], FileMode.Truncate, FileAccess.ReadWrite);
                fs.Flush();
                fs.Close();
                fs.Dispose();
                fs = new FileStream(CabinetData.pathCabinetOrder[cabinetNo], FileMode.Append);
                sw = new StreamWriter(fs);
                sw.WriteLine(line);
                fs.Flush();
                sw.Flush();
                sw.Close();
                fs.Close();
                sw.Dispose();
                fs.Dispose();

                // delete the excel源文件
                String[] filePath = Directory.GetFiles(DataBase.sourcePath[cabinetNo]);
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

                CabinetData.cabinetStatus[cabinetNo] = EnumC.Cabinet.Ready;

                return true;
            }
            catch (Exception e)
            {

            }
            CabinetData.cabinetStatus[cabinetNo] = EnumC.Cabinet.Ready;
            return false;
        }

    }
}
