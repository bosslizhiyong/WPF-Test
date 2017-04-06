using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Socket调用的结果(查询包装)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class EntityWrap<T> : CallResult
    {
        private T obj;
        public T EntityWrapObj 
        {
            get { return obj; }
            set { obj = value; }
        }
    }
}
