using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 基于SQLServer的服务抽象基类
    /// </summary>
    public abstract class SQLDomainService : DomainService
    {
        #region 字    段

        protected IDAOCenter _daoCenter = null;

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected SQLDomainService()
            : this(null, null,null)
        {
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="context"></param>
        protected SQLDomainService(IDomainServiceContext context, string connectionStringName, IDAOCenter daoCenter)
            : base(context,connectionStringName)
        {
            this._daoCenter = daoCenter;
            CreateClusteDAO();//默认创建数据访问对象
        }

        #endregion

        #region 基本方法
        public override void PersistAdd<T>(T dataEntity)
        {
            IDAO<T> dao = CreateDAO<T>();
            //兼容外部设置数据库
            dataEntity.IsExternalConnection = dao.IsExternalConnection;
            dataEntity.ExternalConnectionStringsName = dao.ExternalConnectionStringsName;
            int id=dao.Add<T>(dataEntity);
            SimpleResult = new SimpleResult();
            SimpleResult.Result = true;
            SimpleResult.Message = id+"";
        }
        public override void PersistUpdate<T>(T dataEntity)
        {
            IDAO<T> dao = CreateDAO<T>();
            //兼容外部设置数据库
            dataEntity.IsExternalConnection = dao.IsExternalConnection;
            dataEntity.ExternalConnectionStringsName = dao.ExternalConnectionStringsName;
            dao.Update<T>(dataEntity);
        }
        public override void PersistDelete<T>(T dataEntity)
        {
            IDAO<T> dao = CreateDAO<T>();
            //兼容外部设置数据库
            dataEntity.IsExternalConnection = dao.IsExternalConnection;
            dataEntity.ExternalConnectionStringsName = dao.ExternalConnectionStringsName;
            dao.Delete<T>(dataEntity);
        }

        public override void PersistExecuteNonQuery(string strSql)
        {
            new SqlHelper(_connectionStringName).ExecuteNonQuery(strSql);
        }

        public override void PersistInsertSql(string insertSql)
        {
            new SqlHelper(_connectionStringName).ExecuteNonQuery(insertSql);
        }
        public override void PersistUpdateSql(string updateSql)
        {
            new SqlHelper(_connectionStringName).ExecuteNonQuery(updateSql);
        }
        public override void PersistDeleteSql(string deleteSql)
        {
            new SqlHelper(_connectionStringName).ExecuteNonQuery(deleteSql);
        }

        public override void PersistExecuteProcedure(string procName, List<DbParameter> parameters)
        {
            new SqlHelper(_connectionStringName).ExecuteNonQuery(procName, parameters);
        }
        #endregion

        #region 其他方法
        /// <summary>
        /// 创建数据访问对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual IDAO<T> CreateDAO<T>() where T : DataEntityBase
        {
            IDAO<T> dao = _daoCenter.Create<T>(_connectionStringName);
            return dao;
        }
        /// <summary>
        /// 创建集群数据访问对象
        /// </summary>
        protected abstract override void CreateClusteDAO();
        #endregion
    }
}
