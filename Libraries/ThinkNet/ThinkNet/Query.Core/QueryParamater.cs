using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 查询参数
    /// </summary>
    [Serializable]
    public sealed class QueryParamater
    {
        #region 字    段

        private static readonly QueryParamater all = new QueryParamater();
        /// <summary>
        /// where条件
        /// </summary>
        public string Where = "";
        /// <summary>
        /// 排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)
        /// </summary>
        public bool OrderType = false;
        /// <summary>
        /// 字段名称(逗号分隔)
        /// </summary>
        public string Columns = "";
        /// <summary>
        /// 排序字段(注:可以直接加上DESC/ASC)
        /// </summary>
        public string OrderField = "";
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName = "";

        #endregion

        #region 属    性

        internal static QueryParamater All
        {
            get { return QueryParamater.all; }
        } 

        #endregion

        #region 构造函数

        #endregion

        #region 基本方法

        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <returns></returns>
        public static QueryParamater Create()
        {
            return new QueryParamater();
        }
        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        public static QueryParamater Create(string where)
        {
            return new QueryParamater { Where = where };
        }
        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <returns></returns>
        public static QueryParamater Create(string where, bool orderType)
        {
            return new QueryParamater { Where = where, OrderType = orderType };
        }
        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔)</param>
        /// <returns></returns>
        public static QueryParamater Create(string where, bool orderType, string columns)
        {
            return new QueryParamater { Where = where, OrderType = orderType, Columns = columns };
        }
        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        public static QueryParamater Create(string where, bool orderType, string columns, string orderField)
        {
            return new QueryParamater { Where = where, OrderType = orderType, Columns = columns, OrderField = orderField };
        }
        /// <summary>
        /// 创建查询条件
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="columns">字段名称(逗号分隔)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <param name="tableName">数据表名称</param>
        /// <returns></returns>
        public static QueryParamater Create(string where, bool orderType, string columns, string orderField, string tableName)
        {
            return new QueryParamater { Where = where, OrderType = orderType, Columns = columns, OrderField = orderField, TableName = tableName };
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
