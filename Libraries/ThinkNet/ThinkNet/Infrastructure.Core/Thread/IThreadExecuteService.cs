using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 执行类型
    /// </summary>
    [Flags]
    public enum ExecuteTypes
    {
        /// <summary>
        /// 持续执行
        /// </summary>
        [Description("持续")]
        Continue = 0,
        /// <summary>
        /// 定时执行
        /// </summary>
        [Description("定时")]
        Timer = 1
    }
    /// <summary>
    /// 定时执行类型
    /// </summary>
    [Flags]
    public enum TimerTypes
    {
        /// <summary>
        /// 每天
        /// </summary>
        [Description("每天")]
        Day = 0,
        /// <summary>
        /// 每月
        /// </summary>
        [Description("每月")]
        Month = 1,
        /// <summary>
        /// 每周
        /// </summary>
        [Description("每周")]
        Week = 2
    }
    /// <summary>
    /// 线程执行服务接口
    /// </summary>
    public interface IThreadExecuteService
    {
        /// <summary>
        /// 线程ID号
        /// </summary>
        int ThreadID { get; set; }
        ///// <summary>
        ///// 服务名称
        ///// </summary>
        //string ServiceName { get; set; }
        ///// <summary>
        ///// 执行类型枚举(持续执行或定时执行)
        ///// </summary>
        //ExecuteTypes ExecuteType { get; set; }
        ///// <summary>
        ///// 定时执行类型(每天、每月...)
        ///// ExecuteType如果是timer,该值设置有效,否则无效
        ///// </summary>
        //TimerTypes TimerType { get; set; }
        ///// <summary>
        ///// ExecuteType如果是Continue,该值是间隔执行时间,单位秒
        ///// ExecuteType如果是timer,该值是定时执行的时间(如果TimerType是Day,该值是小时:分钟,如果TimerType是Month,该值是日&小时:分钟)
        ///// </summary>
        //string Time { get; set; }

        /// <summary>
        /// 执行方法
        /// </summary>
        void Execute();
        /// <summary>
        /// 执行一次
        /// </summary>
        /// <param name="parameter">参数对象</param>
        /// <param name="connectionStringName">数据库连接</param>
        /// <returns></returns>
        SimpleResult ExecuteOnce(object parameter, string connectionStringName);
    }
}
