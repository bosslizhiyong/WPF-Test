using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.DataEntity.Core;
using ThinkNet.Domain.Core;

namespace ThinkCRM.Domain.Co.Common.Role
{
    public class CopyRole : AggregateRoot
    {
        /// <summary>
        /// 角色
        /// </summary>
        public CM_Roles MCM_Roles { get; private set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<CM_RoleModule_Ref> LisCM_RoleModule_Ref { get; private set; }
        public List<CM_RoleModule_Ref> LisCM_RoleModule_RefAdd { get; private set; }
        public List<CM_RoleModule_Ref> LisCM_RoleModule_RefUpdate { get; private set; }
        public List<CM_RoleModule_Ref> LisCM_RoleModule_RefDelete { get; private set; }
        /// <summary>
        /// 权限
        /// </summary>
        public List<CM_RoleModuleFunction_Ref> LisCM_RoleModuleFunction_Ref { get; private set; }
        public List<CM_RoleModuleFunction_Ref> LisCM_RoleModuleFunction_RefAdd { get; private set; }
        public List<CM_RoleModuleFunction_Ref> LisCM_RoleModuleFunction_RefUpdate { get; private set; }
        public List<CM_RoleModuleFunction_Ref> LisCM_RoleModuleFunction_RefDelete { get; private set; }

        public CopyRole(CM_Roles MCM_Roles,List<CM_RoleModule_Ref> listCM_RoleModule_Ref, List<CM_RoleModuleFunction_Ref> LisCM_RoleModuleFunction_Ref)
        {
            this.MCM_Roles = MCM_Roles;
            this.LisCM_RoleModule_RefAdd = listCM_RoleModule_Ref;
            this.LisCM_RoleModuleFunction_Ref = LisCM_RoleModuleFunction_Ref;
            //为了提高效率占时注释
            //this.LisCM_RoleModuleFunction_Ref = LisCM_RoleModuleFunction_Ref;
            //this.LisCM_RoleModule_Ref = listCM_RoleModule_Ref;
            //SpliCM_RoleModuleFunctionRef();
            //SpliCM_RoleModule_Ref();
        }
        public void SpliCM_RoleModuleFunctionRef()
        {
            LisCM_RoleModuleFunction_RefAdd = new List<CM_RoleModuleFunction_Ref>();
            LisCM_RoleModuleFunction_RefUpdate = new List<CM_RoleModuleFunction_Ref>();
            LisCM_RoleModuleFunction_RefDelete = new List<CM_RoleModuleFunction_Ref>();
            if (this.LisCM_RoleModuleFunction_Ref != null && this.LisCM_RoleModuleFunction_Ref.Count > 0)
            {
                foreach (CM_RoleModuleFunction_Ref item in this.LisCM_RoleModuleFunction_Ref)
                {
                    switch (item.DataEntityAction)
                    {
                        case DataEntityActions.Add:
                            LisCM_RoleModuleFunction_RefAdd.Add(item);
                            break;
                        case DataEntityActions.Update:
                            LisCM_RoleModuleFunction_RefUpdate.Add(item);
                            break;
                        case DataEntityActions.Delete:
                            LisCM_RoleModuleFunction_RefDelete.Add(item);
                            break;
                        default:
                             LisCM_RoleModuleFunction_RefAdd.Add(item);
                            break;
                    }
                }
            }
        }
        public void SpliCM_RoleModule_Ref()
        {
            LisCM_RoleModule_RefAdd = new List<CM_RoleModule_Ref>();
            LisCM_RoleModule_RefUpdate = new List<CM_RoleModule_Ref>();
            LisCM_RoleModule_RefDelete = new List<CM_RoleModule_Ref>();
            if (this.LisCM_RoleModuleFunction_Ref != null && this.LisCM_RoleModuleFunction_Ref.Count > 0)
            {
                foreach (CM_RoleModule_Ref item in this.LisCM_RoleModule_Ref)
                {
                    switch (item.DataEntityAction)
                    {
                        case DataEntityActions.Add:
                            LisCM_RoleModule_RefAdd.Add(item);
                            break;
                        case DataEntityActions.Update:
                            LisCM_RoleModule_RefUpdate.Add(item);
                            break;
                        case DataEntityActions.Delete:
                            LisCM_RoleModule_RefDelete.Add(item);
                            break;
                        default:
                            LisCM_RoleModule_RefAdd.Add(item);
                            break;
                    }
                }
            }
        }
    }
}
