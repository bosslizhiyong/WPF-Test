using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 仓储上下文接口
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork
    {
        /// <summary>
        /// 将指定的聚合根添加到仓储中
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        /// <param name="repositoryUnitOfWork">仓储工作单元实例</param>
        void RegisterAdd(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork);
        /// <summary>
        /// 更新仓储中指定的聚合根
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        /// <param name="repositoryUnitOfWork">仓储工作单元实例</param>
        void RegisterUpdate(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork);
        /// <summary>
        /// 将指定的聚合根从仓储中删除
        /// </summary>
        /// <param name="aggregateRoot">聚合根实例</param>
        /// <param name="repositoryUnitOfWork">仓储工作单元实例</param>
        void RegisterDelete(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork);

        /// <summary>
        /// 注册插入SQL语句
        /// </summary>
        /// <param name="insertSql">插入SQL语句</param>
        void RegisterInsertSql(string insertSql, IRepositoryUnitOfWork repositoryUnitOfWork);
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="updateSql">更新SQL语句</param>
        void RegisterUpdateSql(string updateSql, IRepositoryUnitOfWork repositoryUnitOfWork);
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="deleteSql">删除SQL语句</param>
        void RegisterDeleteSql(string deleteSql, IRepositoryUnitOfWork repositoryUnitOfWork);

        /// <summary>
        /// 注册存储过程
        /// </summary>
        /// <param name="dicProcedure">存储过程字典(存储过程名称及参数列表)</param>
        void RegisterProcedure(Dictionary<string, List<DbParameter>> dicProcedure, IRepositoryUnitOfWork repositoryUnitOfWork);
    }
}
