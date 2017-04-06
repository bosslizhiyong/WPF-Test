using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域服务工作单元
    /// </summary>
    public interface IDomainServiceUnitOfWork
    {
        void PersistAdd<T>(T dataEntity) where T : DataEntityBase;
        void PersistUpdate<T>(T dataEntity) where T : DataEntityBase;
        void PersistDelete<T>(T dataEntity) where T : DataEntityBase;

        void PersistExecuteNonQuery(string strSql);
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
