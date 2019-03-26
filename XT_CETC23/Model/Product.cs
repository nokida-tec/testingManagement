using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT_CETC23.DataCom;
using XT_CETC23.Model;
using XT_CETC23.Common;

namespace XT_CETC23.Model
{
    class Product
    {
        public enum ProductType
        {
            [EnumDescription("A")]
            A = 1,
            [EnumDescription("B")]
            B = 2,
            [EnumDescription("C")]
            C = 3,
            [EnumDescription("D")]
            D = 4,
            [EnumDescription("E")]
            E = 5,
            [EnumDescription("F")]
            F = 6,
        }
    }
}