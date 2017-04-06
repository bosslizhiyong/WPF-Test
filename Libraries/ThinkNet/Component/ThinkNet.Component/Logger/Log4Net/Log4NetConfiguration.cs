using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkNet.Component;

namespace ThinkNet.Component
{
    public static class Log4NetConfiguration
    {
        public static Configuration UseLog4Net(this Configuration configuration)
        {
            return UseLog4Net(configuration, "log4net.config");
        }
        
        public static Configuration UseLog4Net(this Configuration configuration, string configFile)
        {
            configuration.SetDefault<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));
            return configuration;
        }
    }
}
