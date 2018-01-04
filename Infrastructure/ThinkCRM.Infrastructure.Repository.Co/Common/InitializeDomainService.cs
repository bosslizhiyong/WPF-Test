using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ThinkCRM.Domain.Co;
using ThinkCRM.Infrastructure.DataEntity.Co;
using ThinkNet.Domain.Core;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Utility;

namespace ThinkCRM.Infrastructure.Repository.Co
{
    public class InitializeDomainService : SQLDomainServiceBase, IInitializeDomainService
    {
        #region 字    段
        private SqlHelper _mSqlHelper = null;
       // private IDAO<CO_Enterprise> _mDaoEnterprise = null;
        private IDAO<CM_User> _mDaoUser = null;
        private IDAO<CM_Module> _mDaoModule = null;
        private IDAO<CM_Functions> _mDaoFunctions = null;
        private IDAO<CM_ModuleFunction_Ref> _mDaoRef = null;
        #endregion

        #region 属    性

        #endregion

        #region 构造函数
        public InitializeDomainService()
            : this(null,null)
        {
        }

        public InitializeDomainService(IDomainServiceContext context,IDAOCenter daoCenter)
            : base(context,daoCenter)
        {
            
        }
        #endregion

        #region 基本方法
        public void Initialize(//CO_Enterprise mCO_Enterprise, 
            CM_User mCM_User, 
            List<CM_Module> listCM_Module, List<CM_Functions> listCM_Functions, List<CM_ModuleFunction_Ref> listCM_ModuleFunction_Ref, 
            List<string> listInsertSql)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                //    _mDaoEnterprise.Add(mCO_Enterprise);
                    _mDaoUser.Add(mCM_User);
                    List<string> listUpdateSql = new List<string>();
                    int oldId = 0;
                    int newId = 0;
                    Dictionary<int, int> dicModuleId = new Dictionary<int, int>();
                    foreach (CM_Module item in listCM_Module)
                    {
                        oldId = item.ID;
                        newId=_mDaoModule.Add(item);
                        listUpdateSql.Add(string.Format("UPDATE CM_Module SET ParentID={1} WHERE ParentID={0}", oldId, newId));
                        if (!dicModuleId.ContainsKey(oldId))
                        {
                            dicModuleId.Add(oldId, newId);
                        }
                    }
                    Dictionary<int, int> dicFunctionId = new Dictionary<int, int>();
                    foreach (CM_Functions item in listCM_Functions)
                    {
                        oldId = item.ID;
                        newId=_mDaoFunctions.Add(item);
                        if (!dicFunctionId.ContainsKey(oldId))
                        {
                            dicFunctionId.Add(oldId, newId);
                        }
                    }
                    foreach (CM_ModuleFunction_Ref item in listCM_ModuleFunction_Ref)
                    {
                        if (dicModuleId.ContainsKey(item.ModuleID))
                        {
                            item.ModuleID = dicModuleId[item.ModuleID];
                        }
                        if (dicFunctionId.ContainsKey(item.FunctionID))
                        {
                            item.FunctionID = dicFunctionId[item.FunctionID];
                        }
                        _mDaoRef.Add(item);
                    }
                    foreach (string sql in listInsertSql)
                    {
                        _mSqlHelper.ExecuteNonQuery(sql);
                    }
                    foreach (string sql in listUpdateSql)
                    {
                        _mSqlHelper.ExecuteNonQuery(sql);
                    }

