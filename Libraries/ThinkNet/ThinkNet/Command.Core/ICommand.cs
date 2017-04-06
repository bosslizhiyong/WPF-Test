using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;

namespace ThinkNet.Command.Core
{
    /// <summary>
    /// 命令接口
    /// 作用：1、封装命令数据，以属性为主，少量简单方法(不包含业务逻辑)
    ///       2、实现编译时约束，可以限制传入CommandExecutor的都是Command对象，而不会不小心传错对象（所有的Command对象都必须实现ICommand接口）
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 命令唯一标识(GUID)
        /// </summary>
        object Key { get; }

        /// <summary>
        /// 命令执行简单结果描述
        /// </summary>
        SimpleResult SimpleResult { get; set; }
    }
}
