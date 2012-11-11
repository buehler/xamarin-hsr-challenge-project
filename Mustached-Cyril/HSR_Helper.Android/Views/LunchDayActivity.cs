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
using HSR_Helper.DomainLibrary.Domain;
using HSR_Helper.DomainLibrary.Helper;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using Cheesebaron.HorizontalPager;
using HSR_Helper.Android.Views;
using HSR_Helper.Andoid;
using System.Globalization;

namespace HSR_Helper.Android
{
    [Activity()]
    public class LunchDayActivity : Activity
    {
        public static Context appContext;
        HorizontalPager horiPager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var display = WindowManager.DefaultDisplay;
            horiPager = new HorizontalPager(this.ApplicationContext, display);
            SetContentView(horiPager);
            TextView test = new TextView(this);
            test.Text = "Am Lade...";
            horiPager.AddView(test);
            var _loadedLuchtable = ApplicationSettings.Instance.Persistency.Load<Lunchtable>();
            if (_loadedLuchtable != null && _loadedLuchtable.LunchDays.Count > 0)
            {
                var cal = CultureInfo.CurrentCulture.Calendar;
                int db = cal.GetWeekOfYear(_loadedLuchtable.LastUpdated, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
                System.Console.WriteLine("Kalender woche des Ladens: " + db);
                int act = cal.GetWeekOfYear(System.DateTime.Today, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
                System.Console.WriteLine("Kalender woche aktuell: " + act);

                if (act <= db)
                {
                    DomainLibraryHelper.GetLunchtable(lunchtableCallback);
                    return;
                }
            }
            System.Console.WriteLine(_loadedLuchtable.LastUpdated);
            addLunchtable(_loadedLuchtable);

        }

        public void lunchtableCallback(Lunchtable lunchtable)
        {
            addLunchtable(lunchtable);
            ApplicationSettings.Instance.Persistency.Save(lunchtable);
        }

        public void addLunchtable(Lunchtable lunchtable)
        {
            this.RunOnUiThread(() => horiPager.RemoveAllViews());
            if (lunchtable.NoMenuesFound)
            {
                System.Console.WriteLine("Leider keine Daten gefunden");
                return;
            }
            int day_value = (int)System.DateTime.Today.DayOfWeek-1;
            if (day_value > 5 || day_value < 0) day_value = 0;
            foreach (LunchDay day in lunchtable.LunchDays)
            {
                this.RunOnUiThread(() => horiPager.AddView(new LunchDayView(day).GetView(this, day_value)));
            }
            this.RunOnUiThread(() => horiPager.SetCurrentScreen(day_value, false));
            System.Console.WriteLine(lunchtable.LastUpdated);
        }

        int calculateWeekOfYear(DateTime date)
        {
            var cal = CultureInfo.CurrentCulture.Calendar;
            return cal.GetWeekOfYear(date, 
                DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

    }
}