using System.Collections.Generic;
using System.Linq;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.ObjectModel;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.DomainLibrary.Helper
{
    public static class IEnumerableExtensions
    {
        public static bool ContentsAreIdentical<T>(this IEnumerable<T> items, IEnumerable<T> otherItems)
        {
            if (items.Count() != otherItems.Count())
                return false;
            var l1 = from o in items
                     where !otherItems.Any(i => o.Equals(i))
                     select o;
            var l2 = from o in otherItems
					 where !items.Any(i => o.Equals(i))
                     select o;
            return (l1.Count() == 0 && l2.Count() == 0);
        }

        public static bool ListsAreIdentical<T>(this IEnumerable<T> items, IEnumerable<T> otherItems)
        {
            if (items.Count() != otherItems.Count())
                return false;
            for (int i = 0; i < items.Count(); i++)
            {
                if (!items.ElementAt(i).Equals(otherItems.ElementAt(i)))
                    return false;
            }
            return true;
        }
    }

    public static class StaticMethods
    {
        public static bool CompareString(string s1, string s2)
        {
            if ((s1 == null && s2 != null) || (s2 == null && s1 != null))
                return false;
            if (s1 == null && s2 == null)
                return true;
            return s1.Equals(s2);
        }
    }
}
