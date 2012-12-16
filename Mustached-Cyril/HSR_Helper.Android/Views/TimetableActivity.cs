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
            TextView test = new TextView(this);
            test.Text = "Am Lade...";
            horiPager.AddView(test);
            SetContentView(horiPager);

            var _loadedTimetable = ApplicationSettings.Instance.Persistency.Load<Timetable>();
            if (_loadedTimetable != null && _loadedTimetable.TimetableDays.Count > 0)
            {
                displayTimetable(_loadedTimetable);
            }
            else
            {
                DomainLibraryHelper.GetUserTimetable(ApplicationSettings.Instance.UserCredentials, TimetableCallback);
            }
        }

        private void displayTimetable(Timetable timetable)
        {
            this.RunOnUiThread(() => horiPager.RemoveAllViews());
            if (timetable != null && timetable.TimetableDays.Count > 0)
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
            else
            {
                TextView test = new TextView(this);
                test.Text = "Keine Daten gefunden";
                horiPager.AddView(test);
            }
        }

        private void TimetableCallback(Timetable timetable, object[] callbackArguments)
        {
            displayTimetable(timetable);
            ApplicationSettings.Instance.Persistency.Save(timetable);
        }

    }
}