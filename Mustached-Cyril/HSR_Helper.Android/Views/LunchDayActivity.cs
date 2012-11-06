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
            if (_loadedLuchtable == null || _loadedLuchtable.LunchDays.Count <1)
            {
                DomainLibraryHelper.GetLunchtable(lunchtableCallback);
                
            }
            else
            {
                System.Console.WriteLine(_loadedLuchtable.LastUpdated);
                addLunchtable(_loadedLuchtable);
            }
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
                System.Console.WriteLine(day.DateString);
                this.RunOnUiThread(() => horiPager.AddView(new LunchDayView(day).GetView(this, day_value)));
            }
            this.RunOnUiThread(() => horiPager.SetCurrentScreen(day_value, false));
        }

    }
}