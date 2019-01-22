using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.DataCom
{
    class ActualData<T, K> where T : struct where K : class
    {
        K ProductID;
        K ProductType;
        T FrameLocation;
        T SalverLocation;
        K CheckCabinetA;
        K CheckCabinetB;
        K CheckDate;
        K CheckTime;
        K CheckBatch;
        K CheckResult;
    }
}
