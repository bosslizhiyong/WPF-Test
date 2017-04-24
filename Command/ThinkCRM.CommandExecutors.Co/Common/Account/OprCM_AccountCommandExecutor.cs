using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Commands.Co;
using ThinkNet.Command.Core;
using ThinkNet.DataEntity.Core;
using ThinkNet.Domain.Core;
using ThinkNet.Infrastructure.Core;

namespace ThinkCRM.CommandExecutors.Co.Common
{
    public class OprAccountCommandExecutor : ICommandExecutor<CM_AccountCommand>
    {
        private IDefaultDomainService service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public OprAccountCommandExecutor(IDefaultDomainService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd"></param>
        public void Execute(CM_AccountCommand cmd)
        {
            if (cmd.MCM_Account.DataEntityAction == DataEntityActions.Add)
            {
                service.Add(cmd.MCM_Account);
            }
            else  if(cmd.MCM_Account.DataEntityAction == DataEntityActions.Update)
            {
                if (cmd.MCM_Account.ID != 0)
                {
                      throw new ArgumentException("ID不存在!");
                }
                service.Update(cmd.MCM_Account);
            }
            else if (cmd.MCM_Account.DataEntityAction == DataEntityActions.Delete)
            {
                if (cmd.MCM_Account.ID != 0)
                {
                    throw new ArgumentException("ID不存在!!");
                }
                service.Delete(cmd.MCM_Account);
            }

            service.Context.Commit();
            if (service.SimpleResult != null)
            {
                cmd.SimpleResult = new SimpleResult(service.SimpleResult.Result, service.SimpleResult.Message);
            }
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="connectionStringName"></param>
        public void Execute(CM_AccountCommand cmd, string connectionStringName)
        {
            //方法内的第一句代码,设置外部数据库连接
            service.ExternalConnectionStringsName = connectionStringName;
            //执行命令
            Execute(cmd);
        }
    }
}
