// 作者：李志勇
// 日期：2017-04-14 15:20:52
using System;
using ThinkNet.Persistence.Core;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Command.Core;

namespace ThinkCRM.Commands.Co
{
    /// <summary>
    /// CM_Account 数据库操作类
    /// </summary>
    public class CM_AccountCommand : Command
    {

        #region 属    性
        public CM_Account MCM_Account { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// CM_Account 数据库操作类
        /// </summary>
        public CM_AccountCommand(CM_Account dataEntity)
        {
            this.MCM_Account = dataEntity;
        }
        #endregion

    }
}