using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Persistence.Core
{
    public interface IDAOCenter
    {
        IDAO<T> Create<T>() where T : DataEntityBase;
        IDAO<T> Create<T>(string connectionStringName) where T : DataEntityBase;
    }
}
