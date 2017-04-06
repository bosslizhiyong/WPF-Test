using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;

namespace ThinkNet.Persistence.Core
{
    public class DefaultDAOCenter : IDAOCenter
    {
        public IDAO<T> Create<T>() where T : DataEntity.Core.DataEntityBase
        {
            //IOC 控制反转
            return ObjectContainer.Resolve<IDAO<T>>();
        }

        public IDAO<T> Create<T>(string connectionStringName) where T : DataEntity.Core.DataEntityBase
        {
            //IOC 控制反转
            IDAO<T> dao = Create<T>();
            //使用外部连接
            if (!string.IsNullOrEmpty(connectionStringName))
            {
                dao.IsExternalConnection = true;
                dao.ExternalConnectionStringsName = connectionStringName;
            }
            return dao;
        }
    }
}
