using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ThinkNet.Command.Core;
using ThinkNet.Component;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Query.Core;
using ThinkNet.Utility;
using WCFWeb.Co.Contract;

namespace WCFWeb.Co
{
    public  class ServiceBase : IServiceBase
    {
        #region 字    段

        /// <summary>
        /// 查询参数
        /// </summary>
        protected QueryParamater _mQueryParamater = null;

        /// <summary>
        /// 简单结果描述
        /// </summary>
        protected SimpleResult _mSimpleResult = null;

        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        protected string _connectionStringName = "";


        #endregion 字    段

        #region 属    性

        /// <summary>
        /// 命令总线
        /// </summary>
        protected ICommandBus CommandBus
        {
            get
            {
                ICommandBus commandBus = ObjectContainer.Resolve<ICommandBus>();
                if (!string.IsNullOrEmpty(_connectionStringName))//不为空时，使用外部设置的数据库
                {
                    commandBus.ExternalConnectionStringsName = "Conn" + _connectionStringName;
                }
                return commandBus;
            }
        }

        /// <summary>
        /// 查询服务
        /// </summary>
        protected IDynamicQuery QueryService
        {
            get
            {
                IDynamicQuery dynamicQuery = ObjectContainer.Resolve<IDynamicQuery>();
                if (!string.IsNullOrEmpty(_connectionStringName))//不为空时，使用外部设置的数据库
                {
                    dynamicQuery.ExternalConnectionStringsName = "ConnQuery" + _connectionStringName;
                }
                return dynamicQuery;
            }
        }

        /// <summary>
        /// 日志管理
        /// </summary>
        protected ILogger LogManager
        {
            get
            {
                return ObjectContainer.Resolve<ILoggerFactory>().Create("LoggerError");
            }
        }

        /// <summary>
        /// Json帮助
        /// </summary>
        protected IJsonSerializer JSonHelper
        {
            get
            {
                return ObjectContainer.Resolve<IJsonSerializer>();
            }
        }

        #endregion 属    性

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceBase()
        {
            _mQueryParamater = QueryParamater.Create();
            _mSimpleResult = new SimpleResult();
        }

        #endregion 构造函数

        #region 异常日志

        /// <summary>
        /// 写错误日志
        /// </summary>
        protected void WriteExceptionLog(Exception ex)
        {
            System.Diagnostics.StackTrace mStackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame mStackFrame = mStackTrace.GetFrame(1);// 0为本身的方法；1为调用方法
            string className = mStackFrame.GetMethod().ReflectedType.Name; // 类名
            string methodName = mStackFrame.GetMethod().Name; // 方法名
            LogManager.Error(className + "-->" + methodName + "：" + ex.Message);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        protected void WriteExceptionLog(string errInfo)
        {
            System.Diagnostics.StackTrace mStackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame mStackFrame = mStackTrace.GetFrame(1);// 0为本身的方法；1为调用方法
            string className = mStackFrame.GetMethod().ReflectedType.Name; // 类名
            string methodName = mStackFrame.GetMethod().Name; // 方法名
            LogManager.Error(className + "-->" + methodName + "：" + errInfo);
        }

        #endregion 异常日志

        public void SetExternalConnectionStringsName(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        public bool CheckEmployee(string erpEmployeeId)
        {
            if (string.IsNullOrEmpty(erpEmployeeId))
            {
                return false;
            }
            if (erpEmployeeId == "1")
            {
                return true;
            }
            try
            {
                string strSql = "SELECT ID FROM CM_User WHERE ID =" + erpEmployeeId;
                DataTable table = QueryService.ExecuteDataTable(strSql);
                if (table != null && table.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据用户登录ID获取ID获取权限
        /// </summary>
        /// <param name="erpEmployeeId">员工登录ID</param>
        /// <returns></returns>
        public string GetEmployeeBusIDs(string erpEmployeeId)
        {
            string bussIDs = "";
            try
            {
                if (string.IsNullOrEmpty(erpEmployeeId))
                {
                    return "( -1 )";
                }
                if (erpEmployeeId == "1")
                {
                    return "";
                }

                string strWhere = "";
                if (erpEmployeeId != "1")
                {
                    strWhere = " AND T1.UserID = " + erpEmployeeId;
                }
                string strSql = @" SELECT T0.BusinessID from CO_BusinessEmployee_Ref T0
                                   LEFT  JOIN CO_Employee T1 ON T1.ID = T0.EmployeeID WHERE  1=1 " + strWhere;
                DataTable table = QueryService.ExecuteDataTable(strSql);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bussIDs += row["BusinessID"] + ",";
                    }
                    return " ( " + bussIDs.Substring(0, bussIDs.Length - 1) + " ) ";
                }
                else
                {
                    return "( -1 )";
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return "( -1 )";
            }
        }

        /// <summary>
        /// 获取往来单位权限
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public string GetBusIDsByUserID(string EmployeeId, int TypeID)
        {
            string bussIDs = "";
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    return "( -1 )";
                }
                if (EmployeeId == "1")
                {
                    return "";
                }

                List<DbParameter> parameters = new List<DbParameter>();
                parameters.Add(QueryService.AddInParameter("@UserID", SqlDbType.Int, 0, EmployeeId));
                parameters.Add(QueryService.AddInParameter("@BusinessProperty", SqlDbType.Int, 0, TypeID));
                parameters.Add(QueryService.AddInParameter("@ReturnType", SqlDbType.Int, 0, 0));//集合 //字符串
                //执行存储过程
                DataTable table = QueryService.ExecuteDataTable("[dbo].[proc_Authority_UserBusiness]", parameters);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bussIDs += row["ID"] + ",";
                    }
                    return "( " + bussIDs.Substring(0, bussIDs.Length - 1) + " )";
                }
                else
                {
                    return "( -1 )";
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return "( -1 )";
            }
        }

        /// <summary>
        /// 获取往来单位权限
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public string GetBusAllIDsByUserID(string EmployeeId)
        {
            string bussIDs = "";
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    return "( -1 )";
                }

                if (EmployeeId == "1")
                {
                    return "";
                }

                List<DbParameter> parameters = new List<DbParameter>();
                parameters.Add(QueryService.AddInParameter("@UserID", SqlDbType.Int, 0, EmployeeId));
                parameters.Add(QueryService.AddInParameter("@BusinessProperty", SqlDbType.Int, 0, 1));
                parameters.Add(QueryService.AddInParameter("@ReturnType", SqlDbType.Int, 0, 0));//集合 //字符串
                DataTable tableSupplier = QueryService.ExecuteDataTable("[dbo].[proc_Authority_UserBusiness]", parameters);

                parameters = new List<DbParameter>();
                parameters.Add(QueryService.AddInParameter("@UserID", SqlDbType.Int, 0, EmployeeId));
                parameters.Add(QueryService.AddInParameter("@BusinessProperty", SqlDbType.Int, 0, 2));
                parameters.Add(QueryService.AddInParameter("@ReturnType", SqlDbType.Int, 0, 0));//集合 //字符串
                DataTable tableClient = QueryService.ExecuteDataTable("[dbo].[proc_Authority_UserBusiness]", parameters);


                string strSql = @" SELECT *  FROM CO_Business WHERE BusinessProperty = 3 ";
                DataTable tableLogistics = QueryService.ExecuteDataTable(strSql);

                //供应商
                if (tableSupplier != null && tableSupplier.Rows.Count > 0)
                {
                    foreach (DataRow row in tableSupplier.Rows)
                    {
                        bussIDs += row["ID"] + ",";
                    }
                }

                //客户
                if (tableClient != null && tableClient.Rows.Count > 0)
                {
                    foreach (DataRow row in tableClient.Rows)
                    {
                        bussIDs += row["ID"] + ",";
                    }
                }

                //物流
                if (tableLogistics != null && tableLogistics.Rows.Count > 0)
                {
                    foreach (DataRow row in tableLogistics.Rows)
                    {
                        bussIDs += row["ID"] + ",";
                    }
                }

                if (string.IsNullOrEmpty(bussIDs))
                {
                    return " (  -1 ) ";
                }

                return "( " + bussIDs.Substring(0, bussIDs.Length - 1) + " )";
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return "( -1 )";
            }
        }

