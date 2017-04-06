using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThinkNet.Command.Core;
using ThinkNet.Infrastructure.Core;
using ThinkNet.Component;
using ThinkNet.Query.Core;
using ThinkNet.Utility;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 线程执行服务抽象基类
    /// </summary>
    public abstract class ThreadExecuteService:IThreadExecuteService
    {
        #region 字    段
        ///// <summary>
        ///// 线程要处理的数据类型(-1:无处理任何类型;0:集合类型)
        ///// </summary>
        //private int _handleType = -1;

        /// <summary>
        /// 查询参数
        /// </summary>
        protected QueryParamater _mQueryParamater = null;
        /// <summary>
        /// 简单结果描述
        /// </summary>
        protected SimpleResult _mSimpleResult = null;
        /// <summary>
        /// 信息处理类
        /// </summary>
        protected IMessageHandler _mMessageHandler = null;
        #endregion

        #region 属    性
        /// <summary>
        /// 线程ID号
        /// </summary>
        public int ThreadID { get; set; }
        /// <summary>
        /// 线程
        /// </summary>
        protected SimpleThread MThread
        {
            get
            {
                return ThreadHandler.GetThread(ThreadID);
            }
        }

        /// <summary>
        /// 命令总线
        /// </summary>
        protected ICommandBus CommandBus
        {
            get
            {
                ICommandBus commandBus = ObjectContainer.Resolve<ICommandBus>();
                if (!string.IsNullOrEmpty(ConnectionStringsName))//不为空时，使用外部设置的数据库
                {
                    commandBus.ExternalConnectionStringsName = "Conn" + ConnectionStringsName;
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
                if (!string.IsNullOrEmpty(ConnectionStringsName))//不为空时，使用外部设置的数据库
                {
                    dynamicQuery.ExternalConnectionStringsName = "ConnQuery" + ConnectionStringsName;
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

        /// <summary>
        /// 数据库连接
        /// </summary>
        protected string ConnectionStringsName { get; private set; }
        /// <summary>
        /// 线程要处理的数据类型(-1:无处理任何类型;0:集合类型)
        /// </summary>
        protected abstract int HandleType { get; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected ThreadExecuteService()
        {
            
        }
        ///// <summary>
        ///// 默认构造函数
        ///// <param name="handleType">线程要处理的数据类型(-1:无处理任何类型;0:集合类型)</param>
        ///// </summary>
        //protected ThreadExecuteService(int handleType)
        //{
        //    //_handleType = handleType;
        //    _mQueryParamater = QueryParamater.Create();
        //    _mSimpleResult = new SimpleResult();
        //}
        #endregion

        #region 基本方法

        #region IThreadExecuteService成员
        /// <summary>
        /// 执行方法
        /// </summary>
        public virtual void Execute()
        {
            //当前线程实例
            SimpleThread mThread = this.MThread;
            if (mThread == null)
            {
                WriteExceptionLog("当前线程实例为空!");
                return;
            }

            //TimeSpan ts = DateTime.Now - mThread.BeginTime;
            try
            {
                int interval = 0;
                int continuceTeme = 0;
                while (true)
                {
                    ConnectionStringsName = mThread.ConnectionStringName;
                    if (mThread.ExecuteType == (int)ExecuteTypes.Continue)//持续执行
                    {
                        #region 持续执行
                        //间隔时间
                        continuceTeme = DataTypeConvert.ToInt32(mThread.Time);//秒
                        if (continuceTeme <= 0)
                        {
                            continuceTeme = 30;
                        }
                        interval = continuceTeme * 1000;//毫秒
                        //如果停止则退出
                        if (mThread.Status == (int)ThreadStates.Stopped)
                        {
                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            break;
                        }
                        try
                        {
                            //执行线程数据
                            ExecuteHandleData(mThread);

                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                        }
                        catch (Exception ex)
                        {
                            WriteExceptionLog(ex);
                            mThread.StatusName = "停止";
                            mThread.Status = (int)ThreadStates.Stopped;
                            mThread.Message ="异常:"+ex.Message;

                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            break;
                        }

                        Thread.Sleep(interval);//等待下次执行
                        #endregion
                    }
                    else if (mThread.ExecuteType == (int)ExecuteTypes.Timer)//定时执行
                    {
                        #region 定时执行
                        if (mThread.TimerType == (int)TimerTypes.Day)//每天
                        {
                            #region 每天
                            //定时时间是否异常
                            DateTime executeTimer = DataTypeConvert.ToDateTime(mThread.Time);//小时:分钟
                            if (executeTimer == DateTime.MinValue)
                            {
                                mThread.Message = "执行时间(小时:分钟,如:00:30)异常!";
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }
                            try
                            {
                                //如果停止则退出
                                if (mThread.Status == (int)ThreadStates.Stopped)
                                {
                                    //ts = DateTime.Now - mThread.BeginTime;
                                    //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                    break;
                                }
                                //小时，分钟
                                if (DateTime.Now.Hour == executeTimer.Hour && DateTime.Now.Minute == executeTimer.Minute && DateTime.Now.Second == executeTimer.Second)
                                {
                                    //执行线程数据
                                    ExecuteHandleData(mThread);
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteExceptionLog(ex);
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;
                                mThread.Message = "异常:" + ex.Message;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }

                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            Thread.Sleep(1000);
                            #endregion
                        }
                        else if (mThread.TimerType == (int)TimerTypes.Month)//每月
                        {
                            #region 每月
                            //定时时间是否异常
                            string[] arrTime = DataTypeConvert.ToArray(mThread.Time, '-');//日-小时:分钟
                            if (arrTime == null || arrTime.Length != 2)
                            {
                                mThread.Message = "执行时间(日-小时:分钟,如:1-00:30)异常!";
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }
                            DateTime executeTimer = DataTypeConvert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + "-" + arrTime[0] + " " + arrTime[1]);//小时:分钟
                            if (executeTimer == DateTime.MinValue)
                            {
                                mThread.Message = "执行时间(日-小时:分钟,如:1-00:30)异常!";
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }
                            try
                            {
                                //如果停止则退出
                                if (mThread.Status == (int)ThreadStates.Stopped)
                                {
                                    //ts = DateTime.Now - mThread.BeginTime;
                                    //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                    break;
                                }
                                //日，小时，分钟
                                if (DateTime.Now.Day == executeTimer.Day && DateTime.Now.Hour == executeTimer.Hour && DateTime.Now.Minute == executeTimer.Minute && DateTime.Now.Second == executeTimer.Second)
                                {
                                    //执行线程数据
                                    ExecuteHandleData(mThread);
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteExceptionLog(ex);
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;
                                mThread.Message = "异常:" + ex.Message;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }

                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            Thread.Sleep(1000);
                            #endregion
                        }
                        else if (mThread.TimerType == (int)TimerTypes.Week)//每周
                        {
                            #region 每周
                            //定时时间是否异常
                            string[] arrTime = DataTypeConvert.ToArray(mThread.Time, '-');//星期-小时:分钟
                            if (arrTime == null || arrTime.Length != 2)
                            {
                                mThread.Message = "执行时间(星期-小时:分钟,如:星期日-00:30)异常!";
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }
                            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
                            DateTime executeTimer = DataTypeConvert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + arrTime[1]);//小时:分钟
                            if (!weekdays.Contains(arrTime[0]) || string.IsNullOrEmpty(week) || executeTimer == DateTime.MinValue)
                            {
                                mThread.Message = "执行时间(星期-小时:分钟,如:星期日-00:30)异常!";
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }
                            try
                            {
                                //如果停止则退出
                                if (mThread.Status == (int)ThreadStates.Stopped)
                                {
                                    //ts = DateTime.Now - mThread.BeginTime;
                                    //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                    break;
                                }
                                //星期，小时，分钟
                                if (week == arrTime[0] && DateTime.Now.Hour == executeTimer.Hour && DateTime.Now.Minute == executeTimer.Minute && DateTime.Now.Second == executeTimer.Second)
                                {
                                    //执行线程数据
                                    ExecuteHandleData(mThread);
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteExceptionLog(ex);
                                mThread.StatusName = "停止";
                                mThread.Status = (int)ThreadStates.Stopped;
                                mThread.Message = "异常:" + ex.Message;

                                //ts = DateTime.Now - mThread.BeginTime;
                                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                                break;
                            }

                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            Thread.Sleep(1000);
                            #endregion
                        }
                        else
                        {
                            #region 未知类型
                            mThread.Message = "定时执行类型未知!";
                            //ts = DateTime.Now - mThread.BeginTime;
                            //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                            Thread.Sleep(1000);
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region 未知类型
                        mThread.Message = "执行类型未知!";
                        //ts = DateTime.Now - mThread.BeginTime;
                        //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                        Thread.Sleep(1000);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                mThread.StatusName = "停止";
                mThread.Status = (int)ThreadStates.Stopped;
                mThread.Message = "异常:" + ex.Message;
                //ts = DateTime.Now - mThread.BeginTime;
                //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
            }
        }
        /// <summary>
        /// 执行一次
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="connectionStringName"></param>
        public virtual SimpleResult ExecuteOnce(object parameter, string connectionStringName)
        {
            try
            {
                ConnectionStringsName = connectionStringName;
                switch (HandleType)
                {
                    case -1:
                        //执行代码
                        Task task1 = new Task(new Action(() =>
                        {
                            HandleData(null);
                        }));
                        task1.Start();
                        break;
                    case 0:
                        //获取所有要发送的数据
                        Task task0 = new Task(new Action(() =>
                        {
                            DataTable dtData = GetDataTable(true, parameter);
                            HandleData(dtData);
                        }));
                        task0.Start();
                        break;
                    default:
                        return new SimpleResult(false, "未知的处理类型!");
                }
                
                return new SimpleResult(true, "执行成功!");
            }
            catch (Exception ex)
            {
                WriteExceptionLog(ex);
                return new SimpleResult(false, ex.Message);
            }
        }

        /// <summary>
        /// 获取线程需要处理的数据集合
        /// </summary>
        /// <param name="isExecuteOnce">是否执行一次</param>
        /// <param name="parameter">一次执行的参数</param>
        /// <returns></returns>
        protected abstract DataTable GetDataTable(bool isExecuteOnce,object parameter);
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="data">线程需要处理的数据</param>
        protected abstract void HandleData(object data);
        #endregion

        #endregion

        #region 其他方法
        /// <summary>
        /// 执行线程数据
        /// </summary>
        /// <param name="mThread">当前线程</param>
        private void ExecuteHandleData(SimpleThread mThread)
        {
            DataTable dtData = null;
            //TimeSpan ts = DateTime.Now - mThread.BeginTime;
            switch (HandleType)
            {
                case -1:
                    //执行代码
                    HandleData(null);
                    break;
                case 0:
                    //获取所有要发送的数据
                    dtData = GetDataTable(false, null);
                    while (dtData != null && dtData.Rows.Count > 0)
                    {
                        //如果停止则跳出
                        if (mThread.Status == (int)ThreadStates.Stopped)
                        {
                            break;
                        }
                        //执行代码
                        HandleData(dtData);

                        dtData = GetDataTable(false, null);

                        //ts = DateTime.Now - mThread.BeginTime;
                        //mThread.RunTimes = DataTypeConvert.ToDateTime(ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds).ToString("HH:mm:ss");
                    }
                    break;
                default:
                    WriteExceptionLog("未知的处理类型!");
                    mThread.StatusName = "停止";
                    mThread.Status = (int)ThreadStates.Stopped;
                    mThread.Message = "未知的处理类型";
                    break;
            }
        }

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
        #endregion
    }
}