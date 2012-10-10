using System.Collections.Generic;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Timetable
    {
        [JsonProperty("Days")]
        public List<TimetableDay> TimetableDays { get; set; }
        [JsonProperty("Semester")]
        public string Semester { get; set; }
        
        public Timetable()
        {
            TimetableDays = new List<TimetableDay>();
        }
    }
}
