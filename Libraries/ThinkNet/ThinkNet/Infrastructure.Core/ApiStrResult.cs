using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ThinkNet.Component;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Json字符串数据结果描述
    /// </summary>
    [DataContract]
    public class ApiStrResult
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 错误码1
        /// 200：正常 400：错误
        /// </summary>
        [DataMember]
        public int code
        {
            get;
            set;
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember]
        public object data { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiStrResult()
        {
            code = 400;
            data = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="data">数据</param>
        public ApiStrResult(int code, object data)
        {
            this.code = code;
            this.data = data;
        }
        #endregion

        #region 基本方法

        #endregion

        #region 其他方法

        #endregion
    }
}
