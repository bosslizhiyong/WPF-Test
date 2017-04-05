using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Utility;
using ThinkNet.Query.Core;

namespace WCFWeb.Query.Co
{
    /// <summary>
    /// 数据查询基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryServiceBase<T> : SQLQueryBase<T>
        where T : DataEntityBase, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="daoCenter"></param>
        public QueryServiceBase(IDAOCenter daoCenter)
            : base(ConnectionStrings.ConnQueryCRMCo.ToString(), daoCenter)
        {

        }
    }
}
