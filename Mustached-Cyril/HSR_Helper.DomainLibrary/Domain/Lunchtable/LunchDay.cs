using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using RestSharp.Deserializers;
using Newtonsoft.Json;
using HSR_Helper.DomainLibrary.Helper;
using System.Xml.Serialization;

namespace HSR_Helper.DomainLibrary.Domain.Lunchtable
{
    
    public class LunchDay
    {
        [JsonProperty("date")]
        public string DateString { get; set; }
        [JsonProperty("menus")]
        public List<Dish> Dishes { get; set; }
        [XmlIgnore]
        public bool HasDishes
        {
            get
            {
                return Dishes.Count() != 0;
            }
        }

        public LunchDay()
        {
            Dishes = new List<Dish>();
        }

        public LunchDay(string datestring) : this()
        {
            DateString = datestring;
        }

        public override bool Equals(object obj)
        {
            var o = obj as LunchDay;
            if (o != null)
            {
                return (DateString.Equals(o.DateString) && Dishes.ContentsAreIdentical(o.Dishes));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Dishes.Aggregate(DateString, (current, l) => current + l.ToString());
        }
    }
}
