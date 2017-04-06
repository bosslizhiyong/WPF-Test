using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 仓储工作单元接口
    /// </summary>
    public interface IRepositoryUnitOfWork
    {
        void PersistAdd(AggregateRoot aggregateRoot);
        void PersistUpdate(AggregateRoot aggregateRoot);
        void PersistDelete(AggregateRoot aggregateRoot);

        void PersistInsertSql(string insertSql);
        void PersistUpdateSql(string updateSql);
        void PersistDeleteSql(string deleteSql);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        void PersistExecuteProcedure(string procName, List<DbParameter> parameters);
    }
}
