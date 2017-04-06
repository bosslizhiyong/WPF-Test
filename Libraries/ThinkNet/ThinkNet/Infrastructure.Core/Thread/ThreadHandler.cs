using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 线程处理类
    /// </summary>
    public class ThreadHandler
    {
        #region 字    段
        /// <summary>
        /// 简单线程池
        /// </summary>
        private static List<SimpleThread> _SimpleThreadPool = new List<SimpleThread>();
        #endregion

        #region 属    性
        /// <summary>
        /// 线程数量
        /// </summary>
        public static int ThreadCount
        {
            get { return _SimpleThreadPool.Count; }
        }
        /// <summary>
        /// 线程列表
        /// </summary>
        public static List<SimpleThread> ThreadList
        {
            get
            {
                if (_SimpleThreadPool == null)
                {
                    _SimpleThreadPool = new List<SimpleThread>();
                }
                return _SimpleThreadPool;
            }
        }
        #endregion

        #region 构造函数

        #endregion

        #region 基本方法
        /// <summary>
        /// 根据线程ID获取对应的线程实列
        /// </summary>
        /// <param name="id">线程ID</param>
        /// <returns></returns>
        public static SimpleThread GetThread(int id)
        {
            foreach (SimpleThread mThread in ThreadList)
            {
                if (mThread.ID == id)
                {
                    return mThread;
                }
            }
            return null;
        }
        /// <summary>
        /// 往线程池里添加线程
        /// </summary>
        /// <param name="threadName">线程名称</param>
        /// <returns></returns>
        public static int AddThread(string threadName)
        {
            SimpleThread mThread = new SimpleThread();
            mThread.ID = ThreadCount;
            mThread.ThreadName = threadName;
            mThread.ThreadType = "";
            mThread.BeginTime = DateTime.Now;
            mThread.RunTimes = "00:00:00";
            mThread.RemainTime = "00:00:00";
            mThread.IgnoreCount = 0;
            mThread.SucceedCount = 0;
            mThread.FailedCount = 0;
            mThread.PriorityName = "普通";
            mThread.StatusName = "运行";
            mThread.Status = (int)ThreadStates.Running;
            mThread.Message = "";
            mThread.ObjThread = null;
            ThreadList.Add(mThread);
            return ThreadCount - 1;
        }
        /// <summary>
        /// 往线程池里添加线程
        /// </summary>
        /// <param name="threadName">线程名称</param>
        /// <returns></returns>
        public static SimpleThread AddThreadObject(string threadName)
        {
            SimpleThread mThread = new SimpleThread();
            mThread.ID = ThreadCount;
            mThread.ThreadName = threadName;
            mThread.ThreadType = "";
            mThread.BeginTime = DateTime.Now;
            mThread.RunTimes = "00:00:00";
            mThread.RemainTime = "00:00:00";
            mThread.IgnoreCount = 0;
            mThread.SucceedCount = 0;
            mThread.FailedCount = 0;
            mThread.PriorityName = "普通";
            mThread.StatusName = "运行";
            mThread.Status = (int)ThreadStates.Running;
            mThread.Message = "";
            mThread.ObjThread = null;
            ThreadList.Add(mThread);
            return mThread;
        }
        /// <summary>
        /// 根据线程ID删除对应的线程实列
        /// </summary>
        /// <param name="id">线程ID</param>
        /// <returns></returns>
        public static bool Remove(int id)
        {
            foreach (SimpleThread mThread in ThreadList)
            {
                if (mThread.ID == id)
                {
                    if (mThread.ObjThread!=null)
                    {
                        mThread.ObjThread.Abort();
                    }
                    ThreadList.Remove(mThread);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 其他方法
        /// <summary>
        /// 加载xml文件中的线程配置到线程池中
        /// </summary>
        /// <param name="xmlFileName">xml文件的全路径(包含文件名称)</param>
        public static List<SimpleThread> LoadThreadFromXml(string xmlFileName)
        {
            if (!File.Exists(xmlFileName)) return null;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            string xpath = @"configuration/MultiThread";
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList == null || nodeList.Count <= 0) return null;

            string className = "";
            string nameSpace = "";
            Assembly assembly = null;

            int threadId = 0;//线程ID号
            string threaNname = "";//线程名称
            string executeType = "";
            string executeTypeText = "";//执行类型枚举(持续执行或定时执行)
            string timerType = "";//定时执行类型(每天、每月...)
            
            object obj = null;//线程执行对象
            System.Threading.ThreadStart threadStar = null;
            System.Threading.Thread mThread = null;
            SimpleThread mSimpleThread = null;
            foreach (XmlNode item in nodeList)
            {
                #region 创建执行实例
                if (item.Attributes["Class"] == null) continue;
                className = item.Attributes["Class"].Value;
                if (string.IsNullOrEmpty(className)) continue;
                //反射创建线程执行实例
                nameSpace = className.Substring(0, className.LastIndexOf("."));
                assembly = Assembly.Load(nameSpace);
                obj = assembly.CreateInstance(className);
                IThreadExecuteService target = obj as IThreadExecuteService;//线程执行实例
                if (target == null) continue;
                #endregion

                //创建线程
                threaNname = (item.Attributes["Code"] == null || item.Attributes["Code"].Value == "") ? className.Substring(className.LastIndexOf(".") + 1) : item.Attributes["Code"].Value;
                //threadId = AddThread(threaNname);//添加到线程池
                //简单线程描述
                mSimpleThread = AddThreadObject(threaNname);//添加到线程池
                threadId = mSimpleThread.ID;

                #region 执行实例赋值
                //线程编号
                mSimpleThread.ThreadCode = (item.Attributes["Code"] == null || item.Attributes["Code"].Value == "") ? className.Substring(className.LastIndexOf(".") + 1) : item.Attributes["Code"].Value;
                //执行类型
                executeType = (item.Attributes["ExecuteType"] == null) ? "Continue" : item.Attributes["ExecuteType"].Value;
                mSimpleThread.ExecuteType = (int)ExecuteTypes.Continue;
                executeTypeText = "持续";
                if (executeType == "Timer")
                {
                    mSimpleThread.ExecuteType = (int)ExecuteTypes.Timer;
                    executeTypeText = "定时";
                }
                //定时类型
                timerType = (item.Attributes["TimerType"] == null) ? "Day" : item.Attributes["TimerType"].Value;
                if (timerType == "Day")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Day;
                }
                else if (timerType == "Month")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Month;
                }
                else if (timerType == "Week")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Week;
                }
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    //定时是每天还是每月
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += " 每天";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        executeTypeText += " 每月";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        executeTypeText += " 每周";
                    }
                }
                //执行或等待时间
                mSimpleThread.Time = (item.Attributes["Time"] == null) ? "86400" : item.Attributes["Time"].Value;
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Continue)
                {
                    executeTypeText += " 每隔" + mSimpleThread.Time + "秒执行";
                }
                else if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += mSimpleThread.Time + "执行";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "号" + arrTime[1]+"执行";
                        }
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "" + arrTime[1] + "执行";
                        }
                    }
                }
                #endregion

                threadStar = new System.Threading.ThreadStart(target.Execute);
                mThread = new System.Threading.Thread(threadStar);//线程执行方法
                mThread.Priority = System.Threading.ThreadPriority.Lowest;
                mThread.IsBackground = true;
                
                mSimpleThread.ThreadType = executeTypeText;
                mSimpleThread.ObjThread = mThread;
                mSimpleThread.ObjThreadExecuteService = target;

                //更新线程执行实例的线程ID
                target.ThreadID = threadId;

                //线程启动
                mThread.Start();
            }

            return ThreadList;
        }
        /// <summary>
        /// 加载数据库中的线程配置到线程池中
        /// </summary>
        /// <param name="connectionStringName">数据连接名称</param>
        /// <param name="collenctions">线程集合</param>
        /// <returns></returns>
        public static List<SimpleThread> LoadThreadFromDatabase(string connectionStringName, IEnumerable<DataRow> collenctions)
        {
            if (collenctions == null || collenctions.Count() <= 0) return null;

            string className = "";
            string nameSpace = "";
            Assembly assembly = null;

            int threadId = 0;//线程ID号
            string threaNname = "";//线程名称
            string executeType = "";
            string executeTypeText = "";//执行类型枚举(持续执行或定时执行)
            string timerType = "";//定时执行类型(每天、每月...)

            object obj = null;//线程执行对象
            System.Threading.ThreadStart threadStar = null;
            System.Threading.Thread mThread = null;
            SimpleThread mSimpleThread = null;
            List<SimpleThread> list = new List<SimpleThread>();
            foreach (DataRow row in collenctions)
            {
                #region 创建执行实例
                if (row["Class"] == null || row["Class"]+""=="") continue;
                className = row["Class"] + "";
                //反射创建线程执行实例
                nameSpace = className.Substring(0, className.LastIndexOf("."));
                assembly = Assembly.Load(nameSpace);
                obj = assembly.CreateInstance(className);
                IThreadExecuteService target = obj as IThreadExecuteService;//线程执行实例
                if (target == null) continue;
                #endregion

                //创建线程
                threaNname = (row["Code"] == null || row["Code"] + "" == "") ? className.Substring(className.LastIndexOf(".") + 1) : row["Name"] + "";
                //threadId = AddThread(threaNname);//添加到线程池
                //简单线程描述
                mSimpleThread = AddThreadObject(threaNname);//添加到线程池
                threadId = mSimpleThread.ID;
                mSimpleThread.ConnectionStringName = connectionStringName;//多个数据库情况
                #region 执行实例赋值
                //线程编号
                mSimpleThread.ThreadCode = (row["Code"] == null || row["Code"] + "" == "") ? className.Substring(className.LastIndexOf(".") + 1) : row["Code"] + "";
                //执行类型
                executeType = (row["ExecuteType"] == null || row["ExecuteType"] + "" == "") ? "Continue" : row["ExecuteType"] + "";
                mSimpleThread.ExecuteType = (int)ExecuteTypes.Continue;
                executeTypeText = "持续";
                if (executeType == "Timer")
                {
                    mSimpleThread.ExecuteType = (int)ExecuteTypes.Timer;
                    executeTypeText = "定时";
                }
                //定时类型
                timerType = (row["TimerType"] == null || row["TimerType"] + "" == "") ? "Day" : row["TimerType"]+"";
                if (timerType == "Day")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Day;
                }
                else if (timerType == "Month")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Month;
                }
                else if (timerType == "Week")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Week;
                }
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    //定时是每天还是每月
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += " 每天";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        executeTypeText += " 每月";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        executeTypeText += " 每周";
                    }
                }
                //执行或等待时间
                mSimpleThread.Time = (row["Time"] == null || row["Time"] + "" == "") ? "86400" : row["Time"] + "";
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Continue)
                {
                    executeTypeText += " 每隔" + mSimpleThread.Time + "秒执行";
                }
                else if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += mSimpleThread.Time + "执行";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "号" + arrTime[1] + "执行";
                        }
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "" + arrTime[1] + "执行";
                        }
                    }
                }
                #endregion

                threadStar = new System.Threading.ThreadStart(target.Execute);
                mThread = new System.Threading.Thread(threadStar);//线程执行方法
                mThread.Priority = System.Threading.ThreadPriority.Lowest;
                mThread.IsBackground = true;

                mSimpleThread.ThreadType = executeTypeText;
                mSimpleThread.ObjThread = mThread;
                mSimpleThread.ObjThreadExecuteService = target;

                //更新线程执行实例的线程ID
                target.ThreadID = threadId;

                //线程启动
                mThread.Start();

                list.Add(mSimpleThread);
            }

            return list;
        }
        /// <summary>
        /// 加载数据库中的线程配置到线程池中
        /// </summary>
        /// <param name="connectionStringName">数据连接名称</param>
        /// <param name="dtData">线程集合</param>
        /// <returns></returns>
        public static List<SimpleThread> LoadThreadFromDatabase(string connectionStringName, DataTable dtData)
        {
            if (dtData == null || dtData.Rows.Count <= 0) return null;

            string className = "";
            string nameSpace = "";
            Assembly assembly = null;

            int threadId = 0;//线程ID号
            string threaNname = "";//线程名称
            string executeType = "";
            string executeTypeText = "";//执行类型枚举(持续执行或定时执行)
            string timerType = "";//定时执行类型(每天、每月...)

            object obj = null;//线程执行对象
            System.Threading.ThreadStart threadStar = null;
            System.Threading.Thread mThread = null;
            SimpleThread mSimpleThread = null;
            List<SimpleThread> list = new List<SimpleThread>();
            foreach (DataRow row in dtData.Rows)
            {
                #region 创建执行实例
                if (row["Class"] == null || row["Class"] + "" == "") continue;
                className = row["Class"] + "";
                //反射创建线程执行实例
                nameSpace = className.Substring(0, className.LastIndexOf("."));
                assembly = Assembly.Load(nameSpace);
                obj = assembly.CreateInstance(className);
                IThreadExecuteService target = obj as IThreadExecuteService;//线程执行实例
                if (target == null) continue;
                #endregion

                //创建线程
                threaNname = (row["Code"] == null || row["Code"] + "" == "") ? className.Substring(className.LastIndexOf(".") + 1) : row["Name"] + "";
                //threadId = AddThread(threaNname);//添加到线程池
                //简单线程描述
                mSimpleThread = AddThreadObject(threaNname);//添加到线程池
                threadId = mSimpleThread.ID;
                mSimpleThread.ConnectionStringName = connectionStringName;//多个数据库情况
                #region 执行实例赋值
                //线程编号
                mSimpleThread.ThreadCode = (row["Code"] == null || row["Code"] + "" == "") ? className.Substring(className.LastIndexOf(".") + 1) : row["Code"] + "";
                //执行类型
                executeType = (row["ExecuteType"] == null || row["ExecuteType"] + "" == "") ? "Continue" : row["ExecuteType"] + "";
                mSimpleThread.ExecuteType = (int)ExecuteTypes.Continue;
                executeTypeText = "持续";
                if (executeType == "Timer")
                {
                    mSimpleThread.ExecuteType = (int)ExecuteTypes.Timer;
                    executeTypeText = "定时";
                }
                //定时类型
                timerType = (row["TimerType"] == null || row["TimerType"] + "" == "") ? "Day" : row["TimerType"] + "";
                if (timerType == "Day")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Day;
                }
                else if (timerType == "Month")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Month;
                }
                else if (timerType == "Week")
                {
                    mSimpleThread.TimerType = (int)TimerTypes.Week;
                }
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    //定时是每天还是每月
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += " 每天";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        executeTypeText += " 每月";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        executeTypeText += " 每周";
                    }
                }
                //执行或等待时间
                mSimpleThread.Time = (row["Time"] == null || row["Time"] + "" == "") ? "86400" : row["Time"] + "";
                if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Continue)
                {
                    executeTypeText += " 每隔" + mSimpleThread.Time + "秒执行";
                }
                else if (mSimpleThread.ExecuteType == (int)ExecuteTypes.Timer)
                {
                    if (mSimpleThread.TimerType == (int)TimerTypes.Day)
                    {
                        executeTypeText += mSimpleThread.Time + "执行";
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Month)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "号" + arrTime[1] + "执行";
                        }
                    }
                    else if (mSimpleThread.TimerType == (int)TimerTypes.Week)
                    {
                        string[] arrTime = mSimpleThread.Time.Split('-');
                        if (arrTime != null && arrTime.Length == 2)
                        {
                            executeTypeText += arrTime[0] + "" + arrTime[1] + "执行";
                        }
                    }
                }
                #endregion

                threadStar = new System.Threading.ThreadStart(target.Execute);
                mThread = new System.Threading.Thread(threadStar);//线程执行方法
                mThread.Priority = System.Threading.ThreadPriority.Lowest;
                mThread.IsBackground = true;

                mSimpleThread.ThreadType = executeTypeText;
                mSimpleThread.ObjThread = mThread;
                mSimpleThread.ObjThreadExecuteService = target;

                //更新线程执行实例的线程ID
                target.ThreadID = threadId;

                //线程启动
                mThread.Start();

                list.Add(mSimpleThread);
            }

            return list;
        }
        #endregion
    }
}
