using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 数据列验证类型(枚举)
    /// </summary>
    [Flags]
    public enum VerificationTypes
    {
        /// <summary>
        /// 正确
        /// </summary>
        Correct = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 1,//不通过验证
        /// <summary>
        /// 重复
        /// </summary>
        Repeat = 2,
        /// <summary>
        /// 不存在
        /// </summary>
        NotExist = 3,//不通过验证
        /// <summary>
        /// 存在多个
        /// </summary>
        MultiExist = 4,//不通过验证
        /// <summary>
        /// 存在
        /// </summary>
        Exist = 5,//不通过验证
        /// <summary>
        /// 不存在(自动创建)
        /// </summary>
        NotExistExt = 6,//不存在(自动创建)
    }
}
