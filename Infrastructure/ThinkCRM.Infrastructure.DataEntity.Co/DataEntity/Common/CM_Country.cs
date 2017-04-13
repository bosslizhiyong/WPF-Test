// 作者：汪洪斌
// 日期：2014/12/6 12:58:38
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 国家表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Country : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 国家编号(可以使用国际区号)
        /// </summary>
        private string _CountryCode = "";
        /// <summary>
        /// 国家名称
        /// </summary>
        private string _CountryName = "";
        /// <summary>
        /// 名称缩写(如:CHI)
        /// </summary>
        private string _CountryInit = "";
        /// <summary>
        /// 所属洲际(1-亚洲,2-欧洲,3-非洲,4-北美洲,5-南美洲,6-大洋洲)
        /// </summary>
        private int _ContinentID = 0;
        /// <summary>
        /// 序号
        /// </summary>
        private int _Sequence = 0;
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
        /// 国家编号(可以使用国际区号)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("国家编号(可以使用国际区号)")]
        public string CountryCode
        {
            get { return this._CountryCode; }
            set { this._CountryCode = value; }
        }
        /// <summary>
        /// 国家名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("国家名称")]
        public string CountryName
        {
            get { return this._CountryName; }
            set { this._CountryName = value; }
        }
        /// <summary>
        /// 名称缩写(如:CHI)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("名称缩写(如:CHI)")]
        public string CountryInit
        {
            get { return this._CountryInit; }
            set { this._CountryInit = value; }
        }
        /// <summary>
        /// 所属洲际(1-亚洲,2-欧洲,3-非洲,4-北美洲,5-南美洲,6-大洋洲)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("所属洲际(1-亚洲,2-欧洲,3-非洲,4-北美洲,5-南美洲,6-大洋洲)")]
        public int ContinentID
        {
            get { return this._ContinentID; }
            set { this._ContinentID = value; }
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
        /// 国家表
        /// </summary>
        public CM_Country()
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
