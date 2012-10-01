using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Lunchtable
    {
        public List<Dish> Dishes { get; set; }

        public Lunchtable()
        {
            Dishes = new List<Dish>();
        }
    }
}
