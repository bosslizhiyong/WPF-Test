using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域实体接口,继承于该接口的类都是领域实体类
    /// </summary>
    public interface IDomainEntity
    {
        /// <summary>
        /// 当前领域实体的全局唯一标识
        /// </summary>
        object Key { get; }
    }
}
