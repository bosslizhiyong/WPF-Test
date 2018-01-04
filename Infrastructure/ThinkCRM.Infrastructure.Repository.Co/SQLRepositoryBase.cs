using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Domain.Core;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    /// <summary>
    /// 基于SQLServer的仓储抽象基类
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public abstract class SQLRepositoryBase<TAggregateRoot> : SQLRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="daoCenter"></param>
        public SQLRepositoryBase(IRepositoryContext context, IDAOCenter daoCenter)
            : base(context, ConnectionStrings.ConnCRMCo.ToString(), daoCenter)
        {

        }

        protected abstract override void PersistAdd(TAggregateRoot aggregateRoot);
        protected abstract override void PersistUpdate(TAggregateRoot aggregateRoot);
        protected abstract override void PersistDelete(TAggregateRoot aggregateRoot);

        protected abstract override void CreateClusteDAO();
    }
}
