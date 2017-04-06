using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 数据列对象
    /// </summary>
    [Serializable]
    public class ColumnInfo
    {
        /// <summary>
        /// 是否合法
        /// </summary>
        //public Boolean IsValid { get; set; }//暂时屏蔽

        /// <summary>
        /// 数据列验证类型
        /// </summary>
        public VerificationTypes VerificationType { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage = String.Empty;

        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 行位置
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public String Length { get; set; }

        /// <summary>
        /// 列对应的表字段名
        /// </summary>
        public String ColumnName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 有效值
        /// </summary>
        public Object DataValue { get; set; }

        /// <summary>
        /// 原始值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 其他值
        /// </summary>
        public Object OtherValue { get; set; }

        /// <summary>
        /// 是否唯一
        /// </summary>
        public Boolean Unique { get; set; }

        /// <summary>
        /// 数据所在的列号
        /// </summary>
        public int ColumnNumber { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int ListWidth { get; set; }

        public bool LockLeft { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnInfo()
        {
            //IsValid = true;//暂时屏蔽
            VerificationType = VerificationTypes.Correct;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="description">描述</param>
        public ColumnInfo(string type, string description)
        {
            this.DataType = type;
            this.Description = description;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="length">长度</param>
        /// <param name="description">描述</param>
        public ColumnInfo(string type, string length, string description)
            : this(type, description)
        {
            this.DataType = type;
            this.Length = length;
            this.Description = description;
        }
    }
}
