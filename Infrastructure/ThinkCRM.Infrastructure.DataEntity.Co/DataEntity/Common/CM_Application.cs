// 作者：汪洪斌
// 日期：2014/12/6 12:52:52
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 应用程序(门户)表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Application : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        private string _ApplicationName = "";
        /// <summary>
        /// 应用程序Logo
        /// </summary>
        private string _ApplicationLogo = "";
        /// <summary>
        /// 应用程序URL
        /// </summary>
        private string _ApplicationURL = "";
        /// <summary>
        /// 应用程序说明
        /// </summary>
        private string _ApplicationDesc = "";
        /// <summary>
        /// 序号
        /// </summary>
        private int _Sequence = 0;
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
        /// 应用程序名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序名称")]
        public string ApplicationName
        {
            get { return this._ApplicationName; }
            set { this._ApplicationName = value; }
        }
        /// <summary>
        /// 应用程序Logo
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序Logo")]
        public string ApplicationLogo
        {
            get { return this._ApplicationLogo; }
            set { this._ApplicationLogo = value; }
        }
        /// <summary>
        /// 应用程序URL
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序URL")]
        public string ApplicationURL
        {
            get { return this._ApplicationURL; }
            set { this._ApplicationURL = value; }
        }
        /// <summary>
        /// 应用程序说明
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序说明")]
        public string ApplicationDesc
        {
            get { return this._ApplicationDesc; }
            set { this._ApplicationDesc = value; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("序号")]
        public int Sequence
        {
            get { return this._Sequence; }
            set { this._Sequence = value; }
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
        /// 应用程序(门户)表
        /// </summary>
        public CM_Application()
        {
            PrimaryKey = "ID";
            ConnectionStringsName = ConnectionStrings.ConnCRMCo;
            IsAutoID = false;
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