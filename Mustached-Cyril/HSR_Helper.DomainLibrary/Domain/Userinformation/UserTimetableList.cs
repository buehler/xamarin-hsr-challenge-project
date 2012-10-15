using System.Collections.Generic;
using System.Linq;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.DomainLibrary.Domain.Userinformation
{
    public class UserTimetableList : PersistentObject
    {
        public HashSet<string> Usernames { get; set; }

        public string LastOpenedTimetable { get; set; }

        public UserTimetableList()
        {
            Usernames = new HashSet<string>();
        }

        public override bool Equals(object obj)
        {
            var o = obj as UserTimetableList;
            return o != null && Usernames.SetEquals(o.Usernames);
        }

        public override int GetHashCode()
        {
            var o = Usernames.Aggregate("", (current, s) => current + s);
            return o.GetHashCode();
        }
    }
}
