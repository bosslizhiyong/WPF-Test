using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 应用于某种聚合根的仓储类的接口
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        string ExternalConnectionStringsName { set; }
        /// <summary>
        /// 当前仓储所使用的仓储上下文实例
        /// </summary>
        IRepositoryContext Context { get; }

        /// <summary>
        /// 将指定的聚合根添加到仓储中
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        void Add(TAggregateRoot aggregateRoot);
        /// <summary>
        /// 更新仓储中指定的聚合根
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        void Update(TAggregateRoot aggregateRoot);
        /// <summary>
        /// 将指定的聚合根从仓储中删除
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        void Delete(TAggregateRoot aggregateRoot);

        /// <summary>
        /// 执行插入语句
        /// </summary>
        /// <param name="insertSql"></param>
        void ExecuteInsertSql(string insertSql);
        /// <summary>
        /// 执行更新语句
        /// </summary>
        /// <param name="updateSql"></param>
        void ExecuteUpdateSql(string updateSql);
        /// <summary>
        /// 执行删除语句
        /// </summary>
        /// <param name="deleteSql"></param>
        void ExecuteDeleteSql(string deleteSql);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        void ExecuteProcedure(string procName, List<DbParameter> parameters);
    }
}
