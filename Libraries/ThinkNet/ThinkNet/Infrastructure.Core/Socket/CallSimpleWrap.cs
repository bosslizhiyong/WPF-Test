using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Socket调用的简单包装
    /// </summary>
    [Serializable]
    public class CallSimpleWrap
    {
        /// <summary>
        /// 简单标识
        /// </summary>
        private SocketDictionary.SimpleTypes _simpleType;
        /// <summary>
        /// 简单信息
        /// </summary>
        private object _simpleMessage;

        /// <summary>
        /// 简单标识
        /// </summary>
        public SocketDictionary.SimpleTypes SimpleType
        {
            get
            {
                return _simpleType;
            }
            set
            {
                _simpleType = value;
            }
        }
        /// <summary>
        /// 简单信息
        /// </summary>
        public object SimpleMessage
        {
            get
            {
                return _simpleMessage;
            }
            set
            {
                _simpleMessage = value;
            }
        }
    }
}
