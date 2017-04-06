using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Event.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域事件接口
    /// </summary>
    public interface IDomainEvent : IEvent
    {
        /// <summary>
        /// 领域事件的事件源对象
        /// </summary>
        IDomainEntity EventSource { get; }
    }
}
