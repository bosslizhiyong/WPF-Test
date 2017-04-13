// 作者：汪洪斌
// 日期：2015-12-01 11:47:22
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 个性化定制明细表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_IndividualizeLine:DataEntityBase
	{
		#region 字    段
		/// <summary>
        /// 主键
        /// </summary>
		private int _ID=0;
		/// <summary>
        /// 定制ID号
        /// </summary>
		private int _IndividualizeID=0;
		/// <summary>
        /// 关联位置(窗体名称->控件名称),没有位置关联的就为空
        /// </summary>
		private string _RelationPosition="";
		/// <summary>
        /// 关联ID号(如模块的ID号)
        /// </summary>
		private int _RelationID=0;
		/// <summary>
        /// 关联名称,可为空
        /// </summary>
		private string _RelationName="";
		/// <summary>
        /// 关联文本,可为空
        /// </summary>
		private string _RelationText="";
		/// <summary>
        /// 定制值(可以是数值,字符串),可为空
        /// </summary>
		private string _IndividualizeValue="";
		/// <summary>
        /// 定制顺序
        /// </summary>
		private int _IndividualizeSequence=0;
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
        /// 定制ID号
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制ID号")]
		public int IndividualizeID
		{
			get {return this._IndividualizeID;}
			set {this._IndividualizeID = value;}
		}
		/// <summary>
        /// 关联位置(窗体名称->控件名称),没有位置关联的就为空
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("关联位置(窗体名称->控件名称),没有位置关联的就为空")]
		public string RelationPosition
		{
			get {return this._RelationPosition;}
			set {this._RelationPosition = value;}
		}
		/// <summary>
        /// 关联ID号(如模块的ID号)
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("关联ID号(如模块的ID号)")]
		public int RelationID
		{
			get {return this._RelationID;}
			set {this._RelationID = value;}
		}
		/// <summary>
        /// 关联名称,可为空
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("关联名称,可为空")]
		public string RelationName
		{
			get {return this._RelationName;}
			set {this._RelationName = value;}
		}
		/// <summary>
        /// 关联文本,可为空
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("关联文本,可为空")]
		public string RelationText
		{
			get {return this._RelationText;}
			set {this._RelationText = value;}
		}
		/// <summary>
        /// 定制值(可以是数值,字符串),可为空
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制值(可以是数值,字符串),可为空")]
		public string IndividualizeValue
		{
			get {return this._IndividualizeValue;}
			set {this._IndividualizeValue = value;}
		}
		/// <summary>
        /// 定制顺序
        /// </summary>
		[System.Runtime.Serialization.DataMember,DisplayName("定制顺序")]
		public int IndividualizeSequence
		{
			get {return this._IndividualizeSequence;}
			set {this._IndividualizeSequence = value;}
		}
		#endregion
		
		#region 构造函数
		/// <summary>
		/// 个性化定制明细表
		/// </summary>
		public CM_IndividualizeLine()
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


