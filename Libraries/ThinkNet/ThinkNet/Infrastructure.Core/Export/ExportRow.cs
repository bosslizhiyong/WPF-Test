using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 数据行对象
    /// </summary>
    [Serializable]
    public class ExportRow
    {
        /// <summary>
        /// 数据行号
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 数据列
        /// </summary>
        public List<ExportColumn> Columns { get; set; }

        /// <summary>
        /// 数据行对象
        /// </summary>
        public ExportRow()
        {
            Columns = new List<ExportColumn>();
        }
    }
}
