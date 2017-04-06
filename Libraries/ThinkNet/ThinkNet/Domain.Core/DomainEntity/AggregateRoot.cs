using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 聚合根抽象基类
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        #region 字    段

        private object key;

        #endregion

        #region 属    性

        public object Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// 简单结果描述
        /// </summary>
        public virtual SimpleResult SimpleResult
        {
            get;
            set;
        }

        #endregion

        #region 构造函数
        protected AggregateRoot()
            : this(null)
        {

        }
        protected AggregateRoot(object key)
        {
            this.key = key;
            if (this.key == null)
            {
                this.key = Guid.NewGuid().ToString();
            }
        }

        #endregion

        #region 基本方法

        #endregion

        #region 其他方法

        #endregion
    }
}
