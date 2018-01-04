using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ThinkNet.Utility;

namespace ThinkCRM.Infrastructure.Co
{
    /// <summary>
    /// 验证用到正则表达式
    /// 2016年6月15日09:28:59
    /// </summary>
    public class ValidateRegex : RegexRule
    {

        /// <summary>
        /// 手机号码@"^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]))+\d{8})$"
        /// </summary>
        [Regex(Regex = @"^\d{11}$", Description = "验证手机号码", ErrorDesc = "手机号输入不正确!")]
        public string Mobile { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Regex(Regex = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", Description = "验证电子邮箱", ErrorDesc = "电子邮箱输入不正确!")]
        public string Email { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Regex(Regex = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", Description = "验证身份证号码", ErrorDesc = "身份证号码为15位数字或者18位数字或者17位数字+1位字母!")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 办公号码
        /// </summary>
        [Regex(Regex = @"^(\d{3,4}-)?\d{7,8}$", Description = "验证办公电话", ErrorDesc = "区号以0开头，3位或4位 号码由7位或8位数字组成区号与号码之间“-”连接!如(020-12345678)")]
        public string Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [Regex(Regex = @"^86-\d{2,3}-\d{7,8}$", Description = "验证传真号码", ErrorDesc = "传真号码格式为:以86开头-2或3位-7或8位,如(86-20-1234567)!")]
        public string Fax { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [Regex(Regex = @"^\d{6}$", Description = "验证邮政编码", ErrorDesc = "邮政编码为6位整数!")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 1位大写字母、1位或2位数字
        /// </summary>
        [Regex(Regex = @"^[A-Z]{1}$|^[1-9]{1}[\d]{0,1}$", Description = "", ErrorDesc = "输入1位大写字母或输入1位或2位数字!")]
        public string OneCharOrOneTwoNumber { get; set; }

        /// <summary>
        /// 只能输入整数
        /// </summary>
        [Regex(Regex = @"^[1-9]\d*|0$", Description = "", ErrorDesc = "只能输入正整数!")]
        public string Integer { get; set; }

        /// <summary>
        /// 只能输入数字0-9  ^[0-9]*$
        /// </summary>
        [Regex(Regex = @"^\d*\.{0,1}\d{0,1}$", Description = "", ErrorDesc = "只能输入数字!")]
        public string IntegerCode { get; set; }

        /// <summary>
        /// 只能输入数字0-9    ^[0-9]*$
        /// </summary>
        [Regex(Regex = @"^(\-?)\d+$", Description = "", ErrorDesc = "只能输入数字!")]
        public string IntegerAmount { get; set; }
    }
}
