using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.INTransfer
{
   public interface IRunForm
    {
        void getPlcMode(string mode,string status);
        void getInitStatus(bool sta);
        void getProductID(string id);
        void getCabinetResult(int CabinetNum,string message);
        void getCabinetStatus(int CabinetNum, string message);
        void getGrabNO(int grabNum);
        void transMessage(string message);
        void getGrab(string[] str);
        void getStatus(bool[] bl);
    }
}
