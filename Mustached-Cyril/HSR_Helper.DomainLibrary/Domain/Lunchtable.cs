using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Attribute;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Lunchtable : IPersistentObject
    {
        [PersistentProperty]
        public Guid Id { get; set; }
        [PersistentProperty]
        public List<Dish> Dishes { get; set; }

        public string fudi { get; set; }

        public Lunchtable()
        {
            Dishes = new List<Dish>();
        }
    }
}
