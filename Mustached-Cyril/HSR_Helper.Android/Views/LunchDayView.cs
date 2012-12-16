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

namespace HSR_Helper.Android.Views
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