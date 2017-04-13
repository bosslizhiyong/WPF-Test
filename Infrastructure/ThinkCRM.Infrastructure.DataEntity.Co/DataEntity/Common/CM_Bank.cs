// 作者：汪洪斌
// 日期：2015/1/21 17:44:25
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 银行表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Bank:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 银行编号
        /// </summary>
		private string _BankCode="";
		/// <summary>
        /// 银行名称
        /// </summary>
		private string _BankName="";
		/// <summary>
        /// 序号
        /// </summary>
		private int _Sequence=0;
		/// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
		private int _Status=0;
		/// <summary>
        /// 银行描述
        /// </summary>
		private string _BankDesc="";
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
        /// 银行编号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("银行编号")]
		public string BankCode
		{
			get {return this._BankCode;}
			set {this._BankCode = value;}
		}
		/// <summary>
        /// 银行名称
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("银行名称")]
		public string BankName
		{
			get {return this._BankName;}
			set {this._BankName = value;}
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
        /// 银行描述
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("银行描述")]
		public string BankDesc
		{
			get {return this._BankDesc;}
			set {this._BankDesc = value;}
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
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 银行表
		/// </summary>
		public CM_Bank()
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


