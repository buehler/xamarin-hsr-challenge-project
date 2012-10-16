using System.Collections.Generic;
using Newtonsoft.Json;
using HSR_Helper.DomainLibrary.Persistency;
using System.Xml.Serialization;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class Timetable : PersistentObject
    {
        [XmlIgnore]
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
        
        public Timetable()
        {
            TimetableDays = new List<TimetableDay>();
        }
    }
}
