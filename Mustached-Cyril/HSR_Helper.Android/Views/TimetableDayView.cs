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
using Android.Content.Res;
using Android.Util;
using HSR_Helper.DomainLibrary.Domain.Timetable;

namespace HSR_Helper.Android.Views
{
    class TimetableDayView
    {
        TimetableDay timetableDay { get; set; }
        public TimetableDayView(TimetableDay day)
        {
            this.timetableDay = day;
        }


        public View GetView(Activity activity)
        {
            if (timetableDay.Weekday.Length < 1)
            {
                var timetableDayView = activity.LayoutInflater.Inflate(Resource.Layout.TimetableDay, null);
                var dayTitle = timetableDayView.FindViewById<TextView>(Resource.Id.day_text);
                dayTitle.Text = "Spezial";

                //dishDay.SetBackgroundDrawable(context.Resources.GetDrawable(Resource.Drawable.whitey));
                List<lessionText> lessionTexts = new List<lessionText>();
                foreach (Lession lession in timetableDay.Lessions)
                {
                    foreach (CourseAllocation allocation in lession.CourseAllocations)
                    {
                        var room = "";
                        foreach (RoomAllocation r in allocation.RoomAllocations)
                        {
                            room += r.Roomnumber + ";";
                        }
                        lessionTexts.Add(new lessionText(lession.Name, lession.LecturersShortVersion, room.TrimEnd(';')));
                    }
                }
                var adapter = new LessionItemAdaper(activity, lessionTexts);
                var list = timetableDayView.FindViewById<ListView>(Resource.Id.timetable_list);
                list.Adapter = adapter;

                return timetableDayView;
            }
            else
            {
                var timetableDayView = activity.LayoutInflater.Inflate(Resource.Layout.TimetableDay, null);
                var dayTitle = timetableDayView.FindViewById<TextView>(Resource.Id.day_text);
                dayTitle.Text = timetableDay.Weekday;

                //dishDay.SetBackgroundDrawable(context.Resources.GetDrawable(Resource.Drawable.whitey));
                var adapter = new TimetableItemAdapter(activity, timetableDay.Lessions);
                var list = timetableDayView.FindViewById<ListView>(Resource.Id.timetable_list);
                list.Adapter = adapter;

                return timetableDayView;
            }

        }


    }
}