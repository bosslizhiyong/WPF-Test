// 作者：汪洪斌
// 日期：2016/12/17 17:04:49
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 打印模板表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_PrintNumber:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 订单ID
        /// </summary>
		private int _OrderID=0;
		/// <summary>
        /// 打印类型
        /// </summary>
		private int _PrintType=0;
		/// <summary>
        /// 打印次数
        /// </summary>
		private int _PrintNumber=0;
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
        /// 订单ID
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("订单ID")]
		public int OrderID
		{
			get {return this._OrderID;}
			set {this._OrderID = value;}
		}
		/// <summary>
        /// 打印类型
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("打印类型")]
		public int PrintType
		{
			get {return this._PrintType;}
			set {this._PrintType = value;}
		}
		/// <summary>
        /// 打印次数
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("打印次数")]
		public int PrintNumber
		{
			get {return this._PrintNumber;}
			set {this._PrintNumber = value;}
		}
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 打印模板表
		/// </summary>
		public CM_PrintNumber()
		{
			PrimaryKey = "ID";
            ConnectionStringsName = ConnectionStrings.ConnCRM;
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


