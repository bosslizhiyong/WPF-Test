using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 查询接口,专为UI所用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T> : ICommonQuery
    {
        /// <summary>
        /// 获取一行数据，面向表单
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        T GetSingleObject(QueryParamater queryParamater);
        /// <summary>
        /// 获取分页列表，面向支持分页的表格
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageResult<T> GetPagerList(QueryParamater queryParamater, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据列表，面向不支持分页的表格或下拉框
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        IEnumerable<T> GetList(QueryParamater queryParamater);
        /// <summary>
        /// 获取分页集合，面向支持分页的表格
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageResult<DataRow> GetPagerTable(QueryParamater queryParamater, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合，面向不支持分页的表格或下拉框
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        IEnumerable<DataRow> GetTable(QueryParamater queryParamater);
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        int GetCount(QueryParamater queryParamater);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        bool Exists(QueryParamater queryParamater);
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <returns></returns>
        int GetNextSequence();
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        int GetNextSequence(int parentId);
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        int GetNextSequence(string where);
    }
}
