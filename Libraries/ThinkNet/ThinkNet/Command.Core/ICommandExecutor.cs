using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Command.Core
{
    /// <summary>
    /// 命令执行器接口
    /// 作用：1、验证传入的Command对象是否合法(非验证业务逻辑)
    ///       2、调用领域模型完成操作
    /// </summary>
    public interface ICommandExecutor<TCommand> 
        where TCommand : ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令</param>
        void Execute(TCommand cmd);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="connectionStringName">外部设置的数据库名称</param>
        void Execute(TCommand cmd, string connectionStringName);
    }
}
