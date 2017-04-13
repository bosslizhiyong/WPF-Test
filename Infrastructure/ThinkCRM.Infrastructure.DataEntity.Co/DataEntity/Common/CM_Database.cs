// 作者：汪洪斌
// 日期：2016-12-21 11:29:18
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
	/// <summary>
	/// 数据库表
	/// </summary>
    [System.Runtime.Serialization.DataContract]
	public class CM_Database:DataEntityBase
	{
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 数据库服务器
        /// </summary>
        private string _Server = "";
        /// <summary>
        /// 数据库名
        /// </summary>
        private string _Database = "";
        /// <summary>
        /// 登录名
        /// </summary>
        private string _UserId = "";
        /// <summary>
        /// 密码
        /// </summary>
        private string _Password = "";
        /// <summary>
        /// 数据库状态( -4-配置不存在,-3-连接失败,-2-未创建,-1-未初始化,0-正常)
        /// </summary>
        private int _DatabaseStatus = 0;
        /// <summary>
        /// 数据库类型(0-主要,1-备份)
        /// </summary>
        private int _DatabaseType = 0;
        /// <summary>
        /// 哪个库的备份(DatabaseType为备份时不为空)
        /// </summary>
        private int _FromID = 0;
        /// <summary>
        /// 是否必需(系统必需要有一个数据库才能运行起来)
        /// </summary>
        private bool _IsRequired;
        /// <summary>
        /// 是否默认(预留)
        /// </summary>
        private bool _IsDefault;
        /// <summary>
        /// 中文名称
        /// </summary>
        private string _CnName = "";
        /// <summary>
        /// 连接名称(对应配置文件的连接名称)
        /// </summary>
        private string _ConnName = "";
        /// <summary>
        /// 企业编号
        /// </summary>
        private string _EnterpriseCode = "";
        /// <summary>
        /// 描述
        /// </summary>
        private string _Description = "";
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
        [System.Runtime.Serialization.DataMember, DisplayName("主键")]
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        /// <summary>
        /// 数据库服务器
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("数据库服务器")]
        public string Server
        {
            get { return this._Server; }
            set { this._Server = value; }
        }
        /// <summary>
        /// 数据库名
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("数据库名")]
        public string Database
        {
            get { return this._Database; }
            set { this._Database = value; }
        }
        /// <summary>
        /// 登录名
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("登录名")]
        public string UserId
        {
            get { return this._UserId; }
            set { this._UserId = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("密码")]
        public string Password
        {
            get { return this._Password; }
            set { this._Password = value; }
        }
        /// <summary>
        /// 数据库状态( -4-配置不存在,-3-连接失败,-2-未创建,-1-未初始化,0-正常)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("数据库状态")]
        public int DatabaseStatus
        {
            get { return this._DatabaseStatus; }
            set { this._DatabaseStatus = value; }
        }
        /// <summary>
        /// 数据库类型(0-主要,1-备份)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("数据库类型")]
        public int DatabaseType
        {
            get { return this._DatabaseType; }
            set { this._DatabaseType = value; }
        }
        /// <summary>
        /// 哪个库的备份(DatabaseType为备份时不为空)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("哪个库的备份")]
        public int FromID
        {
            get { return this._FromID; }
            set { this._FromID = value; }
        }
        /// <summary>
        /// 是否必需(系统必需要有一个数据库才能运行起来)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("是否必需")]
        public bool IsRequired
        {
            get { return this._IsRequired; }
            set { this._IsRequired = value; }
        }
        /// <summary>
        /// 是否默认(预留)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("是否默认")]
        public bool IsDefault
        {
            get { return this._IsDefault; }
            set { this._IsDefault = value; }
        }
        /// <summary>
        /// 中文名称
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("中文名称")]
        public string CnName
        {
            get { return this._CnName; }
            set { this._CnName = value; }
        }
        /// <summary>
        /// 连接名称(对应配置文件的连接名称)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("连接名称")]
        public string ConnName
        {
            get { return this._ConnName; }
            set { this._ConnName = value; }
        }
        /// <summary>
        /// 企业编号
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("企业编号")]
        public string EnterpriseCode
        {
            get { return this._EnterpriseCode; }
            set { this._EnterpriseCode = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("描述")]
        public string Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("状态")]
        public int Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("创建人")]
        public string CreateUser
        {
            get { return this._CreateUser; }
            set { this._CreateUser = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("创建时间")]
        public DateTime CreateTime
        {
            get { return this._CreateTime; }
            set { this._CreateTime = value; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("最后修改人")]
        public string LastModifyUser
        {
            get { return this._LastModifyUser; }
            set { this._LastModifyUser = value; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("最后修改时间")]
        public DateTime LastModifyTime
        {
            get { return this._LastModifyTime; }
            set { this._LastModifyTime = value; }
        }
        #endregion
		
		#region 构造函数
		/// <summary>
		/// 数据库表
		/// </summary>
		public CM_Database()
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


