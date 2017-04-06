using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SocketDictionary
    {
        /// <summary>
        /// 结果编号集
        /// </summary>
        [Flags]
        public enum ResultCodes
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            SUCCESS = 0,
            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            FAIL = 101,
            /// <summary>
            /// 登陆异常
            /// </summary>
            [Description("登陆异常")]
            LOGIN = 443,
            /// <summary>
            /// 授权异常
            /// </summary>
            [Description("授权异常")]
            AUTHORIZATION = 444,
            /// <summary>
            /// 不存在
            /// </summary>
            [Description("不存在")]
            NOTEXISTS = 555,
            /// <summary>
            /// 实体NULL
            /// </summary>
            [Description("实体NULL")]
            NULL = 888,
            /// <summary>
            /// 异常
            /// </summary>
            [Description("异常")]
            EXCEPTION = 999
        }

        /// <summary>
        /// 简单标识集
        /// </summary>
        [Flags]
        public enum SimpleTypes
        {
            /// <summary>
            /// 广播
            /// </summary>
            [Description("广播")]
            BroadCast = 1,
            /// <summary>
            /// 打开或关闭服务端的服务
            /// </summary>
            [Description("打开或关闭服务端的服务")]
            ServerOpenOrClose = 2
        }
    }
}
