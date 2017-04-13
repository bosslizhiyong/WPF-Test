// 作者：汪洪斌
// 日期：2016-03-18 9:31:09
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 线程表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Thread:DataEntityBase
	{
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 编号(唯一)
        /// </summary>
        private string _Code = "";
        /// <summary>
        /// 名称
        /// </summary>
        private string _Name = "";
        /// <summary>
        /// 执行类型(Continue或Timer,即持续执行或定时执行)
        /// </summary>
        private string _ExecuteType = "";
        /// <summary>
        /// 定时类型(Day或Month,即每天或每月,此值只有在ExecuteType设为Timer时有效)
        /// </summary>
        private string _TimerType = "";
        /// <summary>
        /// 时间(如果ExecuteType为Continue,该值是间隔执行时间,单位秒;如果ExecuteType为Timer,该值是定时执行的时间,同时如果TimerType是Day,该值是(小时:分钟),如果TimerType是Month,该值是(日-小时:分钟))
        /// </summary>
        private string _Time = "";
        /// <summary>
        /// 类库
        /// </summary>
        private string _Class = "";
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        private int _Status = 0;
        #endregion

        #region 属    性
        /// <summary>
        /// 主键
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("主键")]
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        /// <summary>
        /// 编号(唯一)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("编号(唯一)")]
        public string Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("名称")]
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
        /// <summary>
        /// 执行类型(Continue或Timer,即持续执行或定时执行)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("执行类型(Continue或Timer,即持续执行或定时执行)")]
        public string ExecuteType
        {
            get { return this._ExecuteType; }
            set { this._ExecuteType = value; }
        }
        /// <summary>
        /// 定时类型(Day或Month,即每天或每月,此值只有在ExecuteType设为Timer时有效)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("定时类型(Day或Month,即每天或每月,此值只有在ExecuteType设为Timer时有效)")]
        public string TimerType
        {
            get { return this._TimerType; }
            set { this._TimerType = value; }
        }
        /// <summary>
        /// 时间(如果ExecuteType为Continue,该值是间隔执行时间,单位秒;如果ExecuteType为Timer,该值是定时执行的时间,同时如果TimerType是Day,该值是(小时:分钟),如果TimerType是Month,该值是(日-小时:分钟))
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("时间(如果ExecuteType为Continue,该值是间隔执行时间,单位秒;如果ExecuteType为Timer,该值是定时执行的时间,同时如果TimerType是Day,该值是(小时:分钟),如果TimerType是Month,该值是(日-小时:分钟))")]
        public string Time
        {
            get { return this._Time; }
            set { this._Time = value; }
        }
        /// <summary>
        /// 类库
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("类库")]
        public string Class
        {
            get { return this._Class; }
            set { this._Class = value; }
        }
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("状态(0-正常,1-删除)")]
        public int Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 线程表
        /// </summary>
        public CM_Thread()
        {
            PrimaryKey = "ID";
            ConnectionStringsName = ConnectionStrings.ConnCRMCo;
            IsAutoID = true;
        }
        #endregion

        #region 其他属性
        public override object KeyID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = (int)value;
            }
        }
        #endregion
	}
}


