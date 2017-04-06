using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 正则表达式项
    /// </summary>
    public class RegexItem
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public RegexItem()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regex">正则表达式</param>
        /// <param name="description">描述</param>
        /// <param name="errorDesc">错误描述</param>
        public RegexItem(string regex, string description, string errorDesc)
        {
            this.Regex = regex;
            this.Description = description;
            this.ErrorDesc = errorDesc;
        }
    }
}
