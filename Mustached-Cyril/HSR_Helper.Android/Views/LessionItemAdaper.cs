using Android.App;
using Android.Views;
using Android.Widget;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSR_Helper.Android.Views
{
    class LessionItemAdaper: BaseAdapter
    {
        Activity _activity;
        private List<lessionText> _lessionlist;

        public LessionItemAdaper(Activity activity, List<lessionText> lessions)
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
            var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.TimeSlotItem, parent, false);
            var lession = view.FindViewById<TextView> (Resource.Id.text_lesson);
            var lecturers = view.FindViewById<TextView>(Resource.Id.text_lecturers);
            var rooms = view.FindViewById<TextView>(Resource.Id.text_room);
            var txt = _lessionlist[position];
            lession.Text = txt.LessionText;
            rooms.Text = txt.Room;
            lecturers.Text = txt.Lecturers;

            return view;
        }
        
     }
}
