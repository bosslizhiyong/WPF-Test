// 作者：汪洪斌
// 日期：2015/2/19 12:19:47
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 功能表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Functions:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 功能编号
        /// </summary>
		private string _FunctionCode="";
		/// <summary>
        /// 功能名称
        /// </summary>
		private string _FunctionName="";
		/// <summary>
        /// 功能描述
        /// </summary>
		private string _FunctionDesc="";
		/// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
		private int _ApplicationID=0;
		/// <summary>
        /// 序号
        /// </summary>
		private int _Sequence=0;
		/// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
		private int _Status=0;
		/// <summary>
        /// 创建人
        /// </summary>
		private string _CreateUser="";
		/// <summary>
        /// 创建时间
        /// </summary>
		private DateTime _CreateTime=DateTime.Now;
		/// <summary>
        /// 最后修改人
        /// </summary>
		private string _LastModifyUser="";
		/// <summary>
        /// 最后修改时间
        /// </summary>
		private DateTime _LastModifyTime=DateTime.Now;
		/// <summary>
        /// 所属功能ID,在整个系统中的ID(第一次使用授权时产生的功能ID)
        /// </summary>
		private int _TheID=0;
		/// <summary>
        /// 按钮名称
        /// </summary>
		private string _FunctionButton="";
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
        /// 功能编号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("功能编号")]
		public string FunctionCode
		{
			get {return this._FunctionCode;}
			set {this._FunctionCode = value;}
		}
		/// <summary>
        /// 功能名称
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("功能名称")]
		public string FunctionName
		{
			get {return this._FunctionName;}
			set {this._FunctionName = value;}
		}
		/// <summary>
        /// 功能描述
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("功能描述")]
		public string FunctionDesc
		{
			get {return this._FunctionDesc;}
			set {this._FunctionDesc = value;}
		}
		/// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("应用程序")]
		public int ApplicationID
		{
			get {return this._ApplicationID;}
			set {this._ApplicationID = value;}
		}
		/// <summary>
        /// 序号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("序号")]
		public int Sequence
		{
			get {return this._Sequence;}
			set {this._Sequence = value;}
		}
		/// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("状态(0-正常,1-删除)")]
		public int Status
		{
			get {return this._Status;}
			set {this._Status = value;}
		}
		/// <summary>
        /// 创建人
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("创建人")]
		public string CreateUser
		{
			get {return this._CreateUser;}
			set {this._CreateUser = value;}
		}
		/// <summary>
        /// 创建时间
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("创建时间")]
		public DateTime CreateTime
		{
			get {return this._CreateTime;}
			set {this._CreateTime = value;}
		}
		/// <summary>
        /// 最后修改人
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("最后修改人")]
		public string LastModifyUser
		{
			get {return this._LastModifyUser;}
			set {this._LastModifyUser = value;}
		}
		/// <summary>
        /// 最后修改时间
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("最后修改时间")]
		public DateTime LastModifyTime
		{
			get {return this._LastModifyTime;}
			set {this._LastModifyTime = value;}
		}
		/// <summary>
        /// 所属功能ID,在整个系统中的ID(第一次使用授权时产生的功能ID)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("所属功能ID,在整个系统中的ID(第一次使用授权时产生的功能ID)")]
		public int TheID
		{
			get {return this._TheID;}
			set {this._TheID = value;}
		}
        /// <summary>
        /// 按钮名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("按钮名称")]
        public string FunctionButton
        {
            get { return this._FunctionButton; }
            set { this._FunctionButton = value; }
        }
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 功能表
		/// </summary>
		public CM_Functions()
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


