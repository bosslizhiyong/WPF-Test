// 作者：汪洪斌
// 日期：2014/12/6 13:01:58
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 用户表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_User : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 用户编号
        /// </summary>
        private string _UserCode = "";
        /// <summary>
        /// 用户名称
        /// </summary>
        private string _UserName = "";
        /// <summary>
        /// 用户密码
        /// </summary>
        private string _Password = "";
        /// <summary>
        /// 用户状态(0-注册，1-激活，2-冻结)
        /// </summary>
        private int _UserStatus = 0;
        /// <summary>
        /// 入口标识,指用户可以进入哪个应用程序(关联应用程序表：2-企业门户,3-往来单位门户)
        /// </summary>
        private int _ApplicationID = 0;
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        private DateTime _LastLoginTime = DateTime.Now;
        /// <summary>
        /// 最后退出时间
        /// </summary>
        private DateTime _LastLogoutTime = DateTime.Now;
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
        /// <summary>
        /// 所属用户ID,在整个系统中的ID(第一次使用授权时产生的管理员ID)
        /// </summary>
        private int _TheID = 0;
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
        /// 用户编号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("用户编号")]
        public string UserCode
        {
            get { return this._UserCode; }
            set { this._UserCode = value; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("用户名称")]
        public string UserName
        {
            get { return this._UserName; }
            set { this._UserName = value; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("用户密码")]
        public string Password
        {
            get { return this._Password; }
            set { this._Password = value; }
        }
        /// <summary>
        /// 用户状态(0-注册，1-激活，2-冻结)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("用户状态(0-注册，1-激活，2-冻结)")]
        public int UserStatus
        {
            get { return this._UserStatus; }
            set { this._UserStatus = value; }
        }
        /// <summary>
        /// 入口标识,指用户可以进入哪个应用程序(关联应用程序表：2-企业门户,3-往来单位门户)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("入口标识,指用户可以进入哪个应用程序(关联应用程序表：2-企业门户,3-往来单位门户)")]
        public int ApplicationID
        {
            get { return this._ApplicationID; }
            set { this._ApplicationID = value; }
        }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("最后登陆时间")]
        public DateTime LastLoginTime
        {
            get { return this._LastLoginTime; }
            set { this._LastLoginTime = value; }
        }
        /// <summary>
        /// 最后退出时间
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("最后退出时间")]
        public DateTime LastLogoutTime
        {
            get { return this._LastLogoutTime; }
            set { this._LastLogoutTime = value; }
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
        /// <summary>
        /// 所属用户ID,在整个系统中的ID(第一次使用授权时产生的管理员ID)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("所属用户ID,在整个系统中的ID(第一次使用授权时产生的管理员ID)")]
        public int TheID
        {
            get { return this._TheID; }
            set { this._TheID = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 用户表
        /// </summary>
        public CM_User()
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