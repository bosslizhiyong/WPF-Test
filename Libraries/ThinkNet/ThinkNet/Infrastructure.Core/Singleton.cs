using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 单例模式，泛型构建Singleton模式(需要使用该模式的类,只需要从此类继承即可)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T>
    {
        protected Singleton()
        { }

        protected static T _instance;
        public static T Instance()
        {
            if (_instance == null)
            {
                _instance = (T)Activator.CreateInstance(typeof(T), true);//use reflection to create instance of T
            }
            return _instance;
        }

        public void Destroy()
        {
            _instance = default(T);
        }
    }
}
