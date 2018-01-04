using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ThinkCRM.Infrastructure.Co
{
    [Serializable]
    public class DataTableSchema
    {
        /// <summary>
        /// 列属性列表
        /// </summary>
        public Columns Columns { get; set; }
    }

    [Serializable]
    public class Columns
    {
        /// <summary>
        /// 表名称
        /// </summary>
        [XmlAttribute("TableName")]
        public string TableName { get; set; }

        /// <summary>
        /// KeyColumn
        /// </summary>
        [XmlAttribute("KeyColumn")]
        public string KeyColumn { get; set; }

        [XmlElement]
        public List<Column> Column { get; set; }
    }

    [Serializable]
    public class Column
    {
        /// <summary>
        /// 列描述
        /// </summary>
        [XmlAttribute("Description")]
        public string Description { get; set; }

        /// <summary>
        /// 列名称
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        [XmlAttribute("Type")]
        public string Type { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [XmlAttribute("ColumnNumber")]
        public int ColumnNumber { get; set; }

        /// <summary>
        /// 所属表
        /// </summary>
        [XmlAttribute("Category")]
        public string Category { get; set; }
    }
}