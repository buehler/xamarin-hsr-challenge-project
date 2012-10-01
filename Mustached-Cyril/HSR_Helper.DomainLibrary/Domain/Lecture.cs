using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Lecture
    {
        public string Title { get; set; }
        public string Room { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
