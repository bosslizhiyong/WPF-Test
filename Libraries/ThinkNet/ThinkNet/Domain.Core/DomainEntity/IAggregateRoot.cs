using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 聚合根接口,继承于该接口的类都是聚合根类
    /// </summary>
    public interface IAggregateRoot : IDomainEntity
    {
    }
}
