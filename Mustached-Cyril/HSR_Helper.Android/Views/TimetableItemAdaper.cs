using Android.App;
using Android.Views;
using Android.Widget;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSR_Helper.Android.Views
{
    public class TimetableItemAdapter: BaseAdapter
    {
        Activity _activity;
        private List<Lession> _lessionlist;
        private Dictionary<int, LessionSlot> _slotList;


        public TimetableItemAdapter(Activity activity, List<Lession> lessions)
        {
            _activity = activity;
            _lessionlist = lessions;
            _slotList = new Dictionary<int, LessionSlot>();
            _slotList.Add(0, new LessionSlot("07:05","07:50"));
            _slotList.Add(1, new LessionSlot("08:10", "08:55"));
            _slotList.Add(2, new LessionSlot("09:05", "09:50"));
            _slotList.Add(3, new LessionSlot("10:10", "10:55"));
            _slotList.Add(4, new LessionSlot("11:05", "11:50"));
            _slotList.Add(5, new LessionSlot("12:05", "12:50"));
            _slotList.Add(6, new LessionSlot("13:10", "13:55"));
            _slotList.Add(7, new LessionSlot("14:05", "14:50"));
            _slotList.Add(8, new LessionSlot("15:10", "15:55"));
            _slotList.Add(9, new LessionSlot("16:05", "16:50"));
            _slotList.Add(10, new LessionSlot("16:05", "16:50"));
            _slotList.Add(11, new LessionSlot("17:00", "17:45"));
            _slotList.Add(12, new LessionSlot("17:55", "18:40"));
            _slotList.Add(13, new LessionSlot("19:10", "19:55"));
            _slotList.Add(14, new LessionSlot("20:05", "20:50"));

            foreach (Lession lession in lessions)
            {
                foreach (CourseAllocation allocation in lession.CourseAllocations)
                {
                    var lessionSlot = _slotList[getPositionOfTime(allocation.Timeslot)];
                    var room = "";
                    foreach (RoomAllocation r in allocation.RoomAllocations)
                    {
                        room += r.Roomnumber + ";";
                    }
                    lessionSlot.Lessions.Add(new lessionText(lession.Name,lession.LecturersShortVersion, room.TrimEnd(';')));
                }
            }


        }

        public static int getPositionOfTime(String Time)
        {
            switch (Time)
            {
                case "7:05 - 7:50": return 0;
                case "8:10 - 8:55": return 1;
                case "9:05 - 9:50": return 2;
                case "10:10 - 10:55": return 3;
                case "11:05 - 11:50": return 4;
                case "12:05 - 12:50": return 5;
                case "13:10 - 13:55": return 6;
                case "14:05 - 14:50": return 7;
                case "15:10 - 15:55": return 8;
                case "16:05 - 16:50": return 9;
                case "17:00 - 17:45": return 10;
                default: return -1;
            }
        }

        
        public override int Count {
            get { return _slotList.Count; }
        }

        public override Java.Lang.Object GetItem (int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _slotList[position].GetHashCode();
        }
        
        public override View GetView (int position, View convertView, ViewGroup parent)
        {          
            var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.TimetableItem, parent, false);
            var from = view.FindViewById<TextView> (Resource.Id.text_from);
            var to = view.FindViewById<TextView>(Resource.Id.text_to);
            var lessionList = view.FindViewById<ListView>(Resource.Id.lession_list);

            var Slot = _slotList[position];
            var adapter = new LessionItemAdaper(_activity, Slot.Lessions);
            from.Text = Slot.From;
            to.Text = Slot.To;
            lessionList.Adapter = adapter;
            return view;
        }
        
     }

    class LessionSlot
    {
        public String From { get; set; }
        public String To { get; set; }
        public List<lessionText> Lessions { get; set; }

        public LessionSlot(String From, String To)
        {
            Lessions = new List<lessionText>();
            this.From = From;
            this.To = To;
        }
    }

    class lessionText
    {
        public String LessionText { get; set; }
        public String Lecturers { get; set; }
        public String Room { get; set; }

        public lessionText(String LessionText, String Lecturers, String Room)
        {
            this.LessionText = LessionText;
            this.Lecturers = Lecturers;
            this.Room = Room;
        }
    }
}
