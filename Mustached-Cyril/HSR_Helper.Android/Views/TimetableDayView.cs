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
        bool show_back;
        bool show_next;
        TimetableDay timetableDay { get; set; }

        public TimetableDayView(TimetableDay day, bool show_back, bool show_next)
        {
            this.timetableDay = day;
            this.show_back = show_back;
            this.show_next = show_next;
        }


        public View GetView(Activity activity)
        {
            View view = activity.LayoutInflater.Inflate(Resource.Layout.TimetableDay, null);
            var dayTitle = view.FindViewById<TextView>(Resource.Id.day_text);
            ListView list = view.FindViewById<ListView>(Resource.Id.timetable_list);
            ImageButton btnBack = view.FindViewById<ImageButton>(Resource.Id.previousDay);
            ImageButton btnNext = view.FindViewById<ImageButton>(Resource.Id.nextDay);
            btnBack.Visibility = show_back ? ViewStates.Visible : ViewStates.Invisible;
            btnNext.Visibility = show_next ? ViewStates.Visible : ViewStates.Invisible;
            


            if (timetableDay.Weekday.Length < 1)
            {
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
                list.Adapter = adapter;
            }
            else
            {

                dayTitle.Text = timetableDay.Weekday;

                //dishDay.SetBackgroundDrawable(context.Resources.GetDrawable(Resource.Drawable.whitey));
                var adapter = new TimetableItemAdapter(activity, timetableDay.Lessions);
                list.Adapter = adapter;

            }
            return view;

        }


    }
}