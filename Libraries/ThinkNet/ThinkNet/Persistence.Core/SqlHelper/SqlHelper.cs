using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.DataEntity.Core;

namespace ThinkNet.Persistence.Core
{
    public class SqlHelper
    {
        #region 字    段

        /// <summary>
        /// 数据库连接字符串名称
        /// </summary>
        private string _ConnName = string.Empty;

        #endregion

        #region 属    性
        
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public SqlHelper()
            : this(ConnectionStrings.ConnCRM.ToString())
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connName">数据库连接字符串名称</param>
        public SqlHelper(string connName)
        {
            _ConnName = connName;
        }
        #endregion

        #region 基本方法

        #region 执行操作
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="mSqlInfo">SqlHelper信息对象</param>
        /// <param name="connName">数据库连接字符串名称</param>
        public object ExecuteNonQuery(SqlInfo mSqlInfo, string connName)
        {
            object result;
            Database database = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                if (mSqlInfo.IsProc)
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.CommandText = mSqlInfo.SqlOrProcName;
                if (mSqlInfo.Parameters != null)
                {
                    foreach (DbParameter param in mSqlInfo.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                if (mSqlInfo.IsProc)
                    result = database.ExecuteScalar(command);
                else
                    result = database.ExecuteNonQuery(command);
                return result;
            }
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="mSqlInfo">SqlHelper信息对象</param>
        public object ExecuteNonQuery(SqlInfo mSqlInfo)
        {
            return ExecuteNonQuery(mSqlInfo, _ConnName);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="connName">数据库连接字符串名称</param>
        public object ExecuteNonQuery(string strSql, string connName)
        {
            return ExecuteNonQuery(new SqlInfo(strSql, false, null), connName);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public object ExecuteNonQuery(string strSql)
        {
            return ExecuteNonQuery(new SqlInfo(strSql, false, null));
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="connName">数据库连接字符串名称</param>
        public object ExecuteNonQuery(string procName, List<DbParameter> parameters, string connName)
        {
            return ExecuteNonQuery(new SqlInfo(procName, parameters), connName);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数集合</param>
        public object ExecuteNonQuery(string procName, List<DbParameter> parameters)
        {
            return ExecuteNonQuery(new SqlInfo(procName, parameters));
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="mSqlInfo">SqlHelper信息对象</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public object ExecuteScalar(SqlInfo mSqlInfo, string connName)
        {
            Database database = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                if (mSqlInfo.IsProc)
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.CommandText = mSqlInfo.SqlOrProcName;
                if (mSqlInfo.Parameters != null)
                {
                    foreach (DbParameter param in mSqlInfo.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                return database.ExecuteScalar(command);
            }
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="mSqlInfo">SqlHelper信息对象</param>
        /// <returns></returns>
        public object ExecuteScalar(SqlInfo mSqlInfo)
        {
            return ExecuteScalar(mSqlInfo, _ConnName);
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public object ExecuteScalar(string strSql, string connName)
        {
            return ExecuteScalar(new SqlInfo(strSql, false, null), connName);
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public object ExecuteScalar(string strSql)
        {
            return ExecuteScalar(new SqlInfo(strSql, false, null));
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public object ExecuteScalar(string procName, List<DbParameter> parameters, string connName)
        {
            return ExecuteScalar(new SqlInfo(procName, true, parameters), connName);
        }
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public object ExecuteScalar(string procName, List<DbParameter> parameters)
        {
            return ExecuteScalar(new SqlInfo(procName, true, parameters));
        }

        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="mSqlInfo">SqlHelper信息对象</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(SqlInfo mSqlInfo, string connName)
        {
            Database database = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                if (mSqlInfo.IsProc)
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.CommandText = mSqlInfo.SqlOrProcName;
                if (mSqlInfo.Parameters != null)
                {
                    foreach (DbParameter param in mSqlInfo.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
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
        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="mSqlInfo">命令SqlHelper信息对象</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(SqlInfo mSqlInfo)
        {
            return ExecuteDataTable(mSqlInfo, _ConnName);
        }
        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strSql, string connName)
        {
            return ExecuteDataTable(new SqlInfo(strSql, false, null), connName);
        }
        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strSql)
        {
            return ExecuteDataTable(new SqlInfo(strSql, false, null));
        }
        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string procName, List<DbParameter> parameters, string connName)
        {
            return ExecuteDataTable(new SqlInfo(procName, true, parameters), connName);
        }
        /// <summary>
        /// 读取数据表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string procName, List<DbParameter> parameters)
        {
            return ExecuteDataTable(new SqlInfo(procName, true, parameters));
        }

        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="mSqlInfo">命令SqlHelper信息对象</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(SqlInfo mSqlInfo, string connName)
        {
            Database database = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand command = database.DbProviderFactory.CreateCommand())
            {
                if (mSqlInfo.IsProc)
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                command.CommandText = mSqlInfo.SqlOrProcName;
                if (mSqlInfo.Parameters != null)
                {
                    foreach (DbParameter param in mSqlInfo.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                DataSet ds = database.ExecuteDataSet(command);
                return ds;
            }
        }
        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="mSqlInfo">命令SqlHelper信息对象</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(SqlInfo mSqlInfo)
        {
            return ExecuteDataSet(mSqlInfo, _ConnName);
        }
        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string strSql, string connName)
        {
            return ExecuteDataSet(new SqlInfo(strSql, false, null), connName);
        }
        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string strSql)
        {
            return ExecuteDataSet(new SqlInfo(strSql, false, null));
        }
        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="connName">数据库连接字符串名称</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string procName, List<DbParameter> parameters, string connName)
        {
            return ExecuteDataSet(new SqlInfo(procName, true, parameters), connName);
        }
        /// <summary>
        /// 读取数据集表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string procName, List<DbParameter> parameters)
        {
            return ExecuteDataSet(new SqlInfo(procName, true, parameters));
        }
        #endregion

        #region 添加参数
        /// <summary>
        /// 添加参数方法
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <param name="direction">参数类型</param>
        /// <returns></returns>
        public SqlParameter AddParameter(string parameterName, SqlDbType dbType, int size, object value, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter(parameterName, dbType);
            param.Direction = direction;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 添加输入参数方法
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public SqlParameter AddInParameter(string parameterName, SqlDbType dbType, int size, object value)
        {
            return AddParameter(parameterName, dbType, size, value, ParameterDirection.Input);
        }
        /// <summary>
        /// 添加输出参数方法
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">参数大小</param>
        /// <returns></returns>
        public SqlParameter AddOutParameter(string parameterName, SqlDbType dbType, int size)
        {
            return AddParameter(parameterName, dbType, size, null, ParameterDirection.Output);
        }
        #endregion

        #endregion

        #region 其他方法

        #endregion
    }
}
