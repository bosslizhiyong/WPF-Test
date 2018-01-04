using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Domain.Co;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Domain.Core;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Utility;
using System.Transactions;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    public class CopyRoleDomainService: SQLDomainServiceBase, ICopyRoleDomainService
    {
        #region 字    段
        private SqlHelper _mSqlHelper = null;
        #endregion

        #region 属    性
        
        #endregion

        #region 构造函数
        public CopyRoleDomainService()
            : this(null,null)
        {
        }

        public CopyRoleDomainService(IDomainServiceContext context, IDAOCenter daoCenter)
            : base(context,daoCenter)
        {
            
        }
        #endregion

        #region 基本方法
        public bool CopyRole(int fromRoleId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //新增复制角色
                    string sql = string.Format(@"INSERT INTO CM_Roles(RoleName, RoleDesc, ApplicationID, Sequence, Status, CreateUser, CreateTime, LastModifyUser, LastModifyTime) 
                     SELECT RoleName+'-副本' AS RoleName, RoleDesc, ApplicationID, (SELECT MAX(Sequence)+1 FROM CM_Roles), Status, CreateUser, CreateTime, LastModifyUser, LastModifyTime 
                     FROM CM_Roles WHERE ID={0}
                     select @@identity ", fromRoleId);
                    int toRoleId = DataTypeConvert.ToInt32(_mSqlHelper.ExecuteScalar(sql));
                    //新增复制权限和功能模块关系
                    sql = string.Format(@"INSERT INTO CM_RoleModule_Ref(RoleID,ModuleID) SELECT {0},ModuleID FROM CM_RoleModule_Ref WHERE RoleID={1};
                    INSERT INTO CM_RoleModuleFunction_Ref (RoleID,ModuleID,FunctionID) SELECT {0},ModuleID,FunctionID FROM CM_RoleModuleFunction_Ref WHERE RoleID={1}", toRoleId,fromRoleId);
                    _mSqlHelper.ExecuteNonQuery(sql);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 其他方法
        protected override void CreateClusteDAO()
        {
            _mSqlHelper = new SqlHelper(string.IsNullOrEmpty(_connectionStringName) ? ConnectionStrings.ConnCRMCo.ToString() : _connectionStringName);
        }
        #endregion
    }
}
