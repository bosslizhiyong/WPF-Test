// 作者：汪洪斌
// 日期：2014/12/17 16:47:38
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 流水号表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Sequence : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 编号(唯一)
        /// </summary>
        private string _Code = "";
        /// <summary>
        /// 名称
        /// </summary>
        private string _Name = "";
        /// <summary>
        /// 前缀
        /// </summary>
        private string _Prefix = "";
        /// <summary>
        /// 日期类型，可以为yyyy，yymm, yyyymm，yymmdd，yyyymmdd等,如果为空则为自增流水号
        /// </summary>
        private string _DateType = "";
        /// <summary>
        /// 中缀
        /// </summary>
        private string _Infix = "";
        /// <summary>
        /// 自增号长度
        /// </summary>
        private int _Length = 0;
        /// <summary>
        /// 后缀
        /// </summary>
        private string _Suffix = "";
        /// <summary>
        /// 当前最大日期值
        /// </summary>
        private string _MaxDate = "";
        /// <summary>
        /// 当前最大号值
        /// </summary>
        private int _MaxIndex = 0;
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
        /// 编号(唯一)
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("编号(唯一)")]
        public string Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("名称")]
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
        /// <summary>
        /// 前缀
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("前缀")]
        public string Prefix
        {
            get { return this._Prefix; }
            set { this._Prefix = value; }
        }
        /// <summary>
        /// 日期类型，可以为yyyy，yymm, yyyymm，yymmdd，yyyymmdd等,如果为空则为自增流水号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("日期类型，可以为yyyy，yymm, yyyymm，yymmdd，yyyymmdd等,如果为空则为自增流水号")]
        public string DateType
        {
            get { return this._DateType; }
            set { this._DateType = value; }
        }
        /// <summary>
        /// 中缀
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("中缀")]
        public string Infix
        {
            get { return this._Infix; }
            set { this._Infix = value; }
        }
        /// <summary>
        /// 自增号长度
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("自增号长度")]
        public int Length
        {
            get { return this._Length; }
            set { this._Length = value; }
        }
        /// <summary>
        /// 后缀
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("后缀")]
        public string Suffix
        {
            get { return this._Suffix; }
            set { this._Suffix = value; }
        }
        /// <summary>
        /// 当前最大日期值
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("当前最大日期值")]
        public string MaxDate
        {
            get { return this._MaxDate; }
            set { this._MaxDate = value; }
        }
        /// <summary>
        /// 当前最大号值
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("当前最大号值")]
        public int MaxIndex
        {
            get { return this._MaxIndex; }
            set { this._MaxIndex = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 流水号表
        /// </summary>
        public CM_Sequence()
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
