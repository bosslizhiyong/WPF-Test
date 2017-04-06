using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using ThinkNet.Component;

namespace ThinkNet.Autofac
{
    public static class AutofacConfiguration
    {
        public static Configuration UseAutofac(this Configuration configuration)
        {
            return UseAutofac(configuration, new ContainerBuilder());
        }
        
        public static Configuration UseAutofac(this Configuration configuration, ContainerBuilder containerBuilder)
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer(containerBuilder));
            return configuration;
        }
    }
}
