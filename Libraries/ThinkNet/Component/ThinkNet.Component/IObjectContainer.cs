using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public interface IObjectContainer
    {
        void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton);
        
        void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton);
        
        void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService;
        
        void RegisterInstance<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService;
       
        TService Resolve<TService>() where TService : class;
        
        object Resolve(Type serviceType);
    }
}
