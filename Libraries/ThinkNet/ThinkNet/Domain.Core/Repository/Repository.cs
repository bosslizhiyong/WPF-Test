using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 仓储抽象基类
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot>, IRepositoryUnitOfWork
        where TAggregateRoot : AggregateRoot
    {
        #region 字    段
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        protected string _connectionStringName = "";
        /// <summary>
        /// 仓储上下文
        /// </summary>
        private readonly IRepositoryContext context;
        #endregion

        #region 属    性
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ExternalConnectionStringsName
        {
            set { 
                _connectionStringName=value;
                CreateClusteDAO();//创建数据访问对象
            }
        }
        /// <summary>
        /// 仓储上下文
        /// </summary>
        public IRepositoryContext Context
        {
            get { return this.context; }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected Repository()
            : this(null,"")
        {
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="context"></param>
        protected Repository(IRepositoryContext context, string connectionStringName)
        {
            this.context = context;
            this._connectionStringName = connectionStringName;
        }

        #endregion

        #region 基本方法

        #region IRepository<TAggregateRoot>成员
        public void Add(TAggregateRoot aggregateRoot)
        {
            if (this.context != null)
            {
                this.context.RegisterAdd(aggregateRoot, this);
            }
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            if (this.context != null)
            {
                this.context.RegisterUpdate(aggregateRoot, this);
            }
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            if (this.context != null)
            {
                this.context.RegisterDelete(aggregateRoot, this);
            }
        }

        /// <summary>
        /// 执行插入语句
        /// </summary>
        /// <param name="insertSql"></param>
        public void ExecuteInsertSql(string insertSql)
        {
            if (this.context != null)
            {
                this.context.RegisterInsertSql(insertSql, this);
            }
        }
        /// <summary>
        /// 执行更新语句
        /// </summary>
        /// <param name="updateSql"></param>
        public void ExecuteUpdateSql(string updateSql)
        {
            if (this.context != null)
            {
                this.context.RegisterUpdateSql(updateSql, this);
            }
        }
        /// <summary>
        /// 执行删除语句
        /// </summary>
        /// <param name="deleteSql"></param>
        public void ExecuteDeleteSql(string deleteSql)
        {
            if (this.context != null)
            {
                this.context.RegisterDeleteSql(deleteSql, this);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
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

        #region IRepositoryUnitOfWork成员
        public void PersistAdd(AggregateRoot aggregateRoot)
        {
            this.PersistAdd((TAggregateRoot)aggregateRoot);
        }

        public void PersistUpdate(AggregateRoot aggregateRoot)
        {
            this.PersistUpdate((TAggregateRoot)aggregateRoot);
        }

        public void PersistDelete(AggregateRoot aggregateRoot)
        {
            this.PersistDelete((TAggregateRoot)aggregateRoot);
        }

        public abstract void PersistInsertSql(string insertSql);
        public abstract void PersistUpdateSql(string updateSql);
        public abstract void PersistDeleteSql(string deleteSql);

        public abstract void PersistExecuteProcedure(string procName, List<DbParameter> parameters);
        #endregion

        protected abstract void PersistAdd(TAggregateRoot aggregateRoot);
        protected abstract void PersistUpdate(TAggregateRoot aggregateRoot);
        protected abstract void PersistDelete(TAggregateRoot aggregateRoot);

        #endregion

        #region 其他方法
        /// <summary>
        /// 创建集群数据访问对象
        /// </summary>
        protected abstract void CreateClusteDAO();
        #endregion
    }
}
