using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Command.Core;

namespace ThinkCRM.Commands.Co
{
    public class AddAccountCommand : Command
    {
        /// <summary>
        /// 
        /// </summary>
        public CM_Account MCM_Account { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        public AddAccountCommand(CM_Account dataEntity)
        {
            this.MCM_Account = dataEntity;
        }
    }
}
