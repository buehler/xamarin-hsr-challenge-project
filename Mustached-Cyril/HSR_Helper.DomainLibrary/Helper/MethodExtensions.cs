﻿using System.Collections.Generic;
using System.Linq;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.ObjectModel;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.DomainLibrary.Helper
{
    public static class ObservableCollectionExtensions
    {
        public static bool ContentsAreIdentical<T>(this ObservableCollection<T> items, ObservableCollection<T> otherItems)
        {
            var l1 = from o in items
                     where !otherItems.Contains(o)
                     select o;
            var l2 = from o in otherItems
                     where !items.Contains(o)
                     select o;
            return (l1.Count() == 0 && l2.Count() == 0);
        }
    }
}
