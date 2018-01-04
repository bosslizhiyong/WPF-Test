using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Domain.Core;

namespace ThinkCRM.Domain.Co
{
    public interface ILoginDomainService : IDomainService
    {
        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastLoginTime"></param>
        void UpdateLoginTime(int id, DateTime lastLoginTime);
        /// <summary>
        /// 更新退出时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastLogoutTime"></param>
        void UpdateLogoutTime(int id, DateTime lastLogoutTime);
    }
}
