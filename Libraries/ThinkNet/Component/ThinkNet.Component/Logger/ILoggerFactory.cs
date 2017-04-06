using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    public interface ILoggerFactory
    {
        ILogger Create(string name);
        ILogger Create(Type type);
    }
}
