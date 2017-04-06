using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 正则表达式帮助类
    /// </summary>
    public class RegexHelper
    {
        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        public static bool IsMatch(string strInput, string strRegex)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(strInput);
            return mc.Success;
        }

        /// <summary>
        /// 单个匹配内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="iGroupIndex">分组序号, 从1开始, 0不分组</param>
        public static string GetText(string strInput, string strRegex, int iGroupIndex)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(strInput);
            if (mc.Success)
            {
                if (iGroupIndex > 0)
                    return mc.Groups[iGroupIndex].Value;
                else
                    return mc.Value;
            }
            else
                return "";
        }
        /// <summary>
        /// 单个匹配内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="strGroupName">分组名, ""代表不分组</param>
        public static string GetText(string strInput, string strRegex, string strGroupName)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(strInput);
            if (mc.Success)
            {
                if (strGroupName != "")
                    return mc.Groups[strGroupName].Value;
                else
                    return mc.Value;
            }
            else
                return "";
        }

        /// <summary>
        /// 多个匹配内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="iGroupIndex">第几个分组, 从1开始, 0代表不分组</param>
        public static List<string> GetList(string strInput, string strRegex, int iGroupIndex)
        {
            List<string> list = new List<string>();
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(strInput);
            foreach (Match mc in mcs)
            {
                if (iGroupIndex > 0)
                    list.Add(mc.Groups[iGroupIndex].Value);
                else
                    list.Add(mc.Value);
            }
            return list;
        }
        /// <summary>
        /// 多个匹配内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="strGroupName">分组名, ""代表不分组</param>
        public static List<string> GetList(string strInput, string strRegex, string strGroupName)
        {
            List<string> list = new List<string>();
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(strInput);
            foreach (Match mc in mcs)
            {
                if (strGroupName != "")
                    list.Add(mc.Groups[strGroupName].Value);
                else
                    list.Add(mc.Value);
            }
            return list;
        }

        /// <summary>
        /// 替换指定内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="strReplace">替换值</param>
        /// <param name="iGroupIndex">分组序号, 0代表不分组</param>
        public static string Replace(string strInput, string strRegex, string strReplace, int iGroupIndex)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(strInput);
            foreach (Match mc in mcs)
            {
                if (iGroupIndex > 0)
                    strInput = strInput.Replace(mc.Groups[iGroupIndex].Value, strReplace);
                else
                    strInput = strInput.Replace(mc.Value, strReplace);
            }
            return strInput;
        }
        /// <summary>
        /// 替换指定内容
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="strReplace">替换值</param>
        /// <param name="strGroupName">分组名, "" 代表不分组</param>
        public static string Replace(string strInput, string strRegex, string strReplace, string strGroupName)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(strInput);
            foreach (Match mc in mcs)
            {
                if (strGroupName != "")
                    strInput = strInput.Replace(mc.Groups[strGroupName].Value, strReplace);
                else
                    strInput = strInput.Replace(mc.Value, strReplace);
            }
            return strInput;
        }

        /// <summary>
        /// 分割
        /// </summary>
        /// <param name="strInput">输入内容</param>
        /// <param name="strRegex">表达式字符串</param>
        /// <param name="iStrLen">最小保留字符串长度</param>
        public static List<string> Split(string strInput, string strRegex, int iStrLen)
        {
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            string[] sArray = re.Split(strInput);
            List<string> list = new List<string>();
            list.Clear();
            foreach (string s in sArray)
            {
                if (s.Trim().Length < iStrLen)
                    continue;
                list.Add(s.Trim());
            }
            return list;
        }

        /// <summary>
        /// 正则表达式项
        /// </summary>
        /// <param name="name">正则表达式属性名称</param>
        /// <returns></returns>
        public static RegexItem GetRegexItem<T>(string name) where T : class ,new()
        {
            RegexItem item = new RegexItem();
            Type objType = typeof(T);

            //取属性上的自定义特性
            PropertyInfo propInfo = objType.GetProperty(name);
            object[] objAttrs = propInfo.GetCustomAttributes(typeof(RegexAttribute), true);
            if (objAttrs.Length > 0)
            {
                RegexAttribute attr = objAttrs[0] as RegexAttribute;
                if (attr != null)
                {
                    item = new RegexItem(attr.Regex, attr.Description, attr.ErrorDesc);
                }
            }

            //foreach (PropertyInfo propInfo in objType.GetProperties())
            //{
            //    if (propInfo.Name != name) continue;
            //    object[] objAttrs = propInfo.GetCustomAttributes(typeof(RegexAttribute), true);
            //    if (objAttrs.Length > 0)
            //    {
            //        RegexAttribute attr = objAttrs[0] as RegexAttribute;
            //        if (attr != null)
            //        {
            //            item = new RegexItem(attr.Regex, attr.Description, attr.ErrorDesc);
            //        }
            //    }
            //}
            return item;
        }
    }
}
