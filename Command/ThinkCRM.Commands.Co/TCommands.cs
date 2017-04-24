
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNet.Command.Core;

namespace ThinkCRM.Commands.Co
{
    public class TCommands<T> : Command
    {

        #region 属    性
        public T dataEntity { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// CM_Account 数据库操作类
        /// </summary>
        public TCommands(T dataEntity)
        {
            this.dataEntity = dataEntity;
        }
        #endregion

    }
}
