using System;
using ThinkNet.Component;

namespace WCFWeb.Co.ApiHost
{
    public class ApiBase
    {
        #region 属    性

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

        #region 基本方法

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

        #endregion 基本方法
    }
}