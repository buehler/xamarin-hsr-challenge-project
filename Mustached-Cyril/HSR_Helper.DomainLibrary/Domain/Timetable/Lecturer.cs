using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class Lecturer
    {
        [JsonProperty("Fullname")]
        public string Fullname { get; set; }
        [JsonProperty("Shortname")]
        public string Shortname { get; set; }
    }
}
