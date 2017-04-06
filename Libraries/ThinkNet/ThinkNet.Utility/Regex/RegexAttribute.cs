using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 正则表达式特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RegexAttribute : Attribute
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Regex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDesc { get; set; }
    }
}
