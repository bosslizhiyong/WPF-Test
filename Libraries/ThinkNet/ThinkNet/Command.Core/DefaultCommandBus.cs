using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Command.Core
{
    /// <summary>
    /// 默认的命令总线
    /// </summary>
    public class DefaultCommandBus : ICommandBus
    {
        protected string _connectionStringName = "";

        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ExternalConnectionStringsName
        {
            set
            {
                _connectionStringName = value;
            }
        }

        /// <summary>
        /// 派发命令
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <param name="cmd">命令</param>
        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            //IOC 控制反转
            var executor = ObjectContainer.Resolve<ICommandExecutor<TCommand>>();
            if (!string.IsNullOrEmpty(_connectionStringName))
            {
                executor.Execute(cmd, _connectionStringName);
            }
            else
            {
                executor.Execute(cmd);
            }
        }
    }
}
