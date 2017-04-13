// 作者：汪洪斌
// 日期：2015/3/2 14:45:05
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 角色模块关系表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_RoleModule_Ref:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 角色ID号
        /// </summary>
		private int _RoleID=0;
		/// <summary>
        /// 模块ID号
        /// </summary>
		private int _ModuleID=0;
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
        /// 角色ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("角色ID号")]
		public int RoleID
		{
			get {return this._RoleID;}
			set {this._RoleID = value;}
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
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 角色模块关系表
		/// </summary>
		public CM_RoleModule_Ref()
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


