using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PersistencyAttribute : System.Attribute
    {
        
    }
}
