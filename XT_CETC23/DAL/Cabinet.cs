using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.DataManager;
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
            //Directory.CreateDirectory(Path.GetDirectoryName(CabinetData.pathCabinetStatus[i]));
            //File.Create(CabinetData.pathCabinetStatus[i]).Dispose();
            FileStream fs = new FileStream(CabinetData.pathCabinetStatus[i], FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line = null;
            string lastline = null;
            while ((line = sr.ReadLine()) != null)
            {
                lastline = line;
            }
            if (lastline != null) 
            {
                string[] orders = lastline.Split(new char[1] { ' ' });
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
                        break;
                }
            }
            return EnumC.Cabinet.Ready;
        }
    }
}
