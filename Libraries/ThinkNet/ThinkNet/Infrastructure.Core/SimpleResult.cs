using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 简单结果描述
    /// </summary>
    [Serializable]
    [DataContract]
    public class SimpleResult
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 操作是否成功
        /// </summary>
        [DataMember]
        public Boolean Result { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SimpleResult()
        {
            Result = false;
            Message = String.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public SimpleResult(Boolean result, string message)
        {
            Result = result;
            Message = message;
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
            args["Result"] = Result;
            args["Message"] = Message;
            //return JSonHelper.Serialize(args);
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
