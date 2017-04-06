using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public class ObjectContainer
    {
        public static IObjectContainer Current { get; private set; }

        public static void SetContainer(IObjectContainer container)
        {
            Current = container;
        }

        public static void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            Current.RegisterType(implementationType, life);
        }
        
        public static void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            Current.RegisterType(serviceType, implementationType, life);
        }
        
        public static void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            Current.Register<TService, TImplementer>(life);
        }
        
        public static void RegisterInstance<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService
        {
            Current.RegisterInstance<TService, TImplementer>(instance);
        }
        
        public static TService Resolve<TService>() where TService : class
        {
            return Current.Resolve<TService>();
        }
        
        public static object Resolve(Type serviceType)
        {
            return Current.Resolve(serviceType);
        }
    }
}
