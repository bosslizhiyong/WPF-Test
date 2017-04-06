using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 数据访问基础类(基于SQLServer)
    /// </summary>
    public class SQLServerHelper : DatabaseHelper
    {
        private SqlConnection _mConnection=null;//数据库连接
        private SqlTransaction _mTransaction=null;//事务处理类
        private bool _IsInTransaction = false;   //指示当前是否正处于事务中
        private string _strConn = "";//连接数据库字符串

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConn">连接数据库字符串</param>
        public SQLServerHelper(string strConn)
	    {
            _strConn = strConn;
            _mConnection = new SqlConnection(strConn);
            _IsInTransaction = false;
	    }

        #region 【打开关闭】
        //得到数据库连接
        public override IDbConnection Connection
        {
            get { return _mConnection; }
        }
        //打开一个连接
        public override void Open()
        {
            if (_mConnection.State != ConnectionState.Open)
            {
                if (_mConnection == null)
                {
                    _mConnection = new SqlConnection(_strConn);
                }
                if (_mConnection.State.Equals(ConnectionState.Closed))
                {
                    _mConnection.ConnectionString = _strConn;
                    _mConnection.Open();
                }
            }
        }
        //关闭一个连接
        public override void Close()
        {
            if (_mConnection.State == ConnectionState.Open)
            {
                _mConnection.Close();
            }
        }
        #endregion

        #region 【事务处理】
        //开始一个事务
        public override void BeginTrans()
        {
            try
            {
                Open();
                _mTransaction = _mConnection.BeginTransaction();
                _IsInTransaction = true;
            }
            catch
            {
                _IsInTransaction = false;
            }
        }
        //提交一个事务
        public override void CommitTrans()
        {
            try
            {
                _mTransaction.Commit();
                Close();
                _IsInTransaction = false;
            }
            catch
            {
                RollbackTrans();
            }
        }
        //回滚一个事务
        public override void RollbackTrans()
        {
            _mTransaction.Rollback();
            Close();
            _IsInTransaction = false;
        }
        //当前是否正处于事务中
        public override bool IsInTrans
        {
            get { return _IsInTransaction; }
        }
        #endregion

        #region 【常规操作】
        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string strSql, CommandType cmdType, params object[] paras)
        {
            try
            {
                int result = 0;
                using (SqlCommand cmd = new SqlCommand() { Connection = _mConnection, CommandText = strSql, CommandType = cmdType })
                {
                    if (paras != null)
                    {
                        foreach (SqlParameter item in paras)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }
                    //如果不是出于事务状态，则开启数据库连接，否则给cmd设置事务
                    if (!_IsInTransaction)
                    {
                        Open();
                    }
                    else
                    {
                        cmd.Transaction = _mTransaction;
                    }
                    //执行SQL语句
                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception)
            {
                if (_IsInTransaction)
                {
                    RollbackTrans();
                }
            }
            finally
            {
                if (!_IsInTransaction)
                {
                    Close();
                }
            }
            return 0;
        }
        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public override int ExecuteNonQuery(string strSql)
        {
            return ExecuteNonQuery(strSql, CommandType.Text, null);
        }

        /// <summary>
        /// 执行SQL语句返回一个数据
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public override object ExecuteScalar(string strSql, CommandType cmdType, params object[] paras)
        {
            try
            {
                object result = null;
                using (SqlCommand cmd = new SqlCommand() { Connection = _mConnection, CommandText = strSql, CommandType = cmdType })
                {
                    if (paras != null)
                    {
                        foreach (SqlParameter item in paras)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }
                    //如果不是出于事务状态，则开启数据库连接，否则给cmd设置事务
                    if (!_IsInTransaction)
                    {
                        Open();
                    }
                    else
                    {
                        cmd.Transaction = _mTransaction;
                    }
                    //执行SQL语句
                    result = cmd.ExecuteScalar();
                }
                return result;
            }
            catch (Exception)
            {
                if (_IsInTransaction)
                {
                    RollbackTrans();
                }
            }
            finally
            {
                if (!_IsInTransaction)
                {
                    Close();
                }
            }
            return null;
        }
        /// <summary>
        /// 执行SQL语句返回一个数据
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public override object ExecuteScalar(string strSql)
        {
            return ExecuteScalar(strSql, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL语句返回结果集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public override DataTable ExecuteTable(string strSql, CommandType cmdType, params object[] paras)
        {
            try
            {
                DataSet ds = new DataSet();
                if (!_IsInTransaction)
                {
                    Open();
                }
                //创建SqlCommand 对象
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = _mConnection;
                    cmd.CommandType = cmdType;
                    cmd.CommandText = strSql;
                    //判断cmd 参数是否为空，如果不为空则传入参数
                    if (paras != null)
                    {
                        SqlParameter[] cmdParms = (SqlParameter[])paras;
                        foreach (SqlParameter parm in cmdParms)
                        {
                            cmd.Parameters.Add(parm);
                        }
                    }
                    //创建数据适配器对象 用于填充Dataset
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //填充DataSet 对象
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                }
                return ds.Tables[0];
            }
            catch (Exception)
            {
                if (_IsInTransaction)
                {
                    RollbackTrans();
                }
            }
            finally
            {
                if (!_IsInTransaction)
                {
                    Close();
                }
            }
            return null;
        }
        /// <summary>
        /// 执行SQL语句返回结果集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <returns></returns>
        public override DataTable ExecuteTable(string strSql)
        {
            return ExecuteTable(strSql, CommandType.Text, null);
        }
        /// <summary>
        /// 执行SQL语句,分页获取数据集
        /// </summary>
        /// <param name="strSql">要执行的SQL语句</param>
        /// <param name="cmdType">SQL语句的类型</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="paras">参数数组</param>
        /// <returns></returns>
        public override DataTable ExecuteTableByPage(string strSql, CommandType cmdType, int pageSize, int pageIndex, params object[] paras)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
