using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkNet.Component
{
    public interface IBinarySerializer
    {
        byte[] Serialize(object obj);
        object Deserialize(byte[] data, Type type);
        T Deserialize<T>(byte[] data) where T : class;
    }
}
