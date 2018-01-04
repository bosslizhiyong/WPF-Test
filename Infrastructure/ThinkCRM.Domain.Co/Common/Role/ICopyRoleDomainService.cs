using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Domain.Core;

namespace ThinkCRM.Domain.Co
{
    public interface ICopyRoleDomainService : IDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromRoleId"></param>
        /// <returns></returns>
        bool CopyRole(int fromRoleId);
    }
}