                    scope.Complete();
                }
            }
            catch (Exception)
            {
            }
        }

        //public void ResetInitializeEnterprise(CO_Enterprise mCO_Enterprise, CM_User mCM_User)
        //{
        //    string strSql = string.Format("SELECT ID FROM CO_Enterprise WHERE TheID={0}",mCO_Enterprise.TheID);//当前的ID号
        //    object obj = _mSqlHelper.ExecuteScalar(strSql);
        //    int id = 0;
        //    if(obj!=null)
        //    {
        //        id = Convert.ToInt32(obj);
        //        mCO_Enterprise.ID = id;
        //    }
        //    strSql = string.Format("SELECT ID FROM CM_User WHERE TheID={0}",mCM_User.TheID);//当前的ID号
        //    obj = _mSqlHelper.ExecuteScalar(strSql);
        //    if (obj != null)
        //    {
        //        id = Convert.ToInt32(obj);
        //        mCM_User.ID = id;
        //    }
        //    base.Update<CO_Enterprise>(mCO_Enterprise);
        //    base.Update<CM_User>(mCM_User);
        //    base.Context.Commit();
        //}

        public void ResetInitializeModule(List<CM_Module> listCM_Module, List<CM_Functions> listCM_Functions, List<CM_ModuleFunction_Ref> listCM_ModuleFunction_Ref)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string strSql = "";
                    object obj = null;
                    List<string> listUpdateSql = new List<string>();
                    int oldId = 0;
                    int newId = 0;

                    #region 模块
                    Dictionary<int, int> dicModuleId = new Dictionary<int, int>();
                    foreach (CM_Module item in listCM_Module)
                    {
                        strSql = string.Format("SELECT ID FROM CM_Module WHERE TheID={0}", item.TheID);//当前的ID号
                        obj = _mSqlHelper.ExecuteScalar(strSql);
                        if (obj == null)//不存在
                        {
                            oldId = item.ID;
                            newId = _mDaoModule.Add(item);
                            strSql = string.Format("UPDATE CM_Module SET ParentID=ISNULL((SELECT ID FROM CM_Module WHERE TheID={0}),0) WHERE TheID={1}", item.ParentID, item.ID);
                            listUpdateSql.Add(strSql);
                            if (!dicModuleId.ContainsKey(oldId))
                            {
                                dicModuleId.Add(oldId, newId);
                            }
                        }
                        else
                        {
                            strSql = string.Format(@"UPDATE CM_Module SET ModuleCode=N'{1}',ModuleName=N'{2}',ModuleURL=N'{3}',ModuleForm=N'{4}',IsMDIForm={9},ModuleLevel={5},ModuleType={6},Parameter=N'{7}',Sequence={8} WHERE TheID={0}",
                                item.TheID, item.ModuleCode, item.ModuleName, item.ModuleURL, item.ModuleForm, item.ModuleLevel, item.ModuleType, item.Parameter, item.Sequence,item.IsMDIForm?1:0);
                            listUpdateSql.Add(strSql);
                            if (!dicModuleId.ContainsKey(item.TheID))
                            {
                                dicModuleId.Add(item.TheID, DataTypeConvert.ToInt32(obj));
                            }
                        }
                    }
                    #endregion

                    #region 功能
                    Dictionary<int, int> dicFunctionId = new Dictionary<int, int>();
                    foreach (CM_Functions item in listCM_Functions)
                    {
                        strSql = string.Format("SELECT ID FROM CM_Functions WHERE TheID={0}", item.TheID);//当前的ID号
                        obj = _mSqlHelper.ExecuteScalar(strSql);
                        if (obj == null)//不存在
                        {
                            oldId = item.ID;
                            newId = _mDaoFunctions.Add(item);
                            if (!dicFunctionId.ContainsKey(oldId))
                            {
                                dicFunctionId.Add(oldId, newId);
                            }
                        }
                        else
                        {
                            strSql = string.Format(@"UPDATE CM_Functions SET FunctionCode=N'{1}',FunctionName=N'{2}',FunctionDesc=N'{3}',FunctionButton=N'{4}',Sequence={5} WHERE TheID={0}",
                                item.TheID, item.FunctionCode, item.FunctionName, item.FunctionDesc, item.FunctionButton, item.Sequence);
                            listUpdateSql.Add(strSql);
                            if (!dicFunctionId.ContainsKey(item.TheID))
                            {
                                dicFunctionId.Add(item.TheID, DataTypeConvert.ToInt32(obj));
                            }
                        }
                    }
                    #endregion

                    #region 模块功能关系
                    foreach (CM_ModuleFunction_Ref item in listCM_ModuleFunction_Ref)
                    {
                        strSql = string.Format("SELECT ID FROM CM_ModuleFunction_Ref WHERE ModuleID=(SELECT ID FROM CM_Module WHERE TheID={0}) AND FunctionID=(SELECT ID FROM CM_Functions WHERE TheID={1})", item.ModuleID, item.FunctionID);//当前的ID号
                        obj = _mSqlHelper.ExecuteScalar(strSql);
                        if (obj == null)//不存在
                        {
                            if (dicModuleId.ContainsKey(item.ModuleID))
                            {
                                item.ModuleID = dicModuleId[item.ModuleID];
                            }
                            if (dicFunctionId.ContainsKey(item.FunctionID))
                            {
                                item.FunctionID = dicFunctionId[item.FunctionID];
                            }
                            _mDaoRef.Add(item);
                        }
                    }
                    #endregion

                    foreach (string sql in listUpdateSql)
                    {
                        _mSqlHelper.ExecuteNonQuery(sql);
                    }

                    scope.Complete();
                }
            }
            catch(Exception)
            {

            }
        }
        #endregion

        #region 其他方法
        protected override void CreateClusteDAO()
        {
            _mSqlHelper = new SqlHelper(string.IsNullOrEmpty(_connectionStringName) ? ConnectionStrings.ConnCRMCo.ToString() : _connectionStringName);
           // _mDaoEnterprise = CreateDAO<CO_Enterprise>();
            _mDaoUser = CreateDAO<CM_User>();
            _mDaoModule = CreateDAO<CM_Module>();
            _mDaoFunctions = CreateDAO<CM_Functions>();
            _mDaoRef = CreateDAO<CM_ModuleFunction_Ref>();
        }
        #endregion
    }
}
