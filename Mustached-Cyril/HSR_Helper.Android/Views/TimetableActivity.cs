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
using Cheesebaron.HorizontalPager;
using HSR_Helper.DomainLibrary.Helper;
using HSR_Helper.Andoid;
using HSR_Helper.DomainLibrary.Domain.Timetable;

namespace HSR_Helper.Android
{
    [Activity(Icon="@drawable/Icon")]
    public class ShowTimeTable : Activity
    {
        public static Context appContext;
        HorizontalPager horiPager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var display = WindowManager.DefaultDisplay;
            horiPager = new HorizontalPager(this.ApplicationContext, display);
            SetContentView(horiPager);
            DomainLibraryHelper.GetUserTimetable(ApplicationSettings.Instance.UserCredentials,TimetableCallback);
        }

        private void TimetableCallback(DomainLibrary.Domain.Timetable.Timetable timetable, object[] callbackArguments)
        {
            if (timetable != null)
            {
                foreach (TimetableDay day in timetable.TimetableDays) 
                {
                    System.Console.WriteLine(day.Weekday);
                    foreach (Lession lession in day.Lessions)
                    {
                        System.Console.WriteLine(lession.Name);
                    }

                }
            }
        }

    }
}