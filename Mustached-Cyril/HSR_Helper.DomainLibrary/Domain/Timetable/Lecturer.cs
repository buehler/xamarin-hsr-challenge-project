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

        public override bool Equals(object obj)
        {
            var o = obj as Lecturer;
            if (o != null)
            {
                return (this.Fullname.Equals(o.Fullname) && this.Shortname.Equals(o.Shortname));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }
        
        public override string ToString()
        {
            return (Fullname + Shortname);
        }
    }
}
