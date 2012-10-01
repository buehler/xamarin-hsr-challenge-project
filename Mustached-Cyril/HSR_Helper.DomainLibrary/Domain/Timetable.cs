using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Timetable
    {
        public List<Lecture> Lectures { get; private set; }

        public Timetable()
        {
            Lectures = new List<Lecture>();
        }
    }
}
