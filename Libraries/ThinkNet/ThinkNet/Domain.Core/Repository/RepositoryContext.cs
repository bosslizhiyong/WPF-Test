using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ThinkNet.Domain.Core
{
    /// <summary>
    /// 仓储上下文抽象基类
    /// </summary>
    public abstract class RepositoryContext : IRepositoryContext
    {
        #region 字    段

        private readonly Dictionary<AggregateRoot, IRepositoryUnitOfWork> localAddCollection = new Dictionary<AggregateRoot, IRepositoryUnitOfWork>();
        private readonly Dictionary<AggregateRoot, IRepositoryUnitOfWork> localUpdateCollection = new Dictionary<AggregateRoot, IRepositoryUnitOfWork>();
        private readonly Dictionary<AggregateRoot, IRepositoryUnitOfWork> localDeleteCollection = new Dictionary<AggregateRoot, IRepositoryUnitOfWork>();

        private readonly Dictionary<string, IRepositoryUnitOfWork> localInsertSqlCollection = new Dictionary<string, IRepositoryUnitOfWork>();
        private readonly Dictionary<string, IRepositoryUnitOfWork> localUpdateSqlCollection = new Dictionary<string, IRepositoryUnitOfWork>();
        private readonly Dictionary<string, IRepositoryUnitOfWork> localDeleteSqlCollection = new Dictionary<string, IRepositoryUnitOfWork>();
        private readonly Dictionary<Dictionary<string, List<DbParameter>>, IRepositoryUnitOfWork> localProcedureCollection = new Dictionary<Dictionary<string, List<DbParameter>>, IRepositoryUnitOfWork>();

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected RepositoryContext()
        {
        }

        #endregion

        #region 基本方法

        public void RegisterAdd(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localAddCollection.Add(aggregateRoot, repositoryUnitOfWork);
        }

        public void RegisterUpdate(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localUpdateCollection.Add(aggregateRoot, repositoryUnitOfWork);
        }

        public void RegisterDelete(AggregateRoot aggregateRoot, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localDeleteCollection.Add(aggregateRoot, repositoryUnitOfWork);
        }

        /// <summary>
        /// 注册插入SQL语句
        /// </summary>
        /// <param name="insertSql">插入SQL语句</param>
        public void RegisterInsertSql(string insertSql, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localInsertSqlCollection.Add(insertSql, repositoryUnitOfWork);
        }
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="updateSql">更新SQL语句</param>
        public void RegisterUpdateSql(string updateSql, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localUpdateSqlCollection.Add(updateSql, repositoryUnitOfWork);
        }
        /// <summary>
        /// 注册更新SQL语句
        /// </summary>
        /// <param name="deleteSql">删除SQL语句</param>
        public void RegisterDeleteSql(string deleteSql, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localDeleteSqlCollection.Add(deleteSql, repositoryUnitOfWork);
        }

        public void RegisterProcedure(Dictionary<string, List<DbParameter>> dicProcedure, IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            this.localProcedureCollection.Add(dicProcedure, repositoryUnitOfWork);
        }

        public void Commit()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //删除
                    foreach (AggregateRoot aggregateRoot in this.localDeleteCollection.Keys)
                    {
                        this.localDeleteCollection[aggregateRoot].PersistDelete(aggregateRoot);
                    }
                    foreach (string sql in this.localDeleteSqlCollection.Keys)
                    {
                        this.localDeleteSqlCollection[sql].PersistDeleteSql(sql);
                    }
                    //新增
                    foreach (AggregateRoot aggregateRoot in this.localAddCollection.Keys)
                    {
                        this.localAddCollection[aggregateRoot].PersistAdd(aggregateRoot);
                    }
                    foreach (string sql in this.localInsertSqlCollection.Keys)
                    {
                        this.localInsertSqlCollection[sql].PersistInsertSql(sql);
                    }
                    //更新
                    foreach (AggregateRoot aggregateRoot in this.localUpdateCollection.Keys)
                    {
                        this.localUpdateCollection[aggregateRoot].PersistUpdate(aggregateRoot);
                    }
                    foreach (string sql in this.localUpdateSqlCollection.Keys)
                    {
                        this.localUpdateSqlCollection[sql].PersistUpdateSql(sql);
                    }

                    foreach (Dictionary<string, List<DbParameter>> dicProcedure in this.localProcedureCollection.Keys)
                    {
                        foreach (KeyValuePair<string, List<DbParameter>> item in dicProcedure)
                        {
                            this.localProcedureCollection[dicProcedure].PersistExecuteProcedure(item.Key, item.Value);
                        }
                    }

                    scope.Complete();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.localDeleteCollection.Clear();
                this.localAddCollection.Clear();
                this.localUpdateCollection.Clear();
                this.localInsertSqlCollection.Clear();
                this.localUpdateSqlCollection.Clear();
                this.localDeleteSqlCollection.Clear();
                this.localProcedureCollection.Clear();
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
