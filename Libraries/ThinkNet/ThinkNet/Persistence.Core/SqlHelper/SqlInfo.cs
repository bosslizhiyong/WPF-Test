using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Persistence.Core
{
    public class SqlInfo
    {
        /// <summary>
        /// sql语句或存储过程名
        /// </summary>
        public string SqlOrProcName { get; set; }

        /// <summary>
        /// 是否存储过程 
        /// </summary>
        public bool IsProc { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<DbParameter> Parameters { get; set; }

        /// <summary>
        /// 构造一个SqlHelper信息对象
        /// </summary>
        /// <param name="sqlOrProcName">Sql语句或存储过程名</param>
        /// <param name="isProc">是否存储过程</param>
        /// <param name="parameters">参数列表</param>
        public SqlInfo(string sqlOrProcName, bool isProc, List<DbParameter> parameters)
        {
            this.SqlOrProcName = sqlOrProcName;
            this.IsProc = isProc;
            this.Parameters = parameters;
        }

        /// <summary>
        /// 构造一个SqlHelper信息对象
        /// </summary>
        /// <param name="sqlOrProcName">Sql语句或存储过程名</param>
        /// <param name="parameters">参数列表</param>
        public SqlInfo(string sqlOrProcName, List<DbParameter> parameters)
            : this(sqlOrProcName, true, parameters)
        {

        }

        /// <summary>
        /// 构造一个SqlHelper信息对象
        /// </summary>
        public SqlInfo()
            : this(string.Empty, false, null)
        {

        }
    }
}
