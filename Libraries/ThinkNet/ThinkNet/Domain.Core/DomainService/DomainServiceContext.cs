using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Domain.Core
{
    public abstract class DomainServiceContext : IDomainServiceContext
    {
        #region 字    段

        private readonly Dictionary<DataEntityBase, IDomainServiceUnitOfWork> localAddCollection = new Dictionary<DataEntityBase, IDomainServiceUnitOfWork>();
        private readonly Dictionary<DataEntityBase, IDomainServiceUnitOfWork> localUpdateCollection = new Dictionary<DataEntityBase, IDomainServiceUnitOfWork>();
        private readonly Dictionary<DataEntityBase, IDomainServiceUnitOfWork> localDeleteCollection = new Dictionary<DataEntityBase, IDomainServiceUnitOfWork>();

        private readonly Dictionary<string, IDomainServiceUnitOfWork> localSqlCollection = new Dictionary<string, IDomainServiceUnitOfWork>();
        private readonly Dictionary<string, IDomainServiceUnitOfWork> localInsertSqlCollection = new Dictionary<string, IDomainServiceUnitOfWork>();
        private readonly Dictionary<string, IDomainServiceUnitOfWork> localUpdateSqlCollection = new Dictionary<string, IDomainServiceUnitOfWork>();
        private readonly Dictionary<string, IDomainServiceUnitOfWork> localDeleteSqlCollection = new Dictionary<string, IDomainServiceUnitOfWork>();
        private readonly Dictionary<Dictionary<string, List<DbParameter>>, IDomainServiceUnitOfWork> localProcedureCollection = new Dictionary<Dictionary<string, List<DbParameter>>, IDomainServiceUnitOfWork>();

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected DomainServiceContext()
        {
        }

        #endregion

        #region 基本方法

        public void RegisterAdd<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase
        {
            this.localAddCollection.Add(dataEntity, serviceUnitOfWork);
        }

        public void RegisterUpdate<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase
        {
            this.localUpdateCollection.Add(dataEntity, serviceUnitOfWork);
        }

        public void RegisterDelete<T>(T dataEntity, IDomainServiceUnitOfWork serviceUnitOfWork) where T : DataEntityBase
        {
            this.localDeleteCollection.Add(dataEntity, serviceUnitOfWork);
        }

        /// <summary>
        /// 注册SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        public void RegisterSql(string strSql, IDomainServiceUnitOfWork serviceUnitOfWork)
        {
            this.localSqlCollection.Add(strSql, serviceUnitOfWork);
        }

        public void RegisterInsertSql(string insertSql, IDomainServiceUnitOfWork serviceUnitOfWork)
        {
            this.localInsertSqlCollection.Add(insertSql, serviceUnitOfWork);
        }

        public void RegisterUpdateSql(string updateSql, IDomainServiceUnitOfWork serviceUnitOfWork)
        {
            this.localUpdateSqlCollection.Add(updateSql, serviceUnitOfWork);
        }

        public void RegisterDeleteSql(string deleteSql, IDomainServiceUnitOfWork serviceUnitOfWork)
        {
            this.localDeleteSqlCollection.Add(deleteSql, serviceUnitOfWork);
        }

        public void RegisterProcedure(Dictionary<string, List<DbParameter>> dicProcedure, IDomainServiceUnitOfWork serviceUnitOfWork)
        {
            this.localProcedureCollection.Add(dicProcedure, serviceUnitOfWork);
        }

        public void Commit()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //删除
                    foreach (DataEntityBase dataEntityBase in this.localDeleteCollection.Keys)
                    {
                        this.localDeleteCollection[dataEntityBase].PersistDelete(dataEntityBase);
                    }
                    foreach (string sql in this.localDeleteSqlCollection.Keys)
                    {
                        this.localDeleteSqlCollection[sql].PersistDeleteSql(sql);
                    }
                    //新增
                    foreach (DataEntityBase dataEntityBase in this.localAddCollection.Keys)
                    {
                        this.localAddCollection[dataEntityBase].PersistAdd(dataEntityBase);
                    }
                    foreach (string sql in this.localInsertSqlCollection.Keys)
                    {
                        this.localInsertSqlCollection[sql].PersistInsertSql(sql);
                    }
                    //更新
                    foreach (DataEntityBase dataEntityBase in this.localUpdateCollection.Keys)
                    {
                        this.localUpdateCollection[dataEntityBase].PersistUpdate(dataEntityBase);
                    }
                    foreach (string sql in this.localUpdateSqlCollection.Keys)
                    {
                        this.localUpdateSqlCollection[sql].PersistUpdateSql(sql);
                    }

                    foreach (string sql in this.localSqlCollection.Keys)
                    {
                        this.localSqlCollection[sql].PersistExecuteNonQuery(sql);
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
                this.localSqlCollection.Clear();
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
