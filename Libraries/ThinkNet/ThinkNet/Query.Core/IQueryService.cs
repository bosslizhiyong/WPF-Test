using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Query.Core
{
    public interface IQueryService
    {
        IQuery<T> Create<T>() where T : DataEntityBase;
    }
}
