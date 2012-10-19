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
        public bool Save(PersistentObject obj)
        {
            throw new NotImplementedException();
        }

        public bool Exists<T>() where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }

        public bool Exists<T>(T prototype) where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>() where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(T prototype) where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }

        public T Load<T>() where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }

        public T Load<T>(T prototype) where T : PersistentObject, new()
        {
            throw new NotImplementedException();
        }
    }
}