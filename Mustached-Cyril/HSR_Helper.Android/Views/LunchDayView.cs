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
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using Android.Content.Res;
using Android.Util;

namespace HSR_Helper.Android.Views1
{
    class LunchDayView
    {
        LunchDay lunchDay { get; set; }
        public LunchDayView(LunchDay day)
        {
            this.lunchDay = day;
        }


        public View GetView(Activity activity, int index)
        {
            var dishDay = activity.LayoutInflater.Inflate(Resource.Layout.DishDay, null);

            if (index == 0)
            {
                var previous = dishDay.FindViewById<ImageButton>(Resource.Id.previousDay);
                previous.Visibility = ViewStates.Invisible;
                previous.Enabled = false;
            }
            if (index == 5)
            {
                var next = dishDay.FindViewById<ImageButton>(Resource.Id.nextDay);
                next.Visibility = ViewStates.Invisible;
                next.Enabled = false;
            }
            var dayTitle = dishDay.FindViewById<TextView>(Resource.Id.day_text);
            dayTitle.Text = lunchDay.DateString;

            //dishDay.SetBackgroundDrawable(context.Resources.GetDrawable(Resource.Drawable.whitey));
            var adapter = new DishItemAdapter(activity, lunchDay.Dishes);
            var list = dishDay.FindViewById<ListView>(Resource.Id.dish_list);
            list.Adapter = adapter;

            return dishDay;
        }


    }
}