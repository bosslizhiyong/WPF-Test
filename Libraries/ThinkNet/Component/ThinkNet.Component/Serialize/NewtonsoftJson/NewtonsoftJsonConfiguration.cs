using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkNet.Component;

namespace ThinkNet.Component
{
    public static class NewtonsoftJsonConfiguration
    {
        public static Configuration UseJsonNet(this Configuration configuration, params Type[] creationWithoutConstructorTypes)
        {
            configuration.SetDefault<IJsonSerializer, NewtonsoftJsonSerializer>(new NewtonsoftJsonSerializer(creationWithoutConstructorTypes));
            return configuration;
        }
    }
}
