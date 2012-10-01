using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Dish : IPersistentObject
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
