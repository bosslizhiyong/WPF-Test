using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkCRM.Domain.Co;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Domain.Core;
using ThinkNet.Persistence.Core;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginDomainService : SQLDomainServiceBase, ILoginDomainService
    {
        #region 字    段
        private IDAO<CM_User> _mDAO = null;
        #endregion

        #region 属    性

        #endregion

        #region 构造函数
        public LoginDomainService()
            : this(null,null)
        {
        }

        public LoginDomainService(IDomainServiceContext context, IDAOCenter daoCenter)
            : base(context,daoCenter)
        {
            
        }
        #endregion

        #region 基本方法
        public void UpdateLoginTime(int id, DateTime lastLoginTime)
        {
            CM_User mCM_User = new CM_User();
            mCM_User.ID = id;
            mCM_User.LastLoginTime = lastLoginTime;
            _mDAO.Update(mCM_User, "ID,LastLoginTime");
        }

        public void UpdateLogoutTime(int id, DateTime lastLogoutTime)
        {
            CM_User mCM_User = new CM_User();
            mCM_User.ID = id;
            mCM_User.LastLogoutTime = lastLogoutTime;
            _mDAO.Update(mCM_User, "ID,LastLoginTime");
        }
        #endregion

        #region 其他方法
        protected override void CreateClusteDAO()
        {
            _mDAO = CreateDAO<CM_User>();
        }
        #endregion
    }
}
