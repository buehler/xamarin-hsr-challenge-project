using System.Collections.Generic;
using Newtonsoft.Json;
using HSR_Helper.DomainLibrary.Helper;
using System.Linq;

namespace HSR_Helper.DomainLibrary.Domain.Lunchtable
{
    public class Lunchtable : Persistency.PersistentObject
    {
        [JsonProperty("days")]
        public List<LunchDay> LunchDays { get; set; }
        
        public Lunchtable()
        {
            LunchDays = new List<LunchDay>();
        }

        public override bool Equals(object obj)
        {
            var o = obj as Lunchtable;
            if (o != null)
            {
                return LunchDays.ContentsAreIdentical(o.LunchDays);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (LunchDays.Aggregate("", (current, l) => current + l.ToString())).GetHashCode();
        }
    }
}
