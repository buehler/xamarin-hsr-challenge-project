using Android.App;
using Android.Views;
using Android.Widget;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using System;
using System.Collections.Generic;
using System.Text;

namespace HSR_Helper.Android.Views
{
    class DishItemAdapter: BaseAdapter
    {
        List<Dish> _dishList;
        Activity _activity;

        public DishItemAdapter(Activity activity, List<Dish> dishes)
        {
            _activity = activity;
            _dishList = dishes ;
        }
        
        public override int Count {
            get { return _dishList.Count; }
        }

        public override Java.Lang.Object GetItem (int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _dishList[position].GetHashCode();
        }
        
        public override View GetView (int position, View convertView, ViewGroup parent)
        {          
            var view = convertView ?? _activity.LayoutInflater.Inflate (Resource.Layout.DishItem, parent, false);
            var title = view.FindViewById<TextView> (Resource.Id.dish_title);
            var description = view.FindViewById<TextView> (Resource.Id.description);
            var price = view.FindViewById<TextView>(Resource.Id.price);
            
            title.Text = _dishList[position].Title;
            description.Text = _dishList [position].Description;
            price.Text = _dishList[position].PriceInternal;
            
            return view;
        }
        
     }
}
