using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [Flags]
    public enum DatabaseType
    {
        /// <summary>
        /// MySQL
        /// </summary>
        [Description("MySQL")]
        MySQL = 1,
        /// <summary>
        /// OleDB
        /// </summary>
        [Description("OleDB")]
        OleDB = 2,
        /// <summary>
        /// Oracle
        /// </summary>
        [Description("Oracle")]
        Oracle = 3,
        /// <summary>
        /// SQLLite
        /// </summary>
        [Description("SQLLite")]
        SQLLite = 4,
        /// <summary>
        /// SQLServer
        /// </summary>
        [Description("SQLServer")]
        SQLServer = 5
    }

    public abstract class DatabaseHelper
    {
        #region 【打开关闭】
        //得到数据库连接
        public abstract IDbConnection Connection { get; }
        //打开一个连接
        public abstract void Open();
        //关闭一个连接
        public abstract void Close();
        #endregion

        #region 【事务处理】
        //开始一个事务
        public abstract void BeginTrans();
        //提交一个事务
        public abstract void CommitTrans();
        //回滚一个事务
        public abstract void RollbackTrans();
        //当前是否正处于事务中
        public abstract bool IsInTrans { get; }
        #endregion

        #region 【常规操作】
        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(string strSql, CommandType cmdType, params object[] paras);
        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(string strSql);

        /// <summary>
        /// 执行SQL语句返回一个数据
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public abstract object ExecuteScalar(string strSql, CommandType cmdType, params object[] paras);
        /// <summary>
        /// 执行SQL语句返回一个数据
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public abstract object ExecuteScalar(string strSql);
        /// <summary>
        /// 执行SQL语句返回结果集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public abstract DataTable ExecuteTable(string strSql, CommandType cmdType, params object[] paras);
        /// <summary>
        /// 执行SQL语句返回结果集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public abstract DataTable ExecuteTable(string strSql);
        /// <summary>
        /// 执行SQL语句,分页获取数据集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public abstract DataTable ExecuteTableByPage(string strSql, CommandType cmdType, int pageSize, int pageIndex, params object[] paras);
        #endregion
    }
}