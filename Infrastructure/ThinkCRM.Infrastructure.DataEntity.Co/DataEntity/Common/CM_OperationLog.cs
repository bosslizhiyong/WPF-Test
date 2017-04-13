// 作者：汪洪斌
// 日期：2015/3/3 10:35:48
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 操作日志表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_OperationLog:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 操作人ID号，即用户ID号
        /// </summary>
		private int _UserID=0;
		/// <summary>
        /// 操作人编号，即用户编号
        /// </summary>
		private string _UserCode="";
		/// <summary>
        /// 操作人名称，即用户名称
        /// </summary>
		private string _UserName="";
		/// <summary>
        /// 真实姓名
        /// </summary>
		private string _TrueName="";
		/// <summary>
        /// 模块ID号
        /// </summary>
		private int _ModuleID=0;
		/// <summary>
        /// 功能ID号
        /// </summary>
		private int _FunctionID=0;
		/// <summary>
        /// 操作内容
        /// </summary>
		private string _Content="";
		/// <summary>
        /// 操作结果
        /// </summary>
		private string _Result="";
		/// <summary>
        /// 操作时间
        /// </summary>
		private DateTime _OperateTime=DateTime.Now;
		/// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
		private int _ApplicationID=0;
		/// <summary>
        /// 数据
        /// </summary>
		private string _Data="";
		#endregion
		
		#region 属    性
		/// <summary>
        /// 主键
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("主键")]
		public int ID
		{
			get {return this._ID;}
			set {this._ID = value;}
		}
		/// <summary>
        /// 操作人ID号，即用户ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作人ID号，即用户ID号")]
		public int UserID
		{
			get {return this._UserID;}
			set {this._UserID = value;}
		}
		/// <summary>
        /// 操作人编号，即用户编号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作人编号，即用户编号")]
		public string UserCode
		{
			get {return this._UserCode;}
			set {this._UserCode = value;}
		}
		/// <summary>
        /// 操作人名称，即用户名称
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作人名称，即用户名称")]
		public string UserName
		{
			get {return this._UserName;}
			set {this._UserName = value;}
		}
		/// <summary>
        /// 真实姓名
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("真实姓名")]
		public string TrueName
		{
			get {return this._TrueName;}
			set {this._TrueName = value;}
		}
		/// <summary>
        /// 模块ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("模块ID号")]
		public int ModuleID
		{
			get {return this._ModuleID;}
			set {this._ModuleID = value;}
		}
		/// <summary>
        /// 功能ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("功能ID号")]
		public int FunctionID
		{
			get {return this._FunctionID;}
			set {this._FunctionID = value;}
		}
		/// <summary>
        /// 操作内容
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作内容")]
		public string Content
		{
			get {return this._Content;}
			set {this._Content = value;}
		}
		/// <summary>
        /// 操作结果
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作结果")]
		public string Result
		{
			get {return this._Result;}
			set {this._Result = value;}
		}
		/// <summary>
        /// 操作时间
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("操作时间")]
		public DateTime OperateTime
		{
			get {return this._OperateTime;}
			set {this._OperateTime = value;}
		}
		/// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("应用程序(关联应用程序表)")]
		public int ApplicationID
		{
			get {return this._ApplicationID;}
			set {this._ApplicationID = value;}
		}
		/// <summary>
        /// 数据
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("数据")]
		public string Data
		{
			get {return this._Data;}
			set {this._Data = value;}
		}
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 操作日志表
		/// </summary>
		public CM_OperationLog()
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


