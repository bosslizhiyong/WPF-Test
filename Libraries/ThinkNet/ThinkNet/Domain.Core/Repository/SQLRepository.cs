using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 基于SQLServer的仓储抽象基类
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public abstract class SQLRepository<TAggregateRoot> : Repository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
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
        protected SQLRepository()
            : this(null,null,null)
        {
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="context"></param>
        protected SQLRepository(IRepositoryContext context, string connectionStringName, IDAOCenter daoCenter)
            : base(context,connectionStringName)
        {
            _daoCenter = daoCenter;
            CreateClusteDAO();//默认创建数据访问对象
        }

        #endregion

        #region 基本方法
        protected abstract override void PersistAdd(TAggregateRoot aggregateRoot);
        protected abstract override void PersistUpdate(TAggregateRoot aggregateRoot);
        protected abstract override void PersistDelete(TAggregateRoot aggregateRoot);

        public override void PersistInsertSql(string insertSql)
        {
            SqlHelper mSqlHelper = new SqlHelper();
            mSqlHelper.ExecuteNonQuery(insertSql, _connectionStringName);
        }
        public override void PersistUpdateSql(string updateSql)
        {
            SqlHelper mSqlHelper = new SqlHelper();
            mSqlHelper.ExecuteNonQuery(updateSql, _connectionStringName);
        }
        public override void PersistDeleteSql(string deleteSql)
        {
            SqlHelper mSqlHelper = new SqlHelper();
            mSqlHelper.ExecuteNonQuery(deleteSql, _connectionStringName);
        }

        public override void PersistExecuteProcedure(string procName, List<DbParameter> parameters)
        {
            SqlHelper mSqlHelper = new SqlHelper();
            mSqlHelper.ExecuteNonQuery(procName, parameters, _connectionStringName);
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
