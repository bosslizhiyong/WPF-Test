using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNet.Component;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 服务结果描述
    /// </summary>
    [Serializable]
    public class ServiceResult
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public Boolean Result { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceResult()
        {
            Result = false;
            Message = String.Empty;
            Data =null;
        }

      /// <summary>
      /// 构造函数
      /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ServiceResult(Boolean result, string message, object data)
        {
            Result = result;
            Message = message;
            Data = data;
        }
        #endregion

        #region 基本方法

        /// <summary>
        /// Json字符串
        /// </summary>
        /// <param name="elseArgs">其它需要一同生成Json的键值对</param>
        /// <returns></returns>
        public string ToString(Dictionary<string, object> elseArgs)
        {
            Dictionary<string, object> args = elseArgs != null ? new Dictionary<string, object>(elseArgs) : new Dictionary<string, object>();
            args["Result"] = this.Result;
            args["Message"] = this.Message;
            args["Data"] = this.Data;
            return ObjectContainer.Resolve<IJsonSerializer>().Serialize(args);
        }

        /// <summary>
        /// Json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(null);
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
