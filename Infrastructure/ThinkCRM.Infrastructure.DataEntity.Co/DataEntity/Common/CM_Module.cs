// 作者：汪洪斌
// 日期：2014/12/6 12:59:37
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 系统模块表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Module : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 模块编号
        /// </summary>
        private string _ModuleCode = "";
        /// <summary>
        /// 模块名称
        /// </summary>
        private string _ModuleName = "";
        /// <summary>
        /// 上级模块
        /// </summary>
        private int _ParentID = 0;
        /// <summary>
        /// URL
        /// </summary>
        private string _ModuleURL = "";
        /// <summary>
        /// 窗体名称
        /// </summary>
        private string _ModuleForm = "";
        /// <summary>
        /// 是否MDI窗体(针对winform)
        /// </summary>
        private bool _IsMDIForm;
        /// <summary>
        /// 是否开始分组(针对winform)
        /// </summary>
        private bool _IsBeginGroup;
        /// <summary>
        /// 是否菜单项
        /// </summary>
        private bool _IsMenu;
        /// <summary>
        /// 层级
        /// </summary>
        private int _ModuleLevel = 0;
        /// <summary>
        /// 类型(0-页面,1-节点)
        /// </summary>
        private int _ModuleType = 0;
        /// <summary>
        /// 参数
        /// </summary>
        private string _Parameter = "";
        /// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
        private int _ApplicationID = 0;
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
        /// <summary>
        /// 所属模块ID,在整个系统中的ID(第一次使用授权时产生的模块ID)
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
        /// 模块编号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("模块编号")]
        public string ModuleCode
        {
            get { return this._ModuleCode; }
            set { this._ModuleCode = value; }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("模块名称")]
        public string ModuleName
        {
            get { return this._ModuleName; }
            set { this._ModuleName = value; }
        }
        /// <summary>
        /// 上级模块
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("上级模块")]
        public int ParentID
        {
            get { return this._ParentID; }
            set { this._ParentID = value; }
        }
        /// <summary>
        /// URL
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("URL")]
        public string ModuleURL
        {
            get { return this._ModuleURL; }
            set { this._ModuleURL = value; }
        }
        /// <summary>
        /// 窗体名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("窗体名称")]
        public string ModuleForm
        {
            get { return this._ModuleForm; }
            set { this._ModuleForm = value; }
        }
        /// <summary>
        /// 是否MDI窗体(针对winform)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("是否MDI窗体(针对winform)")]
        public bool IsMDIForm
        {
            get { return this._IsMDIForm; }
            set { this._IsMDIForm = value; }
        }
        /// <summary>
        /// 是否开始分组(针对winform)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("是否开始分组(针对winform)")]
        public bool IsBeginGroup
        {
            get { return this._IsBeginGroup; }
            set { this._IsBeginGroup = value; }
        }
        /// <summary>
        /// 是否菜单项
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("是否菜单项")]
        public bool IsMenu
        {
            get { return this._IsMenu; }
            set { this._IsMenu = value; }
        }
        /// <summary>
        /// 层级
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("层级")]
        public int ModuleLevel
        {
            get { return this._ModuleLevel; }
            set { this._ModuleLevel = value; }
        }
        /// <summary>
        /// 类型(0-页面,1-节点)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("类型")]
        public int ModuleType
        {
            get { return this._ModuleType; }
            set { this._ModuleType = value; }
        }
        /// <summary>
        /// 参数
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数")]
        public string Parameter
        {
            get { return this._Parameter; }
            set { this._Parameter = value; }
        }
        /// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序")]
        public int ApplicationID
        {
            get { return this._ApplicationID; }
            set { this._ApplicationID = value; }
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
        [System.Runtime.Serialization.DataMember,DisplayName("状态")]
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
        /// 所属模块ID,在整个系统中的ID(第一次使用授权时产生的模块ID)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("所属模块ID,在整个系统中的ID(第一次使用授权时产生的模块ID)")]
        public int TheID
        {
            get { return this._TheID; }
            set { this._TheID = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 系统模块表
        /// </summary>
        public CM_Module()
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