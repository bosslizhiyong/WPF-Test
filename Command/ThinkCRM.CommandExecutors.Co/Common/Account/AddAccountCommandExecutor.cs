using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Commands.Co;
using ThinkNet.Command.Core;
using ThinkNet.Domain.Core;
using ThinkNet.Infrastructure.Core;

namespace ThinkCRM.CommandExecutors.Co
{
    public class AddAccountCommandExecutor : ICommandExecutor<AddAccountCommand>
    {
        private IDefaultDomainService service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public AddAccountCommandExecutor(IDefaultDomainService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd"></param>
        public void Execute(AddAccountCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.MCM_Account.DefineAccountCode))
            {
                throw new ArgumentException("编号是必须的!");
            }

            service.Add(cmd.MCM_Account);
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
        public void Execute(AddAccountCommand cmd, string connectionStringName)
        {
            //方法内的第一句代码,设置外部数据库连接
            service.ExternalConnectionStringsName = connectionStringName;
            //执行命令
            Execute(cmd);
        }
    }

}
