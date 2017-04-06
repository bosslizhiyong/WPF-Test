using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.DataEntity.Core
{
    /// <summary>
    /// 实体动作(枚举)
    /// </summary>
    [Flags]
    public enum DataEntityActions
    {
        Delete = -1,
        Default=0,
        Add=1,
        Update=2
    }
}
