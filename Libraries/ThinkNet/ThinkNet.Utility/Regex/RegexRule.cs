using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 通用正则表达式规则
    /// </summary>
    public class RegexRule
    {
        /// <summary>
        /// 
        /// </summary>
        [RegexAttribute(Regex = @"^-?[0-9]\d*$", Description = "验证数字", ErrorDesc = "请输入数字!")]
        public string Number
        { get; set; }
    }
}
