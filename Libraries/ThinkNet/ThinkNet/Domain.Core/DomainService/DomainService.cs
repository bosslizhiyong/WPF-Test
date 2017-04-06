using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 领域服务抽象基类
    /// </summary>
    public abstract class DomainService : IDomainService, IDomainServiceUnitOfWork
    {
        #region 字    段
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        protected string _connectionStringName = "";
        /// <summary>
        /// 领域服务上下文
        /// </summary>
        private readonly IDomainServiceContext context;
        #endregion

        #region 属    性
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ExternalConnectionStringsName
        {
            set { 
                _connectionStringName = value;
                CreateClusteDAO();//创建数据访问对象
            }
        }
        /// <summary>
        /// 领域服务上下文
        /// </summary>
        public IDomainServiceContext Context
        {
            get { return this.context; }
        }
        /// <summary>
        /// 简单结果描述
        /// </summary>
        public SimpleResult SimpleResult
        {
            get;
            set;
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected DomainService()
            : this(null,"")
        {
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="context"></param>
        protected DomainService(IDomainServiceContext context, string connectionStringName)
        {
            this.context = context;
            this._connectionStringName = connectionStringName;
        }

        #endregion

        #region 基本方法

        #region IDomainService成员
        public void Add<T>(T dataEntity) where T : DataEntityBase
        {
            if (this.context != null)
            {
                this.context.RegisterAdd(dataEntity, this);
            }
        }

        public void Update<T>(T dataEntity) where T : DataEntityBase
        {
            if (this.context != null)
            {
                this.context.RegisterUpdate(dataEntity, this);
            }
        }

        public void Delete<T>(T dataEntity) where T : DataEntityBase
        {
            if (this.context != null)
            {
                this.context.RegisterDelete(dataEntity, this);
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        public void ExecuteNonQuery(string strSql)
        {
            if (this.context != null)
            {
                this.context.RegisterSql(strSql, this);
            }
        }

        public void ExecuteInsertSql(string insertSql)
        {
            if (this.context != null)
            {
                this.context.RegisterInsertSql(insertSql, this);
            }
        }

        public void ExecuteUpdateSql(string updateSql)
        {
            if (this.context != null)
            {
                this.context.RegisterUpdateSql(updateSql, this);
            }
        }

        public void ExecuteDeleteSql(string deleteSql)
        {
            if (this.context != null)
            {
                this.context.RegisterDeleteSql(deleteSql, this);
            }
        }

        public virtual SqlParameter AddParameter(string parameterName, System.Data.SqlDbType dbType, int size, object value, System.Data.ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, dbType);
            param.Direction = direction;
            param.Value = value;
            return param;
        }

        public virtual SqlParameter AddInParameter(string parameterName, System.Data.SqlDbType dbType, int size, object value)
        {
            return AddParameter(parameterName, dbType, size, value, ParameterDirection.Input);
        }

        public virtual SqlParameter AddOutParameter(string parameterName, System.Data.SqlDbType dbType, int size)
        {
            return AddParameter(parameterName, dbType, size, null, ParameterDirection.Output);
        }

        public void ExecuteProcedure(string procName, List<DbParameter> parameters)
        {
            Dictionary<string, List<DbParameter>> dicProcedure = new Dictionary<string, List<DbParameter>>();
            dicProcedure.Add(procName, parameters);
            if (this.context != null)
            {
                this.context.RegisterProcedure(dicProcedure, this);
            }
        }
        #endregion

        #region IDomainServiceUnitOfWork成员
        public abstract void PersistAdd<T>(T dataEntity) where T:DataEntityBase;
        public abstract void PersistUpdate<T>(T dataEntity) where T : DataEntityBase;
        public abstract void PersistDelete<T>(T dataEntity) where T : DataEntityBase;

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        public abstract void PersistExecuteNonQuery(string strSql);
        public abstract void PersistInsertSql(string insertSql);
        public abstract void PersistUpdateSql(string updateSql);
        public abstract void PersistDeleteSql(string deleteSql);

        public abstract void PersistExecuteProcedure(string procName, List<DbParameter> parameters);
        #endregion

        #endregion

        #region 其他方法
        /// <summary>
        /// 创建集群数据访问对象
        /// </summary>
        protected abstract void CreateClusteDAO();
        #endregion
    }
}
