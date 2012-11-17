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
using HSR_Helper.Android.Views;

namespace HSR_Helper.Android
{
    [Activity(Icon="@drawable/Icon")]
    public class TimetableActivity : Activity
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

        private void TimetableCallback(Timetable timetable, object[] callbackArguments)
        {
            this.RunOnUiThread(() => horiPager.RemoveAllViews());
            if (timetable != null)
            {
                foreach (TimetableDay day in timetable.TimetableDays) 
                {

                    this.RunOnUiThread(() => horiPager.AddView(new TimetableDayView(day).GetView(this)));

                    System.Console.WriteLine(day.Weekday);
                    foreach (Lession lession in day.Lessions)
                    {
                        System.Console.WriteLine(lession);
                    }

                }
            }
        }

    }
}