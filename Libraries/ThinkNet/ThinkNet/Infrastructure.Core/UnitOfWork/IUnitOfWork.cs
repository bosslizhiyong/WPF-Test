using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// Unit Of Work类接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交当前的Unit Of Work事务。
        /// </summary>
        void Commit();
    }
}
