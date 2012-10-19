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

namespace HSR_Helper.Android
{
    [Activity(Label = "WTF", Icon = "@drawable/icon")]
    public class ShowDishes : Activity
    {
        public static Context appContext;
        HorizontalPager horiPager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var display = WindowManager.DefaultDisplay;
            horiPager= new HorizontalPager(this.ApplicationContext, display);
            horiPager.ScreenChanged += new ScreenChangedEventHandler(horiPager_ScreenChanged);

            //You can also use:
            /*horiPager.ScreenChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Switched to screen: " + ((HorizontalPager)sender).CurrentScreen);
            };*/
               

            for (int i = 0; i < 5; i++)
            {
                var textView = new TextView(this.ApplicationContext);
                textView.Id = i;
                textView.Text = (i + 1).ToString();
                textView.TextSize = 100;
                textView.Gravity = GravityFlags.Center;
                horiPager.AddView(textView);
            }

            SetContentView(horiPager);

            DomainLibraryHelper.GetLunchtable(addLunchtable);

        }

        void horiPager_ScreenChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Switched to screen: " + ((HorizontalPager)sender).CurrentScreen);
        }




        private void addLunchtable(Lunchtable lunchtable)
        {
            
            foreach (LunchDay day in lunchtable.LunchDays)
            {
                string menu = "";
                foreach (Dish dish in day.Dishes)
                {
                    menu += dish.Title + ":";
                }

                var view = this.FindViewById<TextView>(1);

                view.Text = menu;
                view.TextSize = 11;
                view.Gravity = GravityFlags.Center;
                System.Console.WriteLine(day.DateString);
            }
        }

    }
}