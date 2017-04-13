// 作者：汪洪斌
// 日期：2016-11-04 15:06:27
using System;
using System.ComponentModel;
using ThinkNet.DataEntity.Core;

namespace ThinkCRM.Infrastructure.DataEntity.Co
{
    /// <summary>
    /// 推送日志表
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public class CM_PushLog : DataEntityBase
    {
        #region 字    段
        /// <summary>
        /// 主键
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// 推送单号( 提醒单号 推送单号)
        /// </summary>
        private int _PushOrder = 0;
        /// <summary>
        /// 操作人ID号，即员工ID号
        /// </summary>
        private int _EmployeeID = 0;
        /// <summary>
        /// 操作人编号，即员工编号
        /// </summary>
        private string _EmployeeCode = "";
        /// <summary>
        /// 操作人名称，即员工名称
        /// </summary>
        private string _EmployeeName = "";
        /// <summary>
        /// ( 7七种单据, 新品 促销)
        /// </summary>
        private int _PushType = 0;
        /// <summary>
        /// 推送人(电话号码)
        /// </summary>
        private string _Phone = "";
        /// <summary>
        /// 操作内容
        /// </summary>
        private string _Content = "";
        /// <summary>
        /// 操作结果
        /// </summary>
        private bool _Result;
        /// <summary>
        /// 对内推送结果(0 失败1 成功 2 没有结果)
        /// </summary>
        private int _ResultIn = 0;
        /// <summary>
        /// 对外推送结果(0 失败1 成功 2 没有结果)
        /// </summary>
        private int _ResultOut = 0;
        /// <summary>
        /// 对内数据(Json)
        /// </summary>
        private string _DataIn = "";
        /// <summary>
        /// 对外数据(Json)
        /// </summary>
        private string _DataOut = "";
        /// <summary>
        /// 操作时间
        /// </summary>
        private DateTime _OperateTime = DateTime.Now;
        /// <summary>
        /// 推送地址
        /// </summary>
        private string _PushUrl = "";
        /// <summary>
        /// 状态
        /// </summary>
        private int _Status = 0;
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
        /// 推送单号( 提醒单号 推送单号)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("推送单号")]
        public int PushOrder
        {
            get { return this._PushOrder; }
            set { this._PushOrder = value; }
        }
        /// <summary>
        /// 操作人ID号，即员工ID号
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作人ID号，即员工ID号")]
        public int EmployeeID
        {
            get { return this._EmployeeID; }
            set { this._EmployeeID = value; }
        }
        /// <summary>
        /// 操作人编号，即员工编号
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作人编号，即员工编号")]
        public string EmployeeCode
        {
            get { return this._EmployeeCode; }
            set { this._EmployeeCode = value; }
        }
        /// <summary>
        /// 操作人名称，即员工名称
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作人名称，即员工名称")]
        public string EmployeeName
        {
            get { return this._EmployeeName; }
            set { this._EmployeeName = value; }
        }
        /// <summary>
        /// ( 7七种单据, 新品 促销)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("( 7七种单据, 新品 促销)")]
        public int PushType
        {
            get { return this._PushType; }
            set { this._PushType = value; }
        }
        /// <summary>
        /// 推送人(电话号码)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("推送人")]
        public string Phone
        {
            get { return this._Phone; }
            set { this._Phone = value; }
        }
        /// <summary>
        /// 操作内容
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作内容")]
        public string Content
        {
            get { return this._Content; }
            set { this._Content = value; }
        }
        /// <summary>
        /// 操作结果
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作结果")]
        public bool Result
        {
            get { return this._Result; }
            set { this._Result = value; }
        }
        /// <summary>
        /// 对内推送结果(0 失败1 成功 2 没有结果)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("对内推送结果")]
        public int ResultIn
        {
            get { return this._ResultIn; }
            set { this._ResultIn = value; }
        }
        /// <summary>
        /// 对外推送结果(0 失败1 成功 2 没有结果)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("对外推送结果")]
        public int ResultOut
        {
            get { return this._ResultOut; }
            set { this._ResultOut = value; }
        }
        /// <summary>
        /// 对内数据(Json)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("对内数据")]
        public string DataIn
        {
            get { return this._DataIn; }
            set { this._DataIn = value; }
        }
        /// <summary>
        /// 对外数据(Json)
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("对外数据")]
        public string DataOut
        {
            get { return this._DataOut; }
            set { this._DataOut = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("操作时间")]
        public DateTime OperateTime
        {
            get { return this._OperateTime; }
            set { this._OperateTime = value; }
        }
        /// <summary>
        /// 推送地址
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("推送地址")]
        public string PushUrl
        {
            get { return this._PushUrl; }
            set { this._PushUrl = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        [System.Runtime.Serialization.DataMember, DisplayName("状态")]
        public int Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 推送日志表
        /// </summary>
        public CM_PushLog()
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


