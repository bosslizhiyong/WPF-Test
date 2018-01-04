using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Domain.Core;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Utility;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    /// <summary>
    /// 基于SQLServer的服务抽象基类
    /// </summary>
    public abstract class SQLDomainServiceBase : SQLDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="daoCenter"></param>
        public SQLDomainServiceBase(IDomainServiceContext context, IDAOCenter daoCenter)
            : base(context, ConnectionStrings.ConnCRMCo.ToString(), daoCenter)
        {

        }

        protected abstract override void CreateClusteDAO();
    }
}
