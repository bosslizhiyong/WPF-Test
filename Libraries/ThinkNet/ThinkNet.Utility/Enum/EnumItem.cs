using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Utility
{
    public class EnumItem
    {
        /// <summary>
        /// 枚举的值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 枚举的文本
        /// </summary>
        public string Text { get; set; }

        public EnumItem()
        { }

        public EnumItem(string value,string text)
        {
            this.Value = value;
            this.Text = text;
        }
    }
}
