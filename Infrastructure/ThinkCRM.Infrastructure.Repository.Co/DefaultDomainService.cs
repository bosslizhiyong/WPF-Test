using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Domain.Core;
using ThinkNet.Persistence.Core;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    public class DefaultDomainService : SQLDomainServiceBase, IDefaultDomainService
    {
        #region 字    段

        #endregion

        #region 属    性

        #endregion

        #region 构造函数
        public DefaultDomainService()
            : this(null,null)
        {
        }

        public DefaultDomainService(IDomainServiceContext context, IDAOCenter daoCenter)
            : base(context,daoCenter)
        {

        }
        #endregion

        #region 基本方法

        #endregion

        #region 其他方法
        protected override void CreateClusteDAO()
        {
            
        }
        #endregion
    }
}
