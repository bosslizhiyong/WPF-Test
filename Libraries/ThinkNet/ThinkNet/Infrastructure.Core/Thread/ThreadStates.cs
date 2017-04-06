using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 线程状态枚举
    /// </summary>
    [Flags]
    public enum ThreadStates
    {
        /// <summary>
        /// 运行
        /// </summary>
        [Description("运行")]
        Running = 1,
        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stopped = 2
    }
}
