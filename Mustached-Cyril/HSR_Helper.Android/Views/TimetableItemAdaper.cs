using Android.App;
using Android.Views;
using Android.Widget;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSR_Helper.Android.Views
{
    class TimetableItemAdapter: BaseAdapter
    {
        Activity _activity;
        private Activity activity;
        private List<Lession> _lessionlist;

        public TimetableItemAdapter(Activity activity, List<Lession> lessions)
        {
            _activity = activity;
            _lessionlist = lessions;
        }
        
        public override int Count {
            get { return _lessionlist.Count; }
        }

        public override Java.Lang.Object GetItem (int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _lessionlist[position].GetHashCode();
        }
        
        public override View GetView (int position, View convertView, ViewGroup parent)
        {          
            var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.TimetableItem, parent, false);




            var from = view.FindViewById<TextView> (Resource.Id.text_from);
            var lession = view.FindViewById<TextView> (Resource.Id.text_lesson);
            var lecturers = view.FindViewById<TextView>(Resource.Id.text_lecturers);
            var rooms = view.FindViewById<TextView>(Resource.Id.text_room);

            var time = "";
            var room = "";
            foreach (CourseAllocation all in _lessionlist[position].CourseAllocations)
            {
                time += all.Timeslot + ";";
                foreach (RoomAllocation r in all.RoomAllocations)
                {
                    room += r.Roomnumber + ";";
                }
            }

            from.Text = time.TrimEnd(';');
            lession.Text = _lessionlist[position].Name;
            lecturers.Text = _lessionlist[position].LecturersShortVersion;
            rooms.Text = room.TrimEnd(';');           
            return view;
        }
        
     }
}
