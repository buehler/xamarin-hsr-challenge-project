using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace HSR_Helper.Android
{
    [Activity(Label = "HSR_Helper.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            //Create the user interface in code
            var layout = new LinearLayout (this);
            layout.Orientation = Orientation.Vertical;

            var aLabel = new TextView (this);
            aLabel.SetText(Resource.String.helloLabelText);

            var aButton = new Button (this);
            aButton.Text = "Say Hello";
            aButton.Click += (sender, e) => {
                aLabel.SetText(Resource.String.helloButtonText);
            };

            layout.AddView(aLabel);
            layout.AddView(aButton);

            SetContentView(layout);
        }
    }
}

