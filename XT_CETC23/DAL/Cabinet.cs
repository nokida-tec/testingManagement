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

                using (fsW[i] = new FileStream(CabinetData.pathCabinetOrder[i], FileMode.Open))
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

        public void ReadData(int i,ref string data)
        {
            using (fsR[i] = new FileStream(CabinetData.pathCabinetStatus[i], FileMode.Open))
            {
                using (sread[i] = new StreamReader(fsR[i]))
                {
                    data = sread[i].ReadToEnd();
                    fsR[i].Flush();
                    sread[i].Close();
                    fsR[i].Close();
                    sread[i].Dispose();
                    fsR[i].Dispose();
                }
            }                        
        }
    }
}
