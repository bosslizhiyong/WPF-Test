using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;
using ThinkNet.Persistence.Core;
using ThinkNet.Utility;

namespace ThinkNet.Query.Core
{
    public abstract class SQLDynamicQueryBase : IDynamicQuery
    {
        protected string _connectionStringName = "";
        protected IDAOCenter _daoCenter = null;

        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ExternalConnectionStringsName
        {
            set
            {
                _connectionStringName=value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <param name="daoCenter"></param>
        public SQLDynamicQueryBase(string connectionStringName, IDAOCenter daoCenter)
        {
            this._connectionStringName = connectionStringName;
            this._daoCenter = daoCenter;
        }

        #region IDynamicQuery 成员
        /// <summary>
        /// 获取一行数据，面向表单
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        public virtual T GetSingleObject<T>(QueryParamater queryParamater) where T:DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(queryParamater.Where, 1, int.MaxValue, queryParamater.OrderType, queryParamater.Columns, queryParamater.OrderField, queryParamater.TableName);
            List<T> collections = dao.GetList(mPager);
            if (collections != null && collections.Count > 0)
            {
                return collections[0];
            }
            return default(T);
        }
        /// <summary>
        /// 获取分页列表，面向支持分页的表格
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public virtual PageResult<T> GetPagerList<T>(QueryParamater queryParamater, int pageIndex, int pageSize) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(queryParamater.Where, pageIndex, pageSize, queryParamater.OrderType, queryParamater.Columns, queryParamater.OrderField, queryParamater.TableName);
            List<T> collections = dao.GetList(mPager);
            int totalRecord = dao.GetCount(queryParamater.Where);
            PageResult<T> mPageResult = new PageResult<T>(pageIndex, pageSize, totalRecord, collections);
            return mPageResult;
        }
        /// <summary>
        /// 获取数据列表，面向不支持分页的表格或下拉框
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList<T>(QueryParamater queryParamater)where T:DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(queryParamater.Where, 1, int.MaxValue, queryParamater.OrderType, queryParamater.Columns, queryParamater.OrderField, queryParamater.TableName);
            List<T> collections = dao.GetList(mPager);
            return collections;
        }
        /// <summary>
        /// 获取分页集合，面向支持分页的表格
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public virtual PageResult<DataRow> GetPagerTable<T>(QueryParamater queryParamater, int pageIndex, int pageSize)where T:DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(queryParamater.Where, pageIndex, pageSize, queryParamater.OrderType, queryParamater.Columns, queryParamater.OrderField, queryParamater.TableName);
            DataTable collections = dao.GetDataTable(mPager);
            int totalRecord = dao.GetCount(queryParamater.Where);
            PageResult<DataRow> mPageResult = new PageResult<DataRow>(pageIndex, pageSize, totalRecord, collections.AsEnumerable());
            return mPageResult;
        }
        /// <summary>
        /// 获取数据集合，面向不支持分页的表格或下拉框
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        public virtual IEnumerable<DataRow> GetTable<T>(QueryParamater queryParamater) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(queryParamater.Where, 1, int.MaxValue, queryParamater.OrderType, queryParamater.Columns, queryParamater.OrderField, queryParamater.TableName);
            DataTable collections = dao.GetDataTable(mPager);
            return collections.AsEnumerable();
        }
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        public virtual int GetCount<T>(QueryParamater queryParamater) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            return dao.GetCount(queryParamater.Where);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryParamater">查询参数</param>
        /// <returns></returns>
        public virtual bool Exists<T>(QueryParamater queryParamater) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            return dao.Exists(queryParamater.Where);
        }
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <returns></returns>
        public virtual int GetNextSequence<T>() where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager("1=1", 1, int.MaxValue, true, "*", "Sequence");
            List<T> collections = dao.GetList(mPager);
            if (collections == null || collections.Count <= 0) return 1;
            return DataTypeConvert.ToInt32(ReflectHelper.GetValue(collections[0], "Sequence")) + 1;
        }
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        public virtual int GetNextSequence<T>(int parentId) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(string.Format("1=1 AND ParentID={0}", parentId), 1, int.MaxValue, true, "*", "Sequence");
            List<T> collections = dao.GetList(mPager);
            if (collections == null || collections.Count <= 0) return 1;
            return DataTypeConvert.ToInt32(ReflectHelper.GetValue(collections[0], "Sequence")) + 1;
        }
        /// <summary>
        /// 获取下一个序号
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public virtual int GetNextSequence<T>(string where) where T : DataEntityBase
        {
            IDAO<T> dao = CreateDAO<T>();

            Pager mPager = new Pager(where, 1, int.MaxValue, true, "*", "Sequence");
            List<T> collections = dao.GetList(mPager);
            if (collections == null || collections.Count <= 0) return 1;
            return DataTypeConvert.ToInt32(ReflectHelper.GetValue(collections[0], "Sequence")) + 1;
        }
        #endregion

        #region ICommonQuery 成员
        /// <summary>
        /// 获取分页集合，面向支持分页的表格
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        public virtual PageResult<DataRow> GetPagerTable(string strSql, int pageIndex, int pageSize, bool orderType, string orderField)
        {
            Pager mPager = new Pager();
            string strPagerSql = mPager.GetPagerSql(strSql, pageIndex, pageSize, orderType, orderField);
            string strCountSql = mPager.GetCountSql(strSql);
            DataTable collections = ExecuteDataTable(strPagerSql);
            int totalRecord = DataTypeConvert.ToInt32(ExecuteScalar(strCountSql));
            PageResult<DataRow> mPageResult = new PageResult<DataRow>(pageIndex, pageSize, totalRecord, collections.AsEnumerable());
            return mPageResult;
        }

        /// <summary>
        /// 获取分页的SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        public virtual string GetPagerSql(string strSql, int pageIndex, int pageSize, bool orderType, string orderField)
        {
            Pager mPager = new Pager();
            string strPagerSql = mPager.GetPagerSql(strSql, pageIndex, pageSize, orderType, orderField);
            return strPagerSql;
        }

        /// <summary>
        /// 获取单个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <returns></returns>
        public virtual string GetSingleSequenceNumber(string sequenceType)
        {
            List<string> list = GetMultiSequenceNumber(sequenceType, 1);
            return list[0];
        }
        /// <summary>
        /// 获取单个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <param name="tableName">表名称</param>
        /// <param name="fieldName">编号字段名称</param>
        /// <param name="where">其它条件</param>
        /// <returns></returns>
        public virtual string GetNotrepeatSequenceNumber(string sequenceType, string tableName, string fieldName, string where)
        {
            List<string> list = GetMultiSequenceNumber(sequenceType, 1);
            string code = list[0];
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(fieldName))
            {
                string strSql = string.Format(" SELECT COUNT(*) FROM {0} WHERE {1}=N'{2}' ", tableName, fieldName, code);
                if (!string.IsNullOrEmpty(where))
                {
                    where = where.Trim();
                    if (where.ToLower().StartsWith("and"))
                    {
                        strSql += " " + where;
                    }
                    else
                    {
                        strSql += " AND " + where;
                    }
                }
                object obj = new SqlHelper().ExecuteScalar(strSql, _connectionStringName);
                if (DataTypeConvert.ToInt32(obj, 0) > 0)//已经存在，继续获取，直到不存在为止
                {
                    return GetNotrepeatSequenceNumber(sequenceType, tableName, fieldName, where);
                }
            }
            return code;
        }
        /// <summary>
        /// 获取多个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <param name="quantity">数量</param>
        /// <returns></returns>
        public virtual List<string> GetMultiSequenceNumber(string sequenceType, int quantity)
        {
            string maxSequenceNumber = string.Empty;
            byte length = 0;
            byte suffixLength = 0;

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(this.AddInParameter("@Code", SqlDbType.NVarChar, 20, sequenceType));
            parameters.Add(this.AddInParameter("@Count", SqlDbType.Int, 0, quantity));
            DataTable dt = new SqlHelper().ExecuteDataTable("proc_GetSequenceNumber", parameters, _connectionStringName);

            if (dt != null && dt.Rows.Count > 0)
            {
                maxSequenceNumber = Convert.ToString(dt.Rows[0][0]);
                length = Convert.ToByte(dt.Rows[0][1]);
                suffixLength = Convert.ToByte(dt.Rows[0][2]);
            }
            else
            {
                return new List<string>() { maxSequenceNumber };
            }

            if (quantity == 1)
            {
                return new List<string>() { maxSequenceNumber };
            }
            else
            {
                string prefix = maxSequenceNumber.Substring(0, maxSequenceNumber.Length - length - suffixLength);
                int index = Convert.ToInt32(maxSequenceNumber.Substring(prefix.Length, length));
                string suffix = maxSequenceNumber.Substring(maxSequenceNumber.Length - suffixLength);

                string format = "0000000000".Substring(0, length);
                return Enumerable.Range(index - quantity + 1, quantity)
                            .Select(i => prefix + i.ToString(format) + suffix)
                            .ToList();
            }
        }

        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string strSql)
        {
            return new SqlHelper().ExecuteScalar(strSql, _connectionStringName);
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string procName, List<DbParameter> parameters)
        {
            return new SqlHelper().ExecuteScalar(procName, parameters, _connectionStringName);
        }
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public virtual DataTable ExecuteDataTable(string strSql)
        {
            return new SqlHelper().ExecuteDataTable(strSql, _connectionStringName);
        }
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public virtual DataTable ExecuteDataTable(string procName, List<DbParameter> parameters)
        {
            return new SqlHelper().ExecuteDataTable(procName, parameters, _connectionStringName);
        }
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(string strSql)
        {
            return new SqlHelper().ExecuteDataSet(strSql, _connectionStringName);
        }
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public virtual DataSet ExecuteDataSet(string procName, List<DbParameter> parameters)
        {
            return new SqlHelper().ExecuteDataSet(procName, parameters, _connectionStringName);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <param name="direction">输入或输出(ParameterDirection.Input/ParameterDirection.Output)</param>
        /// <returns></returns>
        public virtual SqlParameter AddParameter(string parameterName, SqlDbType dbType, int size, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, dbType);
            param.Direction = direction;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual SqlParameter AddInParameter(string parameterName, SqlDbType dbType, int size, object value)
        {
            return AddParameter(parameterName, dbType, size, value, ParameterDirection.Input);
        }
        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        public virtual SqlParameter AddOutParameter(string parameterName, SqlDbType dbType, int size)
        {
            return AddParameter(parameterName, dbType, size, null, ParameterDirection.Output);
        }
        #endregion

        /// <summary>
        /// 创建数据访问对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IDAO<T> CreateDAO<T>() where T : DataEntityBase
        {
            IDAO<T> dao = _daoCenter.Create<T>(_connectionStringName);
            return dao;
        }
    }
}
