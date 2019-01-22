using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT_CETC23.Common
{
    /*AttributeUsage预订义特性，可以使任何类、结构，枚举等具有属性标签
     * AttributeTargets.Field允许特性标签放置在字段前面，否则报错 ex class struct enum
     * AllowMultiple标记是否可以同一位置多次放置
     * Inherited标记是否可以被继承
     * Attribute自定义标签的基类
     */
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false,Inherited=false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description { get { return description; } }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes =
                (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }
}
