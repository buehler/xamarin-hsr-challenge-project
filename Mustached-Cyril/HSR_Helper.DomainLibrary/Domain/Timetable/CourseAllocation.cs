using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class CourseAllocation
    {
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Timeslot")]
        public string Timeslot { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("RoomAllocations")]
        public List<RoomAllocation> RoomAllocations { get; set; }

        public CourseAllocation()
        {
            RoomAllocations = new List<RoomAllocation>();
        }
    }

    public class RoomAllocation
    {
        [JsonProperty("Number")]
        public string Roomnumber { get; set; }
    }
}
