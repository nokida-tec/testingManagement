using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XT_CETC23.DataCom;

namespace XT_CETC23
{
    class Tasks 
    {
        public static int globalBasicID;


        public bool Clear()
        {
            try
            {
                DataBase.GetInstanse().DBDelete("delete from dbo.MTR");
            }
            catch (Exception e)
            {
                Logger.WriteLine(e);
            }
            return true;
        }

        void updateBasicID(string ProductID, int FrameLocation, int SalverLocation, string ProductType, string ProductCurrentPos, string ProductSign, string ProductChectResult)
        {

        }

    }
    class GetID
    {
        static Object mLock = new Object();
        public static int getID()
        {
            lock (mLock)
            {
                int ID;
                int ID1 = 0;
                int ID2 = 0;
                try {
                    DataTable dt = DataBase.GetInstanse().DBQuery("select max(BasicID) from dbo.MTR");
                    ID1 = Convert.ToInt32(dt.Rows[0][0]);
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    ID1 = 0;
                }

                try {
                    DataTable dt = DataBase.GetInstanse().DBQuery("select max(BasicID) from dbo.FrameData");
                    ID2 = Convert.ToInt32(dt.Rows[0][0]);
                } 
                catch (Exception e)
                {
                    Logger.WriteLine(e);
                    ID2 = 0;
                }

                ID = Math.Max(ID1, ID2);
                ID = Math.Max(ID, 1000);

                return ID + 1;
            }
        }
    }
}
