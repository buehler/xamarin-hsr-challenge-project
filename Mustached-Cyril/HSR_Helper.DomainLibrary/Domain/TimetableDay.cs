using System.Collections.Generic;
using Newtonsoft.Json;


namespace HSR_Helper.DomainLibrary.Domain
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
    }
}
