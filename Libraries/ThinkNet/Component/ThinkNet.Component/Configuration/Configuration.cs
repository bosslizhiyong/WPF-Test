using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public class Configuration
    {
        #region 字    段

        #endregion

        #region 属    性

        public Setting Setting { get; private set; }

        public static Configuration Instance { get; private set; }

        #endregion

        #region 构造函数

        private Configuration(Setting setting)
        {
            Setting = setting ?? new Setting();
        }

        #endregion

        #region 基本方法

        public static Configuration Create(Setting setting = null)
        {
            if (Instance != null)
            {
                throw new Exception("不能创建此类两次或以上。");
            }
            Instance = new Configuration(setting);
            return Instance;
        }

        public Configuration SetDefault<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(life);
            return this;
        }
        public Configuration SetDefault<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance);
            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            //SetDefault<ILoggerFactory, Log4NetLoggerFactory>();
            //SetDefault<IBinarySerializer, DefaultBinarySerializer>();
            //SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>();
            return this;
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
