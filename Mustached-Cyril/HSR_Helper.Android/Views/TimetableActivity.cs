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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var MenuItem1 = menu.Add(0, 1, 1, "Aktualisieren");
            var MenuItem2 = menu.Add(0, 2, 2, "Beenden");

            // Set icon
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var display = WindowManager.DefaultDisplay;
            horiPager = new HorizontalPager(this.ApplicationContext, display);
            TextView test = new TextView(this);
            test.Text = "Laden...";

            horiPager.AddView(test);
            SetContentView(horiPager);

            var _loadedTimetable = ApplicationSettings.Instance.Persistency.Load<Timetable>(new Timetable() { Username=ApplicationSettings.Instance.UserCredentials.Name });
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
                int i = 0;
                int day_value = (int)System.DateTime.Today.DayOfWeek;
                if (day_value == 0) day_value = 7;
                foreach (TimetableDay day in timetable.TimetableDays)
                {
                    this.RunOnUiThread(() => horiPager.AddView(new TimetableDayView(day, i > 0, i < timetable.TimetableDays.Count - 1).GetView(this)));
                    i++;
                }
                this.RunOnUiThread(() => horiPager.SetCurrentScreen(day_value, false));
            }
            else
            {
                TextView test = new TextView(this);
                if (timetable.ErrorMessage != null)
                    test.Text = timetable.ErrorMessage;
                else
                    test.Text = "Keine Daten gefunden";
                this.RunOnUiThread(() => horiPager.AddView(test));
            }
        }

        private void TimetableCallback(Timetable timetable, object[] callbackArguments)
        {
            displayTimetable(timetable);
            ApplicationSettings.Instance.Persistency.Save(timetable);
        }



        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 1:
                    DomainLibraryHelper.GetUserTimetable(ApplicationSettings.Instance.UserCredentials, TimetableCallback);
                    return true;
                case 2:
                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}