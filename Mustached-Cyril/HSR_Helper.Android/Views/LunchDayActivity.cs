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
using HSR_Helper.DomainLibrary.Domain.Userinformation;

namespace HSR_Helper.Android
{
    [Activity()]
    public class LunchDayActivity : Activity
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
            SetContentView(horiPager);
            TextView test = new TextView(this);
            test.Text = "Laden...";
            horiPager.AddView(test);
            var _loadedLuchtable = ApplicationSettings.Instance.Persistency.Load<Lunchtable>();
            if (_loadedLuchtable != null && _loadedLuchtable.LunchDays.Count > 0)
            {
                var cal = CultureInfo.CurrentCulture.Calendar;
                int db = cal.GetWeekOfYear(_loadedLuchtable.LastUpdated, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
                int act = cal.GetWeekOfYear(System.DateTime.Today, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);

                if (act <= db)
                {
                    addLunchtable(_loadedLuchtable);
                    DomainLibraryHelper.GetUserBadgeInformation(ApplicationSettings.Instance.UserCredentials, badgeInfoCallback);
                    return;
                }
            }
            DomainLibraryHelper.GetLunchtable(lunchtableCallback);
            DomainLibraryHelper.GetUserBadgeInformation(ApplicationSettings.Instance.UserCredentials, badgeInfoCallback);

        }

        public void badgeInfoCallback(BadgeInformation badgeInfo)
        {
            RunOnUiThread(() => { 
                for (int i = 0; i < horiPager.ChildCount; i++)
                {
                    var view = horiPager.GetChildAt(i);
                    var textField = view.FindViewById<TextView>(Resource.Id.saldo);
                    if (textField != null )
                    {
                        if (badgeInfo != null)
                            textField.Text = badgeInfo.BadgeSaldoString;
                        else
                            textField.Text = "Saldo: 00.00";
                    } 
                }         
            });
            
        }

        public void lunchtableCallback(Lunchtable lunchtable)
        {
            addLunchtable(lunchtable);
            ApplicationSettings.Instance.Persistency.Save(lunchtable);
        }

        public void addLunchtable(Lunchtable lunchtable)
        {
            this.RunOnUiThread(() => horiPager.RemoveAllViews());
            if (lunchtable.NoMenuesFound || lunchtable.LunchDays.Count < 1)
            {
                View dishDay = this.LayoutInflater.Inflate(Resource.Layout.DishDay, null);

                var dayTitle = dishDay.FindViewById<TextView>(Resource.Id.day_text);
                dayTitle.Text = "Keine Menüeinträge!";
                this.RunOnUiThread(() => horiPager.AddView(dishDay));
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 1:
                    DomainLibraryHelper.GetLunchtable(lunchtableCallback);
                    DomainLibraryHelper.GetUserBadgeInformation(ApplicationSettings.Instance.UserCredentials, badgeInfoCallback);
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