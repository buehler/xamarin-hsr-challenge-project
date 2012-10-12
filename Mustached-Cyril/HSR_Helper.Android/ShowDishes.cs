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

namespace HSR_Helper.Android
{
    [Activity(Label = "WTF", Icon = "@drawable/icon")]
    public class ShowDishes : Activity
    {
        public static Context appContext;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            HorizontalScrollView view = new HorizontalScrollView(this);
            LinearLayout lmo = new LinearLayout(this);
            lmo.Id = 1234;
            view.AddView(lmo);
            
            
            SetContentView(view);

            DomainLibraryHelper.GetLunchtable(addLunchtable);

        }

        private void addLunchtable(Lunchtable lunchtable)
        {
            var view1 = FindViewById<LinearLayout>(1234);
            TextView a = new TextView(this);
            a.Text = "aaa";
            view1.AddView(a);
            foreach (LunchDay day in lunchtable.LunchDays)
            {
                System.Console.WriteLine("muuu");
            }
        }

    }
}