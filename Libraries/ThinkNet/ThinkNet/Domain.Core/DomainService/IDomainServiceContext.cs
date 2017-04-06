using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域服务上下文接口
    /// </summary>
    public interface IDomainServiceContext : IUnitOfWork
    {
        /// <summary>
        /// 将指定的数据实体添加到服务中
        /// </summary>
        /// <param name="dataEntity">数据实体实例</param>
        /// <param name="serviceUnitOfWork">服务工作单元实例</param>
        void RegisterAdd<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase;
        /// <summary>
        /// 更新服务中指定的数据实体
        /// </summary>
        /// <param name="dataEntity">数据实体实例</param>
        /// <param name="serviceUnitOfWork">服务工作单元实例</param>
        void RegisterUpdate<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase;
        /// <summary>
        /// 将指定的聚合根从仓储中删除
        /// </summary>
        /// <param name="dataEntity">数据实体实例</param>
        /// <param name="serviceUnitOfWork">服务工作单元实例</param>
        void RegisterDelete<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase;

        /// <summary>
        /// 注册SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        void RegisterSql(string strSql, IDomainServiceUnitOfWork serviceUnitOfWork);

        /// <summary>
        /// 注册插入SQL语句
        /// </summary>
        /// <param name="insertSql">插入SQL语句</param>
        void RegisterInsertSql(string insertSql, IDomainServiceUnitOfWork serviceUnitOfWork);
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="updateSql">更新SQL语句</param>
        void RegisterUpdateSql(string updateSql, IDomainServiceUnitOfWork serviceUnitOfWork);
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="deleteSql">删除SQL语句</param>
        void RegisterDeleteSql(string deleteSql, IDomainServiceUnitOfWork serviceUnitOfWork);

        /// <summary>
        /// 注册存储过程
        /// </summary>
        /// <param name="dicProcedure">存储过程字典(存储过程名称及参数列表)</param>
        void RegisterProcedure(Dictionary<string, List<DbParameter>> dicProcedure, IDomainServiceUnitOfWork serviceUnitOfWork);
    }
}