        /// <summary>
        /// 获取仓库ID权限
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public string GetStorageByUserID(string EmployeeId)
        {
            string bussIDs = "";
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    return "( -1 )";
                }
                if (EmployeeId == "1")
                {
                    return "";
                }
                List<DbParameter> parameters = new List<DbParameter>();
                parameters.Add(QueryService.AddInParameter("@UserID", SqlDbType.Int, 0, EmployeeId));
                //执行存储过程
                DataTable table = QueryService.ExecuteDataTable("[dbo].[proc_Authority_UserStorage]", parameters);

                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bussIDs += row["ID"] + ",";
                    }
                    return "( " + bussIDs.Substring(0, bussIDs.Length - 1) + " )";
                }
                else
                {
                    return "( -1 )";
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return "( -1 )";
            }
        }


        public string GetAddDate(string strdate)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(strdate);
                return dt.AddDays(1).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return strdate;
            }
        }

        public string GetDate(string strdate)
        {
            try
            {
                if (string.IsNullOrEmpty(strdate) || strdate == "1900/1/1 0:00:00" || strdate == "1900-01-01 0:00:00")
                {
                    return "";
                }
                return DataTypeConvert.DateTimeToString(strdate, DTFormate.SHORT_EN_US) == "1900-01-01" ? "" : DataTypeConvert.DateTimeToString(strdate, DTFormate.SHORT_EN_US);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return "";

        }

        public DateTime GetDateTime(string strData)
        {
            try
            {
                if (string.IsNullOrEmpty(strData))
                {
                    return DataTypeConvert.ToDateTime("1900/1/1 0:00:00", DTFormate.SHORT_EN_US);// Convert.ToDateTime();
                }
                return DataTypeConvert.ToDateTime(strData, DTFormate.SHORT_EN_US);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return DataTypeConvert.ToDateTime("1900/1/1 0:00:00", DTFormate.SHORT_EN_US);
        }

        public bool IsEnterprise(int cid)
        {
            if (cid == -1)
            {
                return false;
            }
            return true;
        }
       
        /// <summary>
        /// 小数转换
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public decimal GetDecimal(decimal number)
        {
            try
            {
                return decimal.Round(number, 2);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return 0;
        }

        /// <summary>
        /// 小数转换
        /// </summary>
        /// <param name="number"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public decimal GetDecimal(decimal number, int t)
        {
            try
            {
                return decimal.Round(number, t);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return 0;
        }

        /// <summary>
        /// 字符转换数字
        /// </summary>
        /// <returns></returns>
        public decimal GetDecimalByString(string number, int t)
        {
            try
            {
                if (string.IsNullOrEmpty(number))
                {
                    return 0;
                }
                return decimal.Round(DataTypeConvert.ToDecimal(number), t);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
            }
            return 0;
        }

        /// <summary>
        /// 解密码
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string GetPwd(string Password)
        {
            try
            {
                return CryptoFactory.Create(CrytoType.TripleDES).Decrypt(Password);
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return "";
            }
        }
    }
}