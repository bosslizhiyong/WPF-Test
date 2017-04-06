using System;
using System.Collections.Generic;
using System.Text;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 闪动窗口接口
    /// </summary>
    public interface ISplashUI
    {
        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="info"></param>
        void SetInformation(string info);
    }
}
