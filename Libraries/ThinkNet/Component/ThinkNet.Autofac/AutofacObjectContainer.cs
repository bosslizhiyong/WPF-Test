﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using ThinkNet.Component;

namespace ThinkNet.Autofac
{
    public class AutofacObjectContainer : IObjectContainer
    {
        private readonly IContainer _container;

        
        public AutofacObjectContainer()
        {
            _container = new ContainerBuilder().Build();
        }
        
        public AutofacObjectContainer(ContainerBuilder containerBuilder)
        {
            _container = containerBuilder.Build();
        }

        
        public IContainer Container
        {
            get
            {
                return _container;
            }
        }
       
        public void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType(implementationType);
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(_container);
        }
        
        public void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton)
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType(implementationType).As(serviceType);
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(_container);
        }
        
        public void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = new ContainerBuilder();
            var registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            if (life == LifeStyle.Singleton)
            {
                registrationBuilder.SingleInstance();
            }
            builder.Update(_container);
        }
        
        public void RegisterInstance<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(instance).As<TService>().SingleInstance();
            builder.Update(_container);
        }
        
        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }
        
        public object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}
