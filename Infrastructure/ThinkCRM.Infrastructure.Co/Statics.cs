using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Infrastructure.Core;

namespace ThinkCRM.Infrastructure.Co
{
    public class Statics
    {
        /// <summary>
        /// 数据库连接集合
        /// </summary>
        public static List<CM_Database> ConnectionStrings = new List<CM_Database>();

        /// <summary>
        /// 线程字典集合
        /// </summary>
        public static Dictionary<string, List<SimpleThread>> DicThreads = null;

        /// <summary>
        /// 价格的最大小数位数
        /// </summary>
        public static int MaxDigitsPrice = 4;
        /// <summary>
        /// 金额的最大小数位数
        /// </summary>
        public static int MaxDigitsMoney = 4;
        /// <summary>
        /// 数量的最大小数位数
        /// </summary>
        public static int MaxDigitsAmount = 4;
    }
}
