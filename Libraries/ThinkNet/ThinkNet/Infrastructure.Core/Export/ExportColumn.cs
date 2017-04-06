using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 数据列对象
    /// </summary>
    [Serializable]
    public class ExportColumn
    {
        /// <summary>
        /// 列名,对应字段名(英文)
        /// </summary>
        public String ColumnName { get; set; }
        /// <summary>
        /// 列名,对应字段描述(中文)
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 列号
        /// </summary>
        public int ColumnNumber { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public object DataValue { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExportColumn()
        {
            this.ColumnWidth = 15;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名,对应字段名(英文)</param>
        /// <param name="description">列名,对应字段描述(中文)</param>
        public ExportColumn(string columnName, string description)
        {
            this.ColumnName = columnName;
            this.Description = description;
            this.ColumnWidth = 15;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名,对应字段名(英文)</param>
        /// <param name="description">列名,对应字段描述(中文)</param>
        public ExportColumn(string columnName, string description,int width)
        {
            this.ColumnName = columnName;
            this.Description = description;
            this.ColumnWidth = width;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名,对应字段名(英文)</param>
        /// <param name="description">列名,对应字段描述(中文)</param>
        /// <param name="dataType">字段类型</param>
        public ExportColumn(string columnName, string description,string dataType)
        {
            this.ColumnName = columnName;
            this.Description = description;
            this.DataType = dataType;
            this.ColumnWidth = 15;
        }
    }
}
