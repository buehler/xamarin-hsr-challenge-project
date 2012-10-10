using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class Lession
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("CourseAllocations")]
        public List<CourseAllocation> CourseAllocations { get; set; }
        [JsonProperty("Lecturers")]
        public List<Lecturer> Lecturers { get; set; }

        public Lession()
        {
            CourseAllocations = new List<CourseAllocation>();
            Lecturers = new List<Lecturer>();
        }
    }
}
