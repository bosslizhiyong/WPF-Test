using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    public interface IMessageHandler
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        SimpleResult Send();
    }
}
