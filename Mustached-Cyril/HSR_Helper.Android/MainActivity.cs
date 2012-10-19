using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HSR_Helper.DomainLibrary.Domain;
using HSR_Helper.DomainLibrary.Helper;

namespace HSR_Helper.Android
{
    [Activity(MainLauncher = true, Label = "@string/ApplicationName")]
    public class MainActivity : TabActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            TabHost.TabSpec spec;     // Resusable TabSpec for each tab
            Intent intent;            // Reusable Intent for each tab

            // Create an Intent to launch an Activity for the tab
            intent = new Intent(this, typeof(ShowDishes));
            intent.AddFlags(ActivityFlags.NewTask);

            // Initialize a TabSpec for each tab and add it to the TabHost
            spec = TabHost.NewTabSpec("lunchtable");
            spec.SetIndicator(Resources.GetString(Resource.String.lunchtable), Resources.GetDrawable(Resource.Drawable.ic_tab_dish));
            spec.SetContent(intent);
            TabHost.AddTab(spec);

            intent = new Intent(this, typeof(ShowTimeTable));
            intent.AddFlags(ActivityFlags.NewTask);
            spec = TabHost.NewTabSpec("timetables");
            spec.SetIndicator(Resources.GetString(Resource.String.timetable), Resources.GetDrawable(Resource.Drawable.ic_tab_dish));
            spec.SetContent(intent);
            TabHost.AddTab(spec);

            intent = new Intent(this, typeof(ShowSettings));
            intent.AddFlags(ActivityFlags.NewTask);
            spec = TabHost.NewTabSpec("settings");
            spec.SetIndicator(Resources.GetString(Resource.String.settings), Resources.GetDrawable(Resource.Drawable.ic_tab_dish));
            spec.SetContent(intent);
            TabHost.AddTab(spec);

            TabHost.CurrentTab = 0;
        }
    }
}

