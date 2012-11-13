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

        private List<Lession> _lessions = new List<Lession>();
        [JsonProperty("Lessons")]
        public List<Lession> Lessions
        {
            get
            {
                return _lessions;
            }
            set
            {
                _lessions = value;
            }
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
