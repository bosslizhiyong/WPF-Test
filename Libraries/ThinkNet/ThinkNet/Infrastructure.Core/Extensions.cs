using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 扩展工具类
    /// </summary>
    public static class Extensions
    {
        #region 集合对象自动拼接成字符串
        /// <summary>
        /// 集合对象自动拼接成字符串
        /// </summary>
        /// <param name="list">List集合</param>
        /// <param name="joinChar">拼接字符(如:,或AND之类)</param>
        /// <returns></returns>
        public static string JoinToString(this IList list, string joinChar)
        {
            if (list != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int n = 0; n < list.Count; n++)
                {
                    if (n != list.Count - 1)
                        sb.AppendFormat("{0}{1}", list[n], !string.IsNullOrEmpty(joinChar) ? joinChar : ",");
                    else
                        sb.AppendFormat("{0}", list[n]);
                }
                return sb.ToString();
            }
            else
                return string.Empty;
        }
        /// <summary>
        /// 集合对象自动拼接成字符串(逗号分隔)
        /// </summary>
        /// <param name="list">List集合</param>
        /// <returns></returns>
        public static string JoinToString(this IList list)
        {
            return JoinToString(list, null);
        }
        #endregion
    }
}
