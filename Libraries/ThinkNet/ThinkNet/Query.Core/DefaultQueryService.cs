using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Query.Core
{
    public class DefaultQueryService:IQueryService
    {
        public IQuery<T> Create<T>() where T : DataEntityBase
        {
            //IOC 控制反转
            return ObjectContainer.Resolve<IQuery<T>>();
        }
    }
}
