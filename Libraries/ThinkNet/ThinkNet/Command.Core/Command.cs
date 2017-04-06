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
    /// 命令抽象类
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// 命令唯一标识(GUID)
        /// </summary>
        public object Key
        {
            get { return Guid.NewGuid().ToString(); }
        }
        /// <summary>
        /// 命令执行简单结果描述
        /// </summary>
        public virtual SimpleResult SimpleResult
        {
            get;
            set;
        }
    }
}
