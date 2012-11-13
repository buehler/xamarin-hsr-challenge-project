using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.DomainLibrary.Domain.Timetable
{
    public class Lession
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        private List<CourseAllocation> _courseAllocations;
        [JsonProperty("CourseAllocations")]
        public List<CourseAllocation> CourseAllocations
        {
            get
            {
                return _courseAllocations;
            }
            set
            {
                _courseAllocations = value;
            }
        }

        private List<Lecturer> _lecturers = new List<Lecturer>();
        [JsonProperty("Lecturers")]
        public List<Lecturer> Lecturers
        {
            get
            {
                return _lecturers;
            }
            set
            {
                _lecturers = value;
            }
        }

        [XmlIgnore]
        public string LecturersShortVersion
        {
            get
            {
                return Lecturers.Aggregate("", (current, l) => current + l.Shortname + (Lecturers.IndexOf(l) == Lecturers.Count() - 1 ? "" : "/"));
            }
        }

        public override bool Equals(object obj)
        {
            var o = obj as Lession;
            if (o != null)
            {
                return (this.Name.Equals(o.Name) && this.Type.Equals(o.Type) && this.CourseAllocations.ContentsAreIdentical(o.CourseAllocations) && this.Lecturers.ContentsAreIdentical(o.Lecturers));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return (ToString()).GetHashCode();
        }
        
        public override string ToString()
        {
            return (Name + Type + CourseAllocations.Aggregate("", (c, ca) => c + ca.ToString()) + Lecturers.Aggregate("", (c,l) => c + l.ToString()));
        }
    }
}
