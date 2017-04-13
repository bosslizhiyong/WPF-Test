// 作者：汪洪斌
// 日期：2014/12/6 13:00:07
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 民族表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_Nation : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 民族编号
        /// </summary>
        private string _NationCode = "";
        /// <summary>
        /// 民族名称
        /// </summary>
        private string _NationName = "";
        /// <summary>
        /// 序号
        /// </summary>
        private int _Sequence = 0;
        /// <summary>
        /// 状态(0-正常,1-删除)
        /// </summary>
        private int _Status = 0;
        /// <summary>
        /// 民族描述
        /// </summary>
        private string _NationDesc = "";
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
        /// 民族编号
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("民族编号")]
        public string NationCode
        {
            get { return this._NationCode; }
            set { this._NationCode = value; }
        }
        /// <summary>
        /// 民族名称
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("民族名称")]
        public string NationName
        {
            get { return this._NationName; }
            set { this._NationName = value; }
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
        /// 民族描述
        /// </summary>
        [System.Runtime.Serialization.DataMember,DisplayName("民族描述")]
        public string NationDesc
        {
            get { return this._NationDesc; }
            set { this._NationDesc = value; }
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
        /// 民族表
        /// </summary>
        public CM_Nation()
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