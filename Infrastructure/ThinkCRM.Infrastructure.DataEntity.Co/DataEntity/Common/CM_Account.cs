// 作者：汪洪斌
// 日期：2015/1/21 17:44:25
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 收付款账户表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Account:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 账户编号
        /// </summary>
		private string _DefineAccountCode="";
		/// <summary>
        /// 账户名称
        /// </summary>
		private string _DefineAccountName="";
		/// <summary>
        /// 账户类型(1-现金账户，2-银行账户，3-虚拟账户)
        /// </summary>
		private int _DefineAccountType=0;
		/// <summary>
        /// 币种(关联币种表)
        /// </summary>
		private int _CurrencyID=0;
		/// <summary>
        /// 开户银行(关联银行表)
        /// </summary>
		private int _BankID=0;
		/// <summary>
        /// 开户名
        /// </summary>
		private string _AccountName="";
		/// <summary>
        /// 银行账号
        /// </summary>
		private string _AccountCode="";
        /// <summary>
        /// 开户支行
        /// </summary>
        private string _BankBranch = "";
		/// <summary>
        /// 是否私人账号
        /// </summary>
		private bool _IsPrivate;
		/// <summary>
        /// 是否默认账户
        /// </summary>
		private bool _IsDefault;
		/// <summary>
        /// 序号
        /// </summary>
		private int _Sequence=0;
		/// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
		private int _Status=0;
		/// <summary>
        /// 备注
        /// </summary>
		private string _AccountDesc="";
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
        /// 账户编号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("账户编号")]
		public string DefineAccountCode
		{
			get {return this._DefineAccountCode;}
			set {this._DefineAccountCode = value;}
		}
		/// <summary>
        /// 账户名称
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("账户名称")]
		public string DefineAccountName
		{
			get {return this._DefineAccountName;}
			set {this._DefineAccountName = value;}
		}
		/// <summary>
        /// 账户类型(1-现金账户，2-银行账户，3-虚拟账户)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("账户类型(1-现金账户，2-银行账户，3-虚拟账户)")]
		public int DefineAccountType
		{
			get {return this._DefineAccountType;}
			set {this._DefineAccountType = value;}
		}
		/// <summary>
        /// 币种(关联币种表)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("币种(关联币种表)")]
		public int CurrencyID
		{
			get {return this._CurrencyID;}
			set {this._CurrencyID = value;}
		}
		/// <summary>
        /// 开户银行(关联银行表)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("开户银行(关联银行表)")]
		public int BankID
		{
			get {return this._BankID;}
			set {this._BankID = value;}
		}
		/// <summary>
        /// 开户名
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("开户名")]
		public string AccountName
		{
			get {return this._AccountName;}
			set {this._AccountName = value;}
		}
		/// <summary>
        /// 银行账号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("银行账号")]
		public string AccountCode
		{
			get {return this._AccountCode;}
			set {this._AccountCode = value;}
		}
        /// <summary>
        /// 开户支行
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("开户支行")]
        public string BankBranch
        {
            get { return this._BankBranch; }
            set { this._BankBranch = value; }
        }
		/// <summary>
        /// 是否私人账号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("是否私人账号")]
		public bool IsPrivate
		{
			get {return this._IsPrivate;}
			set {this._IsPrivate = value;}
		}
		/// <summary>
        /// 是否默认账户
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("是否默认账户")]
		public bool IsDefault
		{
			get {return this._IsDefault;}
			set {this._IsDefault = value;}
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
        /// 备注
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("备注")]
		public string AccountDesc
		{
			get {return this._AccountDesc;}
			set {this._AccountDesc = value;}
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
		/// 收付款账户表
		/// </summary>
		public CM_Account()
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


