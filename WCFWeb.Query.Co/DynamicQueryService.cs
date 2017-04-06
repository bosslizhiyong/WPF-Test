using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Query.Core;

namespace WCFWeb.Query.Co
{
    public class DynamicQueryService : SQLDynamicQueryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="daoCenter"></param>
        public DynamicQueryService(IDAOCenter daoCenter)
            : base(ConnectionStrings.ConnQueryCRMCo.ToString(), daoCenter)
        {
            
        }
    }
}
