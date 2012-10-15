using System.Collections;

namespace HSR_Helper.DomainLibrary.Helper
{
    public static class IEnumerableExtensions
    {
        public static bool ContentsAreIdentical(this IEnumerable items)
        {
            object lastItem = null;
            foreach (object item in items)
            {
                if (lastItem != null && !lastItem.Equals(item))
                    return false;
                lastItem = item;
            }
            return true;
        }
    }
}
