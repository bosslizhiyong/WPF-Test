using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 数据行对象
    /// </summary>
    [Serializable]
    public class RowInfo
    {
        /// <summary>
        /// 数据行号
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 数据列
        /// </summary>
        public List<ColumnInfo> Columns { get; set; }

        /// <summary>
        /// 是否已经处理了(写入目标表)
        /// </summary>
        public bool IsHandle { get; set; }
        /// <summary>
        /// 行处理错误提示
        /// </summary>
        public string HandleError { get; set; }

        /// <summary>
        /// 数据行对象
        /// </summary>
        public RowInfo()
        {
            IsHandle = false;
            HandleError = string.Empty;
            Columns = new List<ColumnInfo>();
        }

        /// <summary>
        /// 行是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                //return !((from c in Columns where c.IsValid == false select c).ToList<ColumnInfo>().Count > 0);//暂时屏蔽
                return !((from c in Columns where c.VerificationType == VerificationTypes.Error || c.VerificationType == VerificationTypes.NotExist || c.VerificationType == VerificationTypes.MultiExist || c.VerificationType == VerificationTypes.Exist select c).ToList<ColumnInfo>().Count > 0);
            }
        }

        /// <summary>
        /// 行是重复的
        /// </summary>
        public bool IsRepeat
        {
            get
            {
                return ((from c in Columns where c.VerificationType == VerificationTypes.Repeat select c).ToList<ColumnInfo>().Count > 0);
            }
        }
        /// <summary>
        /// 行是错误的
        /// </summary>
        public bool IsError
        {
            get
            {
                return ((from c in Columns where c.VerificationType == VerificationTypes.Error select c).ToList<ColumnInfo>().Count > 0);
            }
        }
        /// <summary>
        /// 行是验证不通过的
        /// </summary>
        public bool IsNotPass
        {
            get
            {
                return ((from c in Columns where c.VerificationType == VerificationTypes.NotExist || c.VerificationType == VerificationTypes.MultiExist || c.VerificationType == VerificationTypes.Exist select c).ToList<ColumnInfo>().Count > 0);
            }
        }
    }
}
