// 作者：汪洪斌
// 日期：2015-12-01 11:47:15
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 个性化定制表(用户可设置自己相关的习惯的功能或模块)
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Individualize:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 用户ID号(关联用户表),UserID为0时表示所有用户的定制
        /// </summary>
		private int _UserID=0;
		/// <summary>
        /// 定制类型(如:Tools,Pager,GridHeader)
        /// </summary>
		private string _IndividualizeType="";
		/// <summary>
        /// 定制值(此值不为空时定制值标识字段要设为1)
        /// </summary>
		private string _IndividualizeValue="";
		/// <summary>
        /// 定制值标识(定制值不为空时为1,否则为0)
        /// </summary>
		private int _IndividualizeValueFlag=0;
		/// <summary>
        /// 定制描述
        /// </summary>
		private string _IndividualizeDesc="";
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
        /// 用户ID号(关联用户表),UserID为0时表示所有用户的定制
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("用户ID号(关联用户表),UserID为0时表示所有用户的定制")]
		public int UserID
		{
			get {return this._UserID;}
			set {this._UserID = value;}
		}
		/// <summary>
        /// 定制类型(如:Tools,Pager,GridHeader)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制类型(如:Tools,Pager,GridHeader)")]
		public string IndividualizeType
		{
			get {return this._IndividualizeType;}
			set {this._IndividualizeType = value;}
		}
		/// <summary>
        /// 定制值(此值不为空时定制值标识字段要设为1)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制值(此值不为空时定制值标识字段要设为1)")]
		public string IndividualizeValue
		{
			get {return this._IndividualizeValue;}
			set {this._IndividualizeValue = value;}
		}
		/// <summary>
        /// 定制值标识(定制值不为空时为1,否则为0)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制值标识(定制值不为空时为1,否则为0)")]
		public int IndividualizeValueFlag
		{
			get {return this._IndividualizeValueFlag;}
			set {this._IndividualizeValueFlag = value;}
		}
		/// <summary>
        /// 定制描述
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制描述")]
		public string IndividualizeDesc
		{
			get {return this._IndividualizeDesc;}
			set {this._IndividualizeDesc = value;}
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
		/// 个性化定制表(用户可设置自己相关的习惯的功能或模块)
		/// </summary>
		public CM_Individualize()
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


