using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.DomainLibrary.Android.Persistency
{
    class AndroidPersistency : IPersistency
    {
        public bool Save(IPersistentObject obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IPersistentObject Load(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}