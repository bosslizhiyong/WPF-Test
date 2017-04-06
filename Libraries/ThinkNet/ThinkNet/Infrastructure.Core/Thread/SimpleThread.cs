using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 简单线程描述
    /// </summary>
    [Serializable]
    public class SimpleThread
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 线程编号
        /// </summary>
        private string _ThreadCode = "";
        /// <summary>
        /// 线程名称
        /// </summary>
        private string _ThreadName = "";
        /// <summary>
        /// 线程类型
        /// </summary>
        private string _ThreadType="";
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _BeginTime = DateTime.Now;
        /// <summary>
        /// 运行时间
        /// </summary>
        private string _RunTimes="";
        /// <summary>
        /// 剩余时间
        /// </summary>
        private string _RemainTime="";
        /// <summary>
        /// 忽略数
        /// </summary>
        private int _IgnoreCount = 0;
        /// <summary>
        /// 成功数
        /// </summary>
        private int _SucceedCount = 0;
        /// <summary>
        /// 失败数
        /// </summary>
        private int _FailedCount=0;
        /// <summary>
        /// 优先级名称(如:普通)
        /// </summary>
        private string _PriorityName = "";
        /// <summary>
        /// 状态名称(如:运行,停止)
        /// </summary>
        private string _StatusName = "";
        /// <summary>
        /// 状态(如:1-运行,2-停止)
        /// </summary>
        private int _Status = 0;
        /// <summary>
        /// 结果描述
        /// </summary>
        private string _Message = "";
        /// <summary>
        /// 线程(System.Threading.Thread)的实例
        /// </summary>
        private Thread _objThread = null;
        /// <summary>
        /// 线程执行实例
        /// </summary>
        private IThreadExecuteService _objThreadExecuteService = null;

        /// <summary>
        /// 执行类型(0-持续执行,1-定时执行)
        /// </summary>
        private int _ExecuteType = 0;
        /// <summary>
        /// 定时类型(0-每天,1-每月)
        /// </summary>
        private int _TimerType = 0;
        /// <summary>
        /// ExecuteType如果是Continue,该值是间隔执行时间,单位秒
        /// ExecuteType如果是timer,该值是定时执行的时间(如果TimerType是Day,该值是小时:分钟,如果TimerType是Month,该值是日&小时:分钟)
        /// </summary>
        private string _Time = "";

        /// <summary>
        /// 数据库连接名称
        /// </summary>
        private string _ConnectionStringName = "";
        #endregion

        #region 属    性
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        /// <summary>
        /// 线程编号
        /// </summary>
        public string ThreadCode
        {
            get { return this._ThreadCode; }
            set { this._ThreadCode = value; }
        }
        /// <summary>
        /// 线程名称
        /// </summary>
        public string ThreadName
        {
            get { return this._ThreadName; }
            set { this._ThreadName = value; }
        }
        /// <summary>
        /// 线程类型
        /// </summary>
        public string ThreadType
        {
            get { return this._ThreadType; }
            set { this._ThreadType = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return this._BeginTime; }
            set { this._BeginTime = value; }
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        public string RunTimes
        {
            get { return this._RunTimes; }
            set { this._RunTimes = value; }
        }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public string RemainTime
        {
            get { return this._RemainTime; }
            set { this._RemainTime = value; }
        }
        /// <summary>
        /// 忽略数
        /// </summary>
        public int IgnoreCount
        {
            get { return this._IgnoreCount; }
            set { this._IgnoreCount = value; }
        }
        /// <summary>
        /// 成功数
        /// </summary>
        public int SucceedCount
        {
            get { return this._SucceedCount; }
            set { this._SucceedCount = value; }
        }
        /// <summary>
        /// 失败数
        /// </summary>
        public int FailedCount
        {
            get { return this._FailedCount; }
            set { this._FailedCount = value; }
        }
        /// <summary>
        /// 优先级名称(如:普通)
        /// </summary>
        public string PriorityName
        {
            get { return this._PriorityName; }
            set { this._PriorityName = value; }
        }
        /// <summary>
        /// 状态名称(如:运行,停止)
        /// </summary>
        public string StatusName
        {
            get { return this._StatusName; }
            set { this._StatusName = value; }
        }
        /// <summary>
        /// 状态(如:1-运行,2-停止)
        /// </summary>
        public int Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        /// <summary>
        /// 结果描述
        /// </summary>
        public string Message
        {
            get { return this._Message; }
            set { this._Message = value; }
        }
        /// <summary>
        /// 线程(System.Threading.Thread)的实例
        /// </summary>
        public Thread ObjThread
        {
            get { return _objThread; }
            set { _objThread = value; }
        }
        /// <summary>
        /// 线程执行实例
        /// </summary>
        public IThreadExecuteService ObjThreadExecuteService
        {
            get { return _objThreadExecuteService; }
            set { _objThreadExecuteService = value; }
        }

        /// <summary>
        /// 执行类型(0-持续执行,1-定时执行)
        /// </summary>
        public int ExecuteType
        {
            get { return this._ExecuteType; }
            set { this._ExecuteType = value; }
        }
        /// <summary>
        /// 定时类型(0-每天,1-每月,2-每周)
        /// </summary>
        public int TimerType
        {
            get { return this._TimerType; }
            set { this._TimerType = value; }
        }
        /// <summary>
        /// ExecuteType如果是Continue,该值是间隔执行时间,单位秒
        /// ExecuteType如果是timer,该值是定时执行的时间(如果TimerType是Day,该值是小时:分钟,如果TimerType是Month,该值是日-小时:分钟,如果TimerType是Week,该值是星期-小时:分钟)
        /// </summary>
        public string Time
        {
            get { return this._Time; }
            set { this._Time = value; }
        }

        /// <summary>
        /// 数据库连接名称
        /// </summary>
        public string ConnectionStringName
        {
            get { return this._ConnectionStringName; }
            set { this._ConnectionStringName = value; }
        }
        #endregion
    }
}
