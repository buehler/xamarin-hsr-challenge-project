using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HSR_Helper.DomainLibrary.Persistency
{
    public delegate void PersistentObjectChangedHandler(PersistentObject changedObject);

    public abstract class PersistentObject
    {
        [XmlIgnore]
        public virtual string Id
        {
            get
            {
                return GetType().Name;
            }
        }

        [XmlIgnore]
        public string ErrorMessage{ get; set; }
        [XmlIgnore]
        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }

        public DateTime LastUpdated{ get; set; }

        protected void OnObjectChanged()
        {
            if (ObjectChanged != null)
                ObjectChanged(this);
        }

        public event PersistentObjectChangedHandler ObjectChanged;
    }
}
