using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using HSR_Helper.DomainLibrary.Helper;

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

        public override bool Equals(object obj)
        {
            var o = obj as CourseAllocation;
            if (o != null)
            {
                return (CompareString(Description, o.Description) && CompareString(Timeslot, o.Timeslot) && CompareString(Type, o.Type) && RoomAllocations.ContentsAreIdentical(o.RoomAllocations));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }
        
        public override string ToString()
        {
            return (RoomAllocations.Aggregate(Description + Timeslot + Type, (c,r) => c + r.ToString()));
        }

        private bool CompareString(string s1, string s2)
        {
            if ((s1 == null && s2 != null) || (s2 == null && s1 != null))
                return false;
            if (s1 == null && s2 == null)
                return true;
            return s1.Equals(s2);
        }
    }

    public class RoomAllocation
    {
        [JsonProperty("Number")]
        public string Roomnumber { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as RoomAllocation;
            if (o != null)
            {
                return (this.Roomnumber.Equals(o.Roomnumber));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }
        
        public override string ToString()
        {
            return (Roomnumber);
        }
    }
}
