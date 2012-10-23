using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using HSR_Helper.DomainLibrary.Persistency;
using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class Timetable : PersistentObject
    {
        public override string Id
        {
            get
            {
                return base.Id + Username;
            }
        }

        [JsonProperty("Days")]
        public List<TimetableDay> TimetableDays { get; set; }
        [JsonProperty("Semester")]
        public string Semester { get; set; }

        public string Username { get; set; }

        public bool BlockedTimetable{ get; set; }
        
        public Timetable()
        {
            TimetableDays = new List<TimetableDay>();
        }

        public override bool Equals(object obj)
        {
            var o = obj as Timetable;
            if (o != null)
            {
                return (StaticMethods.CompareString(Semester, o.Semester) && Username.Equals(o.Username) && TimetableDays.ContentsAreIdentical(o.TimetableDays));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (TimetableDays.Aggregate(Username + Semester, (current, day) => current + day.ToString())).GetHashCode();
        }
    }
}
