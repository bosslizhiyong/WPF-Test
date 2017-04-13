// 作者：汪洪斌
// 日期：2015/2/28 13:47:10
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 用户角色关系表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_UserRole_Ref:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 用户ID号
        /// </summary>
		private int _UserID=0;
		/// <summary>
        /// 角色ID号
        /// </summary>
		private int _RoleID=0;
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
        /// 用户ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("用户ID号")]
		public int UserID
		{
			get {return this._UserID;}
			set {this._UserID = value;}
		}
		/// <summary>
        /// 角色ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("角色ID号")]
		public int RoleID
		{
			get {return this._RoleID;}
			set {this._RoleID = value;}
		}
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 用户角色关系表
		/// </summary>
		public CM_UserRole_Ref()
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


