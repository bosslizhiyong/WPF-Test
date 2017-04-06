using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Event.Core
{
    /// <summary>
    /// 事件处理器接口
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }
}
