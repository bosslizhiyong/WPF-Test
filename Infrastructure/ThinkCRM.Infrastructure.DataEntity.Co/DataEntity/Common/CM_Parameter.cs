// 作者：汪洪斌
// 日期：2014/12/6 13:00:45
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 系统参数表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Parameter : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 参数编号
        /// </summary>
        private string _ParameterCode = "";
        /// <summary>
        /// 参数名称,如：是否打印到达日期？
        /// </summary>
        private string _ParameterName = "";
        /// <summary>
        /// 参数值(或值集合),如果是值集合用json数据格式
        /// </summary>
        private string _ParameterValues = "";
        /// <summary>
        /// 值集合标识,默认为0,如果ParameterValues字段为值集合,则为1
        /// </summary>
        private int _ValuesFlag = 0;
        /// <summary>
        /// 编辑标识,此参数是否允许编辑，0是可编辑，1是不可编辑
        /// </summary>
        private int _EditFlag = 0;
        /// <summary>
        /// 参数默认值,如果ParameterValues字段不为值集合则此值等于ParameterValues字段值,否则为值集合中的其中一个值
        /// </summary>
        private string _DefaultValue = "";
        /// <summary>
        /// 参数描述,如：是否要在账单上打印到达日期？
        /// </summary>
        private string _ParameterDesc = "";
        /// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
        private int _ApplicationID = 0;
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
        /// 所属参数ID,在整个系统中的ID(第一次使用授权时产生的参数ID)
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
        /// 参数编号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数编号")]
        public string ParameterCode
        {
            get { return this._ParameterCode; }
            set { this._ParameterCode = value; }
        }
        /// <summary>
        /// 参数名称,如：是否打印到达日期？
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数名称,如：是否打印到达日期？")]
        public string ParameterName
        {
            get { return this._ParameterName; }
            set { this._ParameterName = value; }
        }
        /// <summary>
        /// 参数值(或值集合),如果是值集合用json数据格式
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数值(或值集合),如果是值集合用json数据格式")]
        public string ParameterValues
        {
            get { return this._ParameterValues; }
            set { this._ParameterValues = value; }
        }
        /// <summary>
        /// 值集合标识,默认为0,如果ParameterValues字段为值集合,则为1
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("值集合标识,默认为0,如果ParameterValues字段为值集合,则为1")]
        public int ValuesFlag
        {
            get { return this._ValuesFlag; }
            set { this._ValuesFlag = value; }
        }
        /// <summary>
        /// 编辑标识,此参数是否允许编辑，0是可编辑，1是不可编辑
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("编辑标识,此参数是否允许编辑，0是可编辑，1是不可编辑")]
        public int EditFlag
        {
            get { return this._EditFlag; }
            set { this._EditFlag = value; }
        }
        /// <summary>
        /// 参数默认值,如果ParameterValues字段不为值集合则此值等于ParameterValues字段值,否则为值集合中的其中一个值
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数默认值,如果ParameterValues字段不为值集合则此值等于ParameterValues字段值,否则为值集合中的其中一个值")]
        public string DefaultValue
        {
            get { return this._DefaultValue; }
            set { this._DefaultValue = value; }
        }
        /// <summary>
        /// 参数描述,如：是否要在账单上打印到达日期？
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("参数描述,如：是否要在账单上打印到达日期？")]
        public string ParameterDesc
        {
            get { return this._ParameterDesc; }
            set { this._ParameterDesc = value; }
        }
        /// <summary>
        /// 应用程序(关联应用程序表)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("应用程序(关联应用程序表)")]
        public int ApplicationID
        {
            get { return this._ApplicationID; }
            set { this._ApplicationID = value; }
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
        /// 所属参数ID,在整个系统中的ID(第一次使用授权时产生的参数ID)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("所属参数ID,在整个系统中的ID(第一次使用授权时产生的参数ID)")]
        public int TheID
        {
            get { return this._TheID; }
            set { this._TheID = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 系统参数表
        /// </summary>
        public CM_Parameter()
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