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

        private List<RoomAllocation> _roomAllocations = new List<RoomAllocation>();
        [JsonProperty("RoomAllocations")]
        public List<RoomAllocation> RoomAllocations
        {
            get
            {
                return _roomAllocations;
            }
            set
            {
                _roomAllocations = value;
            }
        }

        public override bool Equals(object obj)
        {
            var o = obj as CourseAllocation;
            if (o != null)
            {
                return (Description.CompareString(o.Description) && Timeslot.CompareString(o.Timeslot) && Type.CompareString(o.Type) && RoomAllocations.ContentsAreIdentical(o.RoomAllocations));
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
