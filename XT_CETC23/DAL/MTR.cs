﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.DataCom
{
    class MTR 
    {
        static MTR mtr;
        public static int globalBasicID;
        DataBase db;
        DataTable dt_Mtr;
        public static MTR GetIntanse()
        {
            if(mtr==null)
            {
                mtr = new MTR();
            }
            return mtr;
        }
        MTR()
        {
            db = DataBase.GetInstanse();
        }
        //更新料架数据
        public void updateFrameBasicID()
        {
            db.DBUpdate("update dbo.BasicID set BasicID='" + GetID.getID() + "',HavePiece='"+1 +"'where EquipmentName='"+"Frame'");
           
        }
        //清除料架数据
        public void deleteFrameBasicID()
        {
            db.DBDelete("update dbo.BasicID set BasicID='0',HavePiece=0 where EquipmentName='Frame'");
                //"delete from " + tableName + " where " + column + "='" + condition + "'";
        }

        public int InsertBasicID(string ProductID,int FrameLocation,int SalverLocation,string ProductType,string CurrentStation, bool StationSign,string ProductChectResult,int CabinetID)
        {
            int lBasicID = GetID.getID();
            dt_Mtr = db.DBQuery("select * from dbo.MTR");
            for(int i=0;i<dt_Mtr.Rows.Count;i++)
            {
                if ((int)dt_Mtr.Rows[i]["BasicID"]== lBasicID)
                { lBasicID = GetID.getID(); }
            }

            string tmpText = "insert into dbo.MTR(ProductID,FrameLocation,SalverLocation,ProductType,CurrentStation,StationSign,ProductCheckResult,BasicID,CabinetID)values('" + ProductID + "'," + FrameLocation + "," + SalverLocation + ",'" + ProductType + "','" + CurrentStation + "','" + StationSign + "','" + ProductChectResult + "'," + lBasicID + "," + CabinetID + ")";
            db.DBInsert("insert into dbo.MTR(ProductID,FrameLocation,SalverLocation,ProductType,CurrentStation,StationSign,ProductCheckResult,BasicID,CabinetID)values('" + ProductID + "'," + FrameLocation + "," + SalverLocation + ",'" + ProductType + "','" + CurrentStation + "','" + StationSign + "','" + ProductChectResult + "'," + lBasicID + "," + CabinetID + ")");
            return lBasicID;
        }

        void updateBasicID(string ProductID, int FrameLocation, int SalverLocation, string ProductType, string ProductCurrentPos, string ProductSign, string ProductChectResult)
        {

        }

    }
    class GetID
    {
        static int BasicID = 2000;
        public static int getID()
        {
            if (BasicID < 2999)
                return BasicID++;
            else
            {
                BasicID = 2000;
                return BasicID++;
            }
        }
    }
}
