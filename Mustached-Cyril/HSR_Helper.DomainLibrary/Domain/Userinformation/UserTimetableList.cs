using System.Collections.Generic;
using System.Linq;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.ObjectModel;
using HSR_Helper.DomainLibrary.Helper;
using System;

namespace HSR_Helper.DomainLibrary.Domain.Userinformation
{
    public class UserTimetableList : PersistentObject
    {
        public ObservableCollection<string> Usernames { get; set; }

        public string LastOpenedTimetable { get; set; }

        public UserTimetableList()
        {
            Usernames = new ObservableCollection<string>();
            Usernames.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => OnObjectChanged();
        }

        public override bool Equals(object obj)
        {
            var o = obj as UserTimetableList;
            if (o != null)
            {
                return Usernames.ContentsAreIdentical(o.Usernames);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Usernames.Aggregate("", (current, s) => current + s)).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[UserTimetableList: Usernames={0}, LastOpenedTimetable={1}]", Usernames.Aggregate("", (c,o) => c + o), LastOpenedTimetable);
        }
    }
}
