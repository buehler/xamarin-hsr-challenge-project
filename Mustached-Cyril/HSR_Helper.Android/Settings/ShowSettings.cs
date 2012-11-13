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
using HSR_Helper.Andoid;
using Android.Views.InputMethods;

namespace HSR_Helper.Android
{
    [Activity()]
    public class ShowSettings : Activity
    {
        public static Context appContext;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Setting);
            var save = this.FindViewById<Button>(Resource.Id.saveButton);
            save.Click += (sender,evt) => 
                {
                    if (this.FindViewById<TextView>(Resource.Id.userName).Text.Length > 0)
                    {
                        ApplicationSettings.Instance.Persistency.Delete(new HSR_Helper.DomainLibrary.Domain.Timetable.Timetable() { Username = ApplicationSettings.Instance.UserCredentials.Name });
                        ApplicationSettings.Instance.UserCredentials.Name = this.FindViewById<TextView>(Resource.Id.userName).Text;
                        ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
                    }
                    if (this.FindViewById<TextView>(Resource.Id.password).Text.Length > 0)
                    {
                        ApplicationSettings.Instance.Persistency.Delete(new HSR_Helper.DomainLibrary.Domain.Timetable.Timetable() { Username = ApplicationSettings.Instance.UserCredentials.Name });
                        ApplicationSettings.Instance.UserCredentials.Password = this.FindViewById<TextView>(Resource.Id.password).Text;
                        ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
                    }

                };
        }
    }
}