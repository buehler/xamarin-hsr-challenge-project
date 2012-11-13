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

        private List<TimetableDay> _days = new List<TimetableDay>();
        [JsonProperty("Days")]
        public List<TimetableDay> TimetableDays
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
            }
        }

        [JsonProperty("Semester")]
        public string Semester { get; set; }

        public string Username { get; set; }

        public bool BlockedTimetable{ get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Timetable;
            if (o != null)
            {
                return (Semester.CompareString(o.Semester) && Username.CompareString(o.Username) && TimetableDays.ContentsAreIdentical(o.TimetableDays));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (TimetableDays.Aggregate(Username + Semester, (current, day) => current + day.ToString())).GetHashCode();
        }
    }
}
