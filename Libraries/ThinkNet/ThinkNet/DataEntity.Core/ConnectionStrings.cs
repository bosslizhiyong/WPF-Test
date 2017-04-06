using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.DataEntity.Core
{
    /// <summary>
    /// 数据库连接名称(枚举)
    /// </summary>
    [Flags]
    public enum ConnectionStrings
    {
        ConnCRM = 1,
        ConnQueryCRM = 2,
        ConnCRMCo = 3,
        ConnQueryCRMCo = 4
    }
}
