using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Domain.Core;

namespace ThinkCRM.Domain.Co
{
    public interface IInitializeDomainService : IDomainService
    {
        //void Initialize(CO_Enterprise mCO_Enterprise, CM_User mCM_User, List<CM_Module> listCM_Module, List<CM_Functions> listCM_Functions, List<CM_ModuleFunction_Ref> listCM_ModuleFunction_Ref,List<string> listInsertSql);
        //void ResetInitializeEnterprise(CO_Enterprise mCO_Enterprise, CM_User mCM_User);
        void ResetInitializeModule(List<CM_Module> listCM_Module, List<CM_Functions> listCM_Functions, List<CM_ModuleFunction_Ref> listCM_ModuleFunction_Ref);
    }
}
