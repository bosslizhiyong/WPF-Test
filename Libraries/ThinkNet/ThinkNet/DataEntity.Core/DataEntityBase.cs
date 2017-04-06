using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.DataEntity.Core
{
    [Serializable]
    [DataContract]
    public partial class DataEntityBase
    {
        #region 字    段

        /// <summary>
        /// 主键是否自增长字段ID
        /// </summary>
        private bool _IsAutoID = true;
        /// <summary>
        /// 主健字段
        /// </summary>
        private string _PrimaryKey = String.Empty;
        /// <summary>
        /// 数据库连接名称
        /// </summary>
        private ConnectionStrings _ConnectionStringsName;
        /// <summary>
        /// 是否外部设置数据库
        /// </summary>
        private bool _IsExternalConnection = false;
        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        private string _ExternalConnectionStringsName = String.Empty;
        /// <summary>
        /// 实体动作
        /// </summary>
        private DataEntityActions _DataEntityAction = 0;

        /// <summary>
        /// 实体唯一标识值
        /// </summary>
        private object _KeyID = 0;

        #endregion

        #region 属    性

        /// <summary>
        /// 主键是否自增长字段ID
        /// </summary>
        [DataMember]
        public bool IsAutoID
        {
            get
            {
                return this._IsAutoID;
            }
            set
            {
                this._IsAutoID = value;
            }
        }

        /// <summary>
        /// 主健字段
        /// </summary>
        [DataMember]
        public string PrimaryKey
        {
            get
            {
                return this._PrimaryKey;
            }
            set
            {
                this._PrimaryKey = value;
            }
        }

        /// <summary>
        /// 数据库连接名称
        /// </summary>
        [DataMember]
        public ConnectionStrings ConnectionStringsName
        {
            get
            {
                return this._ConnectionStringsName;
            }
            set
            {
                this._ConnectionStringsName = value;
            }
        }

        /// <summary>
        /// 是否外部设置数据库
        /// </summary>
        [DataMember]
        public bool IsExternalConnection
        {
            get
            {
                return this._IsExternalConnection;
            }
            set
            {
                this._IsExternalConnection = value;
            }
        }

        /// <summary>
        /// 外部设置的数据库名称
        /// </summary>
        [DataMember]
        public string ExternalConnectionStringsName
        {
            get
            {
                return this._ExternalConnectionStringsName;
            }
            set
            {
                this._ExternalConnectionStringsName = value;
            }
        }

        /// <summary>
        /// 实体动作
        /// </summary>
        [DataMember]
        public DataEntityActions DataEntityAction
        {
            get
            {
                return this._DataEntityAction;
            }
            set
            {
                this._DataEntityAction = value;
            }
        }

        /// <summary>
        /// 实体唯一标识值
        /// </summary>
        [DataMember]
        public virtual object KeyID
        {
            get
            {
                return this._KeyID;
            }
            set
            {
                this._KeyID = value;
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public DataEntityBase()
        { }
        #endregion
    }

    /// <summary>
    /// 业务扩展类
    /// </summary>
    public partial class DataEntityBase 
    {
        /// <summary>
        /// 仓库量ID
        /// </summary>
        private int _StockID = 0;
        /// <summary>
        /// 仓库量ID
        /// </summary>
        [DataMember]
        public virtual int StockID
        {
            get
            {
                return this._StockID;
            }
            set
            {
                this._StockID = value;
            }
        }
    }
}
