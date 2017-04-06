using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Persistence.Core
{
    public class Parameter
    {
        #region 字段
        /// <summary>
        /// 参数类型
        /// </summary>
        private DbType _ParameterDbType = DbType.String;
        /// <summary>
        /// 参数名称
        /// </summary>
        private string _ParameterName = "";
        /// <summary>
        /// 参数值
        /// </summary>
        private object _ParameterValue = null;
        #endregion

        #region 属性
        /// <summary>
        /// 参数类型
        /// </summary>
        public DbType ParameterDbType
        {
            get
            {
                return this._ParameterDbType;
            }
            set
            {
                this._ParameterDbType = value;
            }
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get
            {
                return this._ParameterName;
            }
            set
            {
                this._ParameterName = value;
            }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public object ParameterValue
        {
            get
            {
                return this._ParameterValue;
            }
            set
            {
                this._ParameterValue = value;
            }
        }
        #endregion
    }
}
