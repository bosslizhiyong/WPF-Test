// 作者：汪洪斌
// 日期：2015/11/19 16:33:25
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
    /// 打印模板表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_PrintTemplate:DataEntityBase
	{
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 模板名称
        /// </summary>
        private string _TemplateName = "";
        /// <summary>
        /// 原始文件名
        /// </summary>
        private string _FileName = "";
        /// <summary>
        /// 文件相对路径
        /// </summary>
        private string _FilePath = "";
        /// <summary>
        /// 保存文件名
        /// </summary>
        private string _FileSaveName = "";
        /// <summary>
        /// 文件扩展名
        /// </summary>
        private string _FileExtension = "";
        /// <summary>
        /// 模板类型(如:SalesOrder)
        /// </summary>
        private string _TemplateType = "";
        /// <summary>
        /// 是否默认
        /// </summary>
        private bool _IsDefault;
        /// <summary>
        /// 序号
        /// </summary>
        private int _Sequence;
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        private int _Status = 0;
        /// <summary>
        /// 创建人
        /// </summary>
        private string _CreateUser = "";
        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _CreateTime = DateTime.Now;
        /// <summary>
        /// 最后修改人
        /// </summary>
        private string _LastModifyUser = "";
        /// <summary>
        /// 最后修改时间
        /// </summary>
        private DateTime _LastModifyTime = DateTime.Now;
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
        /// 模板名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("模板名称")]
        public string TemplateName
        {
            get { return this._TemplateName; }
            set { this._TemplateName = value; }
        }
        /// <summary>
        /// 原始文件名
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("原始文件名")]
        public string FileName
        {
            get { return this._FileName; }
            set { this._FileName = value; }
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("文件相对路径")]
        public string FilePath
        {
            get { return this._FilePath; }
            set { this._FilePath = value; }
        }
        /// <summary>
        /// 保存文件名
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("保存文件名")]
        public string FileSaveName
        {
            get { return this._FileSaveName; }
            set { this._FileSaveName = value; }
        }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("文件扩展名")]
        public string FileExtension
        {
            get { return this._FileExtension; }
            set { this._FileExtension = value; }
        }
        /// <summary>
        /// 模板类型(如:SalesOrder)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("模板类型(如:SalesOrder)")]
        public string TemplateType
        {
            get { return this._TemplateType; }
            set { this._TemplateType = value; }
        }
        /// <summary>
        /// 是否默认
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("是否默认")]
        public bool IsDefault
        {
            get { return this._IsDefault; }
            set { this._IsDefault = value; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("序号")]
        public int Sequence
        {
            get { return this._Sequence; }
            set { this._Sequence = value; }
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
        /// <summary>
        /// 创建人
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("创建人")]
        public string CreateUser
        {
            get { return this._CreateUser; }
            set { this._CreateUser = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("创建时间")]
        public DateTime CreateTime
        {
            get { return this._CreateTime; }
            set { this._CreateTime = value; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("最后修改人")]
        public string LastModifyUser
        {
            get { return this._LastModifyUser; }
            set { this._LastModifyUser = value; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("最后修改时间")]
        public DateTime LastModifyTime
        {
            get { return this._LastModifyTime; }
            set { this._LastModifyTime = value; }
        }
        #endregion
		
		#region 构造函数
		/// <summary>
        /// 打印模板表
		/// </summary>
		public CM_PrintTemplate()
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


