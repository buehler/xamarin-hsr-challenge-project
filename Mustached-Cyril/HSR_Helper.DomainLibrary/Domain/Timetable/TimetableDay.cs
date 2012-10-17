using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class TimetableDay
    {
        [JsonProperty("Description")]
        public string Weekday { get; set; }
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Lessons")]
        public List<Lession> Lessions { get; set; }

        public TimetableDay()
        {
            Lessions = new List<Lession>();
        }

        public override bool Equals(object obj)
        {
            var o = obj as TimetableDay;
            if (o != null)
            {
                return (this.Id.Equals(o.Id) && this.Weekday.Equals(o.Weekday) && this.Lessions.ContentsAreIdentical(o.Lessions));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return (Lessions.Aggregate(Weekday + Id.ToString(), (current, l) => current + l.ToString()));
        }
    }
}
