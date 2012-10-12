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

namespace HSR_Helper.Android
{
    [Activity()]
    public class ShowSettings : Activity
    {
        public static Context appContext;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            TextView textView = new TextView(this);
            textView.Text = "Settings";
            SetContentView(textView);
        }
    }
}