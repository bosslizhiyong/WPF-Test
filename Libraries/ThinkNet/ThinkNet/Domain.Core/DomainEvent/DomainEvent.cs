using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域事件抽象类
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        #region 字    段

        private readonly IDomainEntity eventSource;

        #endregion

        #region 属    性

        public IDomainEntity EventSource
        {
            get { return eventSource; }
        }

        #endregion

        #region 构造函数
        public DomainEvent()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventSource">代表事件来源的领域实体</param>
        public DomainEvent(IDomainEntity eventSource)
        {
            this.eventSource = eventSource;
        }
        #endregion

        #region 基本方法

        #endregion

        #region 其他方法

        #endregion
    }
}
