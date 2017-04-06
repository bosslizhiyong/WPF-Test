using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNet.Component;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Json数据结果描述
    /// </summary>
    [Serializable]
    public class SimpleJsonDate
    {
        #region 字    段

        #endregion

        #region 属    性

        /// <summary>
        /// 操作是否成功
        /// </summary>
        public Boolean r { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object d { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SimpleJsonDate()
        {
            r = false;
            m = String.Empty;
            d =null;
        }

      /// <summary>
      /// 构造函数
      /// </summary>
      /// <param name="r"></param>
      /// <param name="m"></param>
      /// <param name="d"></param>
        public SimpleJsonDate(Boolean r, string m, object d)
        {
            this.r = r;
            this.m = m;
            this.d = d;
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
            args["r"] = r;
            args["m"] = m;
            args["d"] = d;
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
        /// <summary>
        /// 清空
        /// </summary>
        public void ClaerJsonDate()
        {
            r = false;
            m = String.Empty;
            d = null;
        }
        #endregion
    }
}
