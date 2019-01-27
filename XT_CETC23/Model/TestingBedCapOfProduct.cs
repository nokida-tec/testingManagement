using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.Model
{
    class TestingBedCapOfProduct
    {
        static public TestingBedCapOfProduct[] sTestingBedCapOfProduct = 
        {
            new TestingBedCapOfProduct(0, "未定义", "NG", 0),
            new TestingBedCapOfProduct(1, "A组件", "A", 1),
            new TestingBedCapOfProduct(2, "B组件", "B", 2),
            new TestingBedCapOfProduct(3, "2类组件", "E", 5),
            new TestingBedCapOfProduct(4, "AB组件", "F", 6),
            new TestingBedCapOfProduct(5, "C组件", "C", 3),
            new TestingBedCapOfProduct(6, "D组件", "D", 4),
        };

        private TestingBedCapOfProduct (int id, string showName, string productType, byte plcMode)
        {
            this.id = id;
            this.showName = showName;
            this.productType = productType;
            this.plcMode = plcMode;
        }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        private string showName;
        public string ShowName
        {
            get { return showName; }
            set
            {
                showName = value;
            }
        }

        private string productType;
        public string ProductType
        {
            get { return productType; }
            set
            {
                productType = value;
            }
        }

        private byte plcMode;
        public byte PlcMode
        {
            get { return plcMode; }
        }
    }
}
