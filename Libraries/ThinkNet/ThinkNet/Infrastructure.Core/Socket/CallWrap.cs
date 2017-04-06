using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Socket调用的包装
    /// </summary>
    [Serializable]
    public class CallWrap
    {
        /// <summary>
        /// 调用的类
        /// </summary>
        private string _cmdCls;
        /// <summary>
        /// 调用的方法
        /// </summary>
        private string _cmdName;
        /// <summary>
        /// 参数
        /// </summary>
        private object _params;
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        private string _connectionStringName = "";

        /// <summary>
        /// 调用的类
        /// </summary>
        public string CommandCls
        {
            get
            {
                return _cmdCls;
            }
            set
            {
                _cmdCls = value;
            }
        }
        /// <summary>
        /// 调用的方法
        /// </summary>
        public string CommandName
        {
            get
            {
                return _cmdName;
            }
            set
            {
                _cmdName = value;
            }
        }
        /// <summary>
        /// 参数
        /// </summary>
        public object Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ConnectionStringName
        {
            get { return _connectionStringName; }
            set { _connectionStringName = value; }
        }
    }
}
