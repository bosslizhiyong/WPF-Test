// 作者：汪洪斌
// 日期：2014/12/16 14:15:30
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 下载文件表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_DownloadFile : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 文件分类(如：AuthorizeFile)
        /// </summary>
        private string _FileCategory = "";
        /// <summary>
        /// 上级ID号,关联相应的主表
        /// </summary>
        private int _ParentID = 0;
        /// <summary>
        /// 文件相对路径
        /// </summary>
        private string _FilePath = "";
        /// <summary>
        /// 文件类型(如:xls,txt,zip,rar)
        /// </summary>
        private string _FileType = "";
        /// <summary>
        /// 文件扩展名
        /// </summary>
        private string _FileExtension = "";
        /// <summary>
        /// 原始文件名
        /// </summary>
        private string _FileName = "";
        /// <summary>
        /// 保存文件名
        /// </summary>
        private string _FileSaveName = "";
        /// <summary>
        /// 用户ID号
        /// </summary>
        private int _UserID = 0;
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        private int _Status = 0;
        /// <summary>
        /// 文件描述
        /// </summary>
        private string _FileDesc = "";
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
        /// 文件分类(如：AuthorizeFile)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("文件分类(如：AuthorizeFile)")]
        public string FileCategory
        {
            get { return this._FileCategory; }
            set { this._FileCategory = value; }
        }
        /// <summary>
        /// 上级ID号,关联相应的主表
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("上级ID号,关联相应的主表")]
        public int ParentID
        {
            get { return this._ParentID; }
            set { this._ParentID = value; }
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
        /// 文件类型(如:xls,txt,zip,rar)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("文件类型(如:xls,txt,zip,rar)")]
        public string FileType
        {
            get { return this._FileType; }
            set { this._FileType = value; }
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
        /// 原始文件名
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("原始文件名")]
        public string FileName
        {
            get { return this._FileName; }
            set { this._FileName = value; }
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
        /// 用户ID号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("用户ID号")]
        public int UserID
        {
            get { return this._UserID; }
            set { this._UserID = value; }
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
        /// 文件描述
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("文件描述")]
        public string FileDesc
        {
            get { return this._FileDesc; }
            set { this._FileDesc = value; }
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
        /// 下载文件表
        /// </summary>
        public CM_DownloadFile()
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
