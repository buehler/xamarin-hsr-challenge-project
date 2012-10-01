using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PersistentPropertyAttribute : System.Attribute
    {
        public string PropertyName { get; private set; }
        
        public PersistentPropertyAttribute(string propertyName = "")
        {
            PropertyName = propertyName;
        }
    }
}
