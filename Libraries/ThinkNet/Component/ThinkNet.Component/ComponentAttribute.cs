using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Component
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public LifeStyle LifeStyle { get; private set; }
        
        public ComponentAttribute() : this(LifeStyle.Transient) { }
        
        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }
    }

    public enum LifeStyle
    {
        Transient,
        Singleton
    }
}
