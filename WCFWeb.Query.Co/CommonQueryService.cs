using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Query.Core;

namespace WCFWeb.Query.Co
{
    public class CommonQueryService: SQLCommonQueryBase
    {
        /// <summary>
        /// 
        /// </summary>
        public CommonQueryService()
            : base(ConnectionStrings.ConnQueryCRMCo.ToString())
        {
            
        }
    }
}
