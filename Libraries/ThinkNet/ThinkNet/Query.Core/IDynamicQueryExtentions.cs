using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 扩展IDynamicQuery接口
    /// </summary>
    public static class IDynamicQueryExtentions
    {
        /// <summary>
        /// 根据Id获取记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="thisQuery"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetSingleObjectById<T, TId>(this IDynamicQuery thisQuery, TId id) where T : DataEntityBase
        {
            var query = QueryParamater.Create(string.Format("ID={0}", id));
            return thisQuery.GetSingleObject<T>(query);
        }

        /// <summary>
        /// 获取所有记录列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisQuery"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetListAll<T>(this IDynamicQuery thisQuery) where T : DataEntityBase
        {
            return thisQuery.GetList<T>(QueryParamater.Create());
        }

        /// <summary>
        /// 获取所有记录集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisQuery"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> GetTableAll<T>(this IDynamicQuery thisQuery) where T : DataEntityBase
        {
            return thisQuery.GetTable<T>(QueryParamater.Create());
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool ExistsById<T, TId>(this IDynamicQuery thisQuery, TId id) where T : DataEntityBase
        {
            var query = QueryParamater.Create(string.Format("ID={0}", id));
            return thisQuery.Exists<T>(query);
        }
    }
}
