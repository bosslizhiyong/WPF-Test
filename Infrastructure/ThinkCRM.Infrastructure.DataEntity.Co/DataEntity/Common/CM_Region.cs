// 作者：汪洪斌
// 日期：2014/12/6 13:01:15
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 行政区域表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Region : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 区域编号
        /// </summary>
        private string _RegionCode = "";
        /// <summary>
        /// 区域名称
        /// </summary>
        private string _RegionName = "";
        /// <summary>
        /// 区域全称
        /// </summary>
        private string _RegionFullName = "";
        /// <summary>
        /// 区号
        /// </summary>
        private string _AreaCode = "";
        /// <summary>
        /// 上级区域
        /// </summary>
        private string _ParentCode = "";
        /// <summary>
        /// 层级
        /// </summary>
        private int _RegionLevel = 0;
        /// <summary>
        /// 区域类别（1-大区，2-省，3-市，4-区县）
        /// </summary>
        private int _RegionTypeID = 0;

        /// <summary>
        /// 是否默认(0-国家默认的,1手动添加的)
        /// </summary>
        private bool _IsDefault;
        /// <summary>
        /// 所属国家
        /// </summary>
        private string _CountryCode = "";
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
        /// 区域编号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("区域编号")]
        public string RegionCode
        {
            get { return this._RegionCode; }
            set { this._RegionCode = value; }
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("区域名称")]
        public string RegionName
        {
            get { return this._RegionName; }
            set { this._RegionName = value; }
        }
        /// <summary>
        /// 区域全称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("区域全称")]
        public string RegionFullName
        {
            get { return this._RegionFullName; }
            set { this._RegionFullName = value; }
        }
        /// <summary>
        /// 区号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("区号")]
        public string AreaCode
        {
            get { return this._AreaCode; }
            set { this._AreaCode = value; }
        }
        /// <summary>
        /// 上级区域
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("上级区域")]
        public string ParentCode
        {
            get { return this._ParentCode; }
            set { this._ParentCode = value; }
        }
        /// <summary>
        /// 层级
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("层级")]
        public int RegionLevel
        {
            get { return this._RegionLevel; }
            set { this._RegionLevel = value; }
        }
        /// <summary>
        /// 区域类别（1-大区，2-省，3-市，4-区县）
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("区域类别（1-大区，2-省，3-市，4-区县）")]
        public int RegionTypeID
        {
            get { return this._RegionTypeID; }
            set { this._RegionTypeID = value; }
        }
        /// <summary>
        /// 是否默认(0-国家默认的,1手动添加的)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("是否默认")]
        public bool IsDefault
        {
            get { return this._IsDefault; }
            set { this._IsDefault = value; }
        }
        /// <summary>
        /// 所属国家
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("所属国家")]
        public string CountryCode
        {
            get { return this._CountryCode; }
            set { this._CountryCode = value; }
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
        /// 行政区域表
        /// </summary>
        public CM_Region()
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