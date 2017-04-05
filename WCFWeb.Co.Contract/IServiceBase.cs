using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFWeb.Co.Contract
{
    public interface IServiceBase
    {
        /// <summary>
        /// 设置外部数据库名称
        /// </summary>
        /// <param name="connectionStringName">数据库连接名称</param>
        [OperationContract]
        void SetExternalConnectionStringsName(string connectionStringName);
    }
}
