using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 扩展IQuery<T>接口
    /// </summary>
    public static class IQueryExtentions
    {
        /// <summary>
        /// 根据Id获取记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="thisQuery"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetSingleObjectById<T, TId>(this IQuery<T> thisQuery, TId id)
        {
            var query = QueryParamater.Create(string.Format("ID={0}", id));
            return thisQuery.GetSingleObject(query);
        }

        /// <summary>
        /// 获取所有记录列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisQuery"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetListAll<T>(this IQuery<T> thisQuery)
        {
            return thisQuery.GetList(QueryParamater.Create());
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisQuery"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> GetTableAll<T>(this IQuery<T> thisQuery)
        {
            return thisQuery.GetTable(QueryParamater.Create());
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool ExistsById<T, TId>(this IQuery<T> thisQuery, TId id)
        {
            var query = QueryParamater.Create(string.Format("ID={0}", id));
            return thisQuery.Exists(query);
        }
    }
}
