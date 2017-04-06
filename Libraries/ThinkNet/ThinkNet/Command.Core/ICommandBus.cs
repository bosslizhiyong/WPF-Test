using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Command.Core
{
    /// <summary>
    /// 命令总线接口
    /// 作用：1、将一个Command派发给相应的CommandExecutor去执行
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        string ExternalConnectionStringsName { set; }
        /// <summary>
        /// 派发命令
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <param name="cmd">命令</param>
        void Send<TCommand>(TCommand cmd) where TCommand : ICommand;
    }
}
