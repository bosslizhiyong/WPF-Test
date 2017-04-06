using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Persistence.Core
{
    /// <summary>
    /// 分页参数类接口
    /// </summary>
    public interface IPager
    {
        /// <summary>
        /// where条件(可为空)
        /// </summary>
        string Where { get; }
        /// <summary>
        /// 当前页数(-1或0均转为1)
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 每页显示记录数(-1或0均转为int.MaxValue)
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)
        /// </summary>
        bool OrderType { get; }
        /// <summary>
        /// 字段名称(逗号分隔,可为空)
        /// </summary>
        string Columns { get; }
        /// <summary>
        /// 排序字段(注:可以直接加上DESC/ASC,可为空)
        /// </summary>
        string OrderField { get; }
        /// <summary>
        /// 数据表名称(可为空)
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 获取分页的Sql语句
        /// </summary>
        /// <param name="strSql">未分页的sql语句</param>
        /// <param name="pageIndex">当前页数(-1或0均转为1)</param>
        /// <param name="pageSize">每页显示记录数(-1或0均转为int.MaxValue)</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        string GetPagerSql(string strSql, int pageIndex, int pageSize, bool orderType, string orderField);
        /// <summary>
        /// 获取分页的Sql语句的总记录数
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        string GetCountSql(string strSql);
    }
}
