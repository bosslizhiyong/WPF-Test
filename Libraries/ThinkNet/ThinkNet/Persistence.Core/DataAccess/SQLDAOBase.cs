using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ThinkNet.DataEntity.Core;
using ThinkNet.Utility;
using ThinkNet.Utility;

namespace ThinkNet.Persistence.Core
{
    /// <summary>
    /// 数据(持久化)访问对象的抽象基类(基于SQLServer)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SQLDAOBase<T> : IDAO<T> 
        where T : DataEntityBase,new ()
    {
        #region 字段
        /// <summary>
        /// 数据实体
        /// </summary>
        private T _t = null;
        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        private string _ConnName = string.Empty;
        /// <summary>
        /// 主键名称
        /// </summary>
        private string _PrimaryKey = string.Empty;
        /// <summary>
        /// 数据表名称
        /// </summary>
        private string _TableName = string.Empty;
        #endregion

        #region 属性
        /// <summary>
        /// 数据实体
        /// </summary>
        private T t
        {
            get
            {
                if (this._t == null)
                {
                    this._t = Activator.CreateInstance<T>();
                }
                return this._t;
            }
        }
        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        public string ConnName
        {
            get
            {
                //外部设置数据库链接
                if (this.t.IsExternalConnection)
                {
                    if (string.IsNullOrEmpty(this._ConnName))
                    {
                        this._ConnName = string.IsNullOrEmpty(this.t.ExternalConnectionStringsName) ? this.t.ConnectionStringsName.ToString() : this.t.ExternalConnectionStringsName;
                    }
                    return this._ConnName;
                }
                return this.t.ConnectionStringsName.ToString();
            }
            set
            {
                this._ConnName = value;
            }
        }
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                if (string.IsNullOrEmpty(_PrimaryKey))
                {
                    this._PrimaryKey = this.t.PrimaryKey;
                }
                return this._PrimaryKey;
            }
            set
            {
                this._PrimaryKey = value;
            }
        }
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_TableName))
                {
                    this._TableName = "[" + typeof(T).Name + "]";
                }
                return this._TableName;
            }
            set
            {
                this._TableName = value;
            }
        }
        /// <summary>
        /// 是否外部设置数据库
        /// </summary>
        public bool IsExternalConnection
        {
            get
            {
                return this.t.IsExternalConnection;
            }
            set
            {
                this.t.IsExternalConnection = value;
            }
        }
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        public string ExternalConnectionStringsName
        {
            get
            {
                return this.t.ExternalConnectionStringsName;
            }
            set
            {
                this.t.ExternalConnectionStringsName = value;
            }
        }
        /// <summary>
        /// 最大ID值
        /// </summary>
        public int MaxID
        {
            get
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("SELECT MAX({0}) FROM {1}", this.PrimaryKey, this.TableName));
                Database database = DatabaseFactory.CreateDatabase(this.ConnName);
                using (DbCommand command = database.GetSqlStringCommand(strSql.ToString()))
                {
                    object obj = database.ExecuteScalar(command);
                    if (obj == null || string.IsNullOrEmpty(obj.ToString().Trim()))
                    {
                        return 0;
                    }
                    return DataTypeConvert.ToInt32(obj, 0);
                }
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public SQLDAOBase()
        {
            this._ConnName = string.Empty;
            this._PrimaryKey = string.Empty;
            this._TableName = string.Empty;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public virtual int Add(T dataEntity)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append(string.Format("INSERT INTO {0}(", this.TableName));

            //公共属性
            PropertyInfo[] properties = GetNonUnderlineAndKeyProperties();
            List<Parameter> listParameter = new List<Parameter>();//值参数列表
            StringBuilder strParameter = new StringBuilder();//值参数串
            for (int i = 0; i < properties.Length; i++)
            {
                strSql.Append("[" + properties[i].Name + "],");
                strParameter.Append("@" + properties[i].Name + ",");
                Parameter parameter = new Parameter();
                parameter.ParameterName = properties[i].Name;
                parameter.ParameterValue = properties[i].GetValue(dataEntity, null);
                parameter.ParameterDbType = DataTypeConvert.ToDbType(properties[i].PropertyType);
                listParameter.Add(parameter);
            }
            strSql = strSql.Replace(",", ")", strSql.Length - 1, 1);
            strSql.Append(" VALUES (");
            strParameter = strParameter.Replace(",", ")", strParameter.Length - 1, 1);
            strSql.Append(strParameter + ";");
            //如果实体对象为自增主键，返回当前主键值
            if (this.t.IsAutoID)
            {
                strSql.Append("SELECT SCOPE_IDENTITY()");
            }
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                foreach (Parameter param in listParameter)
                {
                    database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                }
                object obj = database.ExecuteScalar(command);
                return DataTypeConvert.ToInt32(obj, 0);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dataEntity">数据实体列表</param>
        /// <returns></returns>
        public virtual bool Add(List<T> list)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (T dataEntity in list)
                    {
                        Add(dataEntity);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public virtual bool Update(T dataEntity)
        {
            return Update(dataEntity, null);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataEntity">数据实体列表</param>
        /// <returns></returns>
        public virtual bool Update(List<T> list)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (T dataEntity in list)
                    {
                        Update(dataEntity);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <param name="columns">字段(逗号分隔)</param>
        /// <returns></returns>
        public virtual bool Update(T dataEntity, string columns)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append("UPDATE " + this.TableName + " SET ");

            //公共属性
            PropertyInfo[] properties = GetNonUnderlineProperties(null);
            List<Parameter> listParameter = new List<Parameter>();
            //更新全部字段
            if (string.IsNullOrEmpty(columns))
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    //剔除实体对象主键是自增的主键属性
                    if (properties[i].Name != this.PrimaryKey)
                    {
                        strSql.Append(properties[i].Name + "=@" + properties[i].Name + ",");
                    }
                    Parameter parameter = new Parameter();
                    parameter.ParameterName = properties[i].Name;
                    parameter.ParameterValue = properties[i].GetValue(dataEntity, null);
                    parameter.ParameterDbType = DataTypeConvert.ToDbType(properties[i].PropertyType);
                    listParameter.Add(parameter);
                }
            }
            else//更新具体字段
            {
                string[] arrColumns = columns.Split(new char[] { ',' });
                bool isExitsKey = false;//是否包含主键字段
                PropertyInfo ptKey = null;
                for (int j = 0; j < arrColumns.Length; j++)
                {
                    //剔除带下划线(_)的公共属性
                    if (arrColumns[j].IndexOf('_') == 0)
                    {
                        continue;
                    }
                    if (arrColumns[j].ToUpper()==this.PrimaryKey.ToUpper())
                    {
                        isExitsKey = true;
                    }
                    foreach (PropertyInfo pt in properties)
                    {
                        if (pt.Name == this.PrimaryKey)
                        {
                            ptKey = pt;
                        }
                        if (pt.Name == arrColumns[j])
                        {
                            //剔除实体对象主键是自增的主键属性
                            if (pt.Name != this.PrimaryKey)
                            {
                                strSql.Append(pt.Name + "=@" + pt.Name + ",");
                            }
                            Parameter parameter = new Parameter();
                            parameter.ParameterName = pt.Name;
                            parameter.ParameterValue = pt.GetValue(dataEntity, null);
                            parameter.ParameterDbType = DataTypeConvert.ToDbType(pt.PropertyType);
                            listParameter.Add(parameter);
                            break;
                        }
                    }//end foreach (PropertyInfo pt in properties)
                }//end for (int j = 0; j < arrColumns.Length; j++)
                if(!isExitsKey)
                {
                    Parameter parameter = new Parameter();
                    parameter.ParameterName = ptKey.Name;
                    parameter.ParameterValue = ptKey.GetValue(dataEntity, null);
                    parameter.ParameterDbType = DataTypeConvert.ToDbType(ptKey.PropertyType);
                    listParameter.Add(parameter);
                }
            }//end if
            strSql = strSql.Replace(",", " ", strSql.Length - 1, 1);
            strSql.Append(" WHERE " + this.PrimaryKey + "=@" + this.PrimaryKey);
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                foreach (Parameter param in listParameter)
                {
                    database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                }
                int count = database.ExecuteNonQuery(command);
                return count > 0;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dataEntity">数据实体</param>
        /// <returns></returns>
        public virtual int Delete(T dataEntity)
        {
            return Delete(DataTypeConvert.ToInt32(dataEntity.KeyID));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        public virtual int Delete(int id)
        {
            string where = string.Format("{0}={1}", this.PrimaryKey, id);
            return Delete(where);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        public virtual int Delete(long id)
        {
            string where = string.Format("{0}={1}", this.PrimaryKey, id);
            return Delete(where);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        public virtual int Delete(string where)
        {
            return Delete(where, null);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        public virtual int Delete(string where, List<Parameter> listParameter)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append("DELETE " + this.TableName);
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                if (listParameter != null && listParameter.Count > 0)
                {
                    foreach (Parameter param in listParameter)
                    {
                        database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                    }
                }
                int count = database.ExecuteNonQuery(command);
                return count;
            }
        }
        #endregion

        #region 是否存在
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        public virtual bool Exists(int id)
        {
            List<Parameter> listParameter = new List<Parameter>();
            Parameter param = new Parameter();
            param.ParameterName = this.PrimaryKey;
            param.ParameterDbType = DbType.Int32;
            param.ParameterValue = id;
            listParameter.Add(param);
            string where = string.Format("{0}=@{0}", this.PrimaryKey);
            return Exists(where, listParameter);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        public virtual bool Exists(long id)
        {
            List<Parameter> listParameter = new List<Parameter>();
            Parameter param = new Parameter();
            param.ParameterName = this.PrimaryKey;
            param.ParameterDbType = DbType.Int64;
            param.ParameterValue = id;
            listParameter.Add(param);
            string where = string.Format("{0}=@{0}", this.PrimaryKey);
            return Exists(where, listParameter);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        public virtual bool Exists(string where)
        {
            return Exists(where, null);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        public virtual bool Exists(string where, List<Parameter> listParameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM " + this.TableName);
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                if (listParameter != null && listParameter.Count > 0)
                {
                    foreach (Parameter param in listParameter)
                    {
                        database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                    }
                }
                object obj = database.ExecuteScalar(command);
                return DataTypeConvert.ToInt32(obj, 0) > 0;
            }
        }
        #endregion

        #region 获取记录数
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        public virtual int GetCount(string where)
        {
            return GetCount(where, null);
        }
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        public virtual int GetCount(string where, List<Parameter> listParameter)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append("SELECT COUNT(0) FROM " + this.TableName);
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" WHERE " + where);
            }
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                if (listParameter != null && listParameter.Count > 0)
                {
                    foreach (Parameter param in listParameter)
                    {
                        database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                    }
                }
                object obj = database.ExecuteScalar(command);
                return DataTypeConvert.ToInt32(obj, 0);
            }
        }
        #endregion

        #region 获取数据实体
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="id">主键值(整型)</param>
        /// <returns></returns>
        public virtual T GetDataEntity(int id)
        {
            string where = string.Format("{0}={1}", this.PrimaryKey, id);
            return GetDataEntity(where);
        }
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="id">主键值(长整型)</param>
        /// <returns></returns>
        public virtual T GetDataEntity(long id)
        {
            string where = string.Format("{0}={1}", this.PrimaryKey, id);
            return GetDataEntity(where);
        }
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns></returns>
        public virtual T GetDataEntity(string where)
        {
            return GetDataEntity(where, null);
        }
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="listParameter">参数列表</param>
        /// <returns></returns>
        public virtual T GetDataEntity(string where, List<Parameter> listParameter)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            strSql.Append(string.Format("SELECT TOP 1 {0} FROM {1}", "*", this.TableName));
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append("  WHERE " + where);
            }
            T model = default(T);
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql.ToString();
                if (listParameter != null)
                {
                    foreach (Parameter param in listParameter)
                    {
                        database.AddInParameter(command, param.ParameterName, param.ParameterDbType, param.ParameterValue);
                    }
                }
                using (IDataReader reader = database.ExecuteReader(command))
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            model = GetDataEntity(reader);
                        }
                    }
                }
                return model;
            }
        }
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        public virtual T GetDataEntity(DataRow row)
        {
            T model = new T();
            //公共属性
            PropertyInfo[] arrPropertyInfo = GetNonUnderlineProperties(model);
            foreach (PropertyInfo pt in arrPropertyInfo)
            {
                try
                {
                    if (row[pt.Name].ToString() != "")
                    {
                        pt.SetValue(model, row[pt.Name], null);
                    }
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }
            return model;
        }
        /// <summary>
        /// 获取数据实体
        /// </summary>
        /// <param name="reader">数据流</param>
        /// <returns></returns>
        public virtual T GetDataEntity(IDataReader reader)
        {
            T model = new T();
            //公共属性
            PropertyInfo[] arrPropertyInfo = GetNonUnderlineProperties(model);
            int i;
            object val;
            foreach (PropertyInfo pt in arrPropertyInfo)
            {
                try
                {
                    i = reader.GetOrdinal(pt.Name);
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
                if (reader.IsDBNull(i))
                {
                    continue;
                }
                val = reader.GetValue(i);
                pt.SetValue(model, val, null);
            }
            return model;
        }
        #endregion

        #region 获取数据集合
        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        public virtual IDataReader GetReader(Pager mPager)
        {
            return GetPagerDataReader(mPager);
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <returns></returns>
        /// <param name="mPager">分页参数</param>
        public virtual DataTable GetDataTable(Pager mPager)
        {
            return GetPagerDataTable(mPager);
        }
        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        public virtual List<T> GetList(Pager mPager)
        {
            using (IDataReader reader = GetPagerDataReader(mPager))
            {
                return GetList(reader);
            }
        }
        /// <summary>
        /// 获取数据实体列表
        /// </summary>
        /// <param name="reader">数据流</param>
        /// <returns></returns>
        public virtual List<T> GetList(IDataReader reader)
        {
            List<T> list = new List<T>();
            while (reader.Read())
            {
                list.Add(GetDataEntity(reader));
            }
            return list;
        }

        /// <summary>
        /// 获取分页的数据流
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        public virtual IDataReader GetPagerDataReader(Pager mPager)
        {
            string strSql = BuildSql(mPager);
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql;
                return database.ExecuteReader(command);
            }
        }
        /// <summary>
        /// 获取分页的数据集合
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        public virtual DataTable GetPagerDataTable(Pager mPager)
        {
            string strSql = BuildSql(mPager);
            Database database = DatabaseFactory.CreateDatabase(this.ConnName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = strSql;
                DataSet ds = database.ExecuteDataSet(command);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 构建SQL语句
        /// </summary>
        /// <param name="mPager">分页参数</param>
        /// <returns></returns>
        public virtual string BuildSql(Pager mPager)
        {
            StringBuilder strSql = new StringBuilder();//完整SQL语句
            //查询字段
            string strColumns = "";
            if (string.IsNullOrEmpty(mPager.Columns) || (mPager.Columns == "*"))
            {
                //公共属性
                PropertyInfo[] properties = GetNonUnderlineProperties(null);
                foreach (PropertyInfo info in properties)
                {
                    strColumns = strColumns + "[" + info.Name + "],";
                }
                strColumns = strColumns.Substring(0, strColumns.Length - 1);
            }
            else
            {
                strColumns = DataTypeConvert.FormatToSqlString(mPager.Columns);
            }
            //排序字段
            string strOrder = "";
            string orderField = mPager.OrderField;
            if (string.IsNullOrEmpty(orderField))
            {
                orderField = this.PrimaryKey;
                if (mPager.OrderType)
                {
                    strOrder = string.Format(" ORDER BY {0} DESC", orderField);//降序
                }
                else
                {
                    strOrder = string.Format(" ORDER BY {0} ASC", orderField);//升序
                }
            }
            else
            {
                //多个字段排序
                if (orderField.Contains(","))
                {
                    strOrder = string.Format(" ORDER BY {0} ", orderField);
                }
                else
                {
                    if (orderField.TrimEnd().ToUpper().EndsWith("DESC") || orderField.TrimEnd().ToUpper().EndsWith("ASC"))
                    {
                        strOrder = string.Format(" ORDER BY {0}", orderField);
                    }
                    else
                    {
                        if (mPager.OrderType)
                        {
                            strOrder = string.Format(" ORDER BY {0} DESC", orderField);//降序
                        }
                        else
                        {
                            strOrder = string.Format(" ORDER BY {0} ASC", orderField);//升序
                        }
                    }//end if (orderField.ToUpper().EndsWith("DESC") || orderField.ToUpper().EndsWith("ASC"))
                }//end if (orderField.Contains(","))
            }

            string where = mPager.Where;
            int pageIndex = mPager.PageIndex;
            int pageSize = mPager.PageSize;
            string tableName = string.IsNullOrEmpty(mPager.TableName)?this.TableName:mPager.TableName;
            if (string.IsNullOrEmpty(where))
            {
                strSql.Append(string.Format("SELECT {0} FROM (SELECT {1}, ROW_NUMBER() OVER({2}) AS row FROM {3}", new object[] { strColumns, strColumns, strOrder, tableName }));
                strSql.Append(string.Format(") a WHERE row BETWEEN {0} AND {1}", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize));
            }
            else
            {
                strSql.Append(string.Format("SELECT {0} FROM (SELECT {1}, ROW_NUMBER() OVER({2}) AS row FROM {3}", new object[] { strColumns, strColumns, strOrder, tableName }));
                strSql.Append(string.Format(" WHERE {0}", where));
                strSql.Append(string.Format(") a WHERE row BETWEEN {0} AND {1}", ((pageIndex - 1) * pageSize) + 1, pageIndex * pageSize));
            }
            return strSql.ToString();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取数据实体的属性(剔除带下划线(_)的公共属性、剔除实体对象主键是自增的主键属性)
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] GetNonUnderlineAndKeyProperties()
        {
            List<PropertyInfo> listPropertyInfo = new List<PropertyInfo>();
            //反射取得实体对象的公共属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo info in properties)
            {
                //剔除带下划线(_)的公共属性、剔除实体对象主键是自增的主键属性
                if (info.Name.IndexOf('_') == 0 || (this.t.IsAutoID && this.t.PrimaryKey == info.Name))
                {
                    continue;
                }
                if(info.Name=="KeyID")
                {
                    continue;
                }
                listPropertyInfo.Add(info);
            }
            return listPropertyInfo.ToArray();
        }
        /// <summary>
        /// 获取数据实体的属性(剔除带下划线(_)的公共属性)
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] GetNonUnderlineProperties(T model)
        {
            List<PropertyInfo> listPropertyInfo = new List<PropertyInfo>();
            //反射取得实体对象的公共属性
            PropertyInfo[] properties = null;
            if (model == null)
            {
                properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            }
            else
            {
                properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            }
            foreach (PropertyInfo info in properties)
            {
                //剔除带下划线(_)的公共属性
                if (info.Name.IndexOf('_') == 0)
                {
                    continue;
                }
                if (info.Name == "KeyID")
                {
                    continue;
                }
                listPropertyInfo.Add(info);
            }
            return listPropertyInfo.ToArray();
        }
        /// <summary>
        /// 获取数据实体的属性
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] GetProperties()
        {
            //反射取得实体对象的公共属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            List<PropertyInfo> listPropertyInfo = new List<PropertyInfo>();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name == "KeyID")
                {
                    continue;
                }
                listPropertyInfo.Add(info);
            }
            return listPropertyInfo.ToArray();
        }
        #endregion
    }
}
