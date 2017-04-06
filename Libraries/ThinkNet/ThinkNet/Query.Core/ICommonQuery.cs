using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Query.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonQuery
    {
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        string ExternalConnectionStringsName { set; }
        /// <summary>
        /// 获取分页集合，面向支持分页的表格
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        PageResult<DataRow> GetPagerTable(string strSql, int pageIndex, int pageSize, bool orderType, string orderField);
        /// <summary>
        /// 获取分页的SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderType">排序规则(true-降序；flase-升序)(注:orderField参数有多个或单个以DESC/ASC结尾时此参数无效)</param>
        /// <param name="orderField">排序字段(注:可以直接加上DESC/ASC)</param>
        /// <returns></returns>
        string GetPagerSql(string strSql, int pageIndex, int pageSize, bool orderType, string orderField);

        /// <summary>
        /// 获取单个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <returns></returns>
        string GetSingleSequenceNumber(string sequenceType);
        /// <summary>
        /// 获取单个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <param name="tableName">表名称</param>
        /// <param name="fieldName">编号字段名称</param>
        /// <param name="where">其它条件</param>
        /// <returns></returns>
        string GetNotrepeatSequenceNumber(string sequenceType, string tableName,string fieldName,string where);
        /// <summary>
        /// 获取多个流水编号
        /// </summary>
        /// <param name="sequenceType">流水编号类别</param>
        /// <param name="quantity">数量</param>
        /// <returns></returns>
        List<string> GetMultiSequenceNumber(string sequenceType, int quantity);

        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        object ExecuteScalar(string strSql);
        /// <summary>
        /// 读取一个数据
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        object ExecuteScalar(string procName, List<DbParameter> parameters);
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string strSql);
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string procName, List<DbParameter> parameters);
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string strSql);
        /// <summary>
        /// 读取数据集合
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string procName, List<DbParameter> parameters);

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <param name="direction">输入或输出(ParameterDirection.Input/ParameterDirection.Output)</param>
        /// <returns></returns>
        SqlParameter AddParameter(string parameterName, SqlDbType dbType, int size, object value, ParameterDirection direction);
        /// <summary>
        /// 添加输入参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        SqlParameter AddInParameter(string parameterName, SqlDbType dbType, int size, object value);
        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">数据类型(如:SqlDbType.NVarChar)</param>
        /// <param name="size">大小</param>
        /// <returns></returns>
        SqlParameter AddOutParameter(string parameterName, SqlDbType dbType, int size);
    }
}
