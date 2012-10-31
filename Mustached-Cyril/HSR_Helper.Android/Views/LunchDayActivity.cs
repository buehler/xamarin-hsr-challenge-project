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
using HSR_Helper.Android.Views1;

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
            DomainLibraryHelper.GetLunchtable(addLunchtable);
            horiPager.SetCurrentScreen(3, false);
            //DomainLibraryHelper.GetUserBadgeInformation(
        }

        public void addLunchtable(Lunchtable lunchtable)
        {
            if (lunchtable.NoMenuesFound)
            {
                System.Console.WriteLine("Leider keine Daten gefunden");
                return;
            }
            int day_value = (int)System.DateTime.Today.DayOfWeek;
            foreach (LunchDay day in lunchtable.LunchDays)
            {
                System.Console.WriteLine(day.DateString);
                this.RunOnUiThread(() => horiPager.AddView(new LunchDayView(day).GetView(this, day_value)));
            }
            horiPager.SetCurrentScreen(day_value, false);
        }

    }
}