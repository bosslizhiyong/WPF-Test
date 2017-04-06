using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;

namespace ThinkNet.Utility
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogManager
    {
        #region 字    段

        static ILog _LoggerInfo = null;
        static ILog _LoggerError = null;

        #endregion

        #region 属    性

        #endregion

        #region 构造函数

        static LogManager()
        {
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));//读取日志配置

            if (!file.Exists) return;

            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);

            _LoggerInfo = log4net.LogManager.GetLogger("LoggerInfo");
            _LoggerError = log4net.LogManager.GetLogger("LoggerError");
        }

        #endregion

        #region 基本方法

        public static LogManager Instance()
        {
            return new LogManager();
        }

        public static void WriteLog(string info)
        {
            if (_LoggerInfo.IsInfoEnabled)
            {
                _LoggerInfo.Info(info);
            }
        }

        public static void WriteLog(string info, Exception ex)
        {
            if (_LoggerError.IsErrorEnabled)
            {
                _LoggerError.Error(info, ex);
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}