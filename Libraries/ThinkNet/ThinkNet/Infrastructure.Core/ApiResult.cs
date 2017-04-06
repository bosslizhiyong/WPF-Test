using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ThinkNet.Component;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Json数据结果描述
    /// </summary>
    [DataContract]
    public class ApiResult<T>
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 错误码
        /// 200：正常 400：错误
        /// </summary>
        [DataMember]
        public int code
        {
            get;
            set;
        }
        /// <summary>
        /// 服务端 设置返回的错误提示，免得每次更新APP
        /// </summary>
        [DataMember]
        public string msg
        {
            get;
            set;
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember]
        public T data { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiResult()
        {
            code = 400;
            msg = "操作失败！";
            data = default(T);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="data">数据</param>
        public ApiResult(int code, T data)
        {
            this.code = code;
            this.data = data;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg"></param>
        /// <param name="data">数据</param>
        public ApiResult(int code, string msg, T data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
        #endregion

        #region 基本方法

        #endregion

        #region 其他方法

        #endregion
    }
}
