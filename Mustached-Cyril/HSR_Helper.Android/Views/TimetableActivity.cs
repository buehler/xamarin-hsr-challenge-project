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
            //DomainLibraryHelper.GetUserTimetable();
        }

    }
}