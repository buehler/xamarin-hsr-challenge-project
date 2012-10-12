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

namespace HSR_Helper.Android
{
    class MyTabsListener
    {
        public Fragment fragment;
 
        public MyTabsListener(Fragment fragment) {
        this.fragment = fragment;
        }
 
        @Override
        public void onTabReselected(Tab tab, FragmentTransaction ft) {
        Toast.makeText(StartActivity.appContext, "Reselected!", Toast.LENGTH_LONG).show();
        }
 
        @Override
        public void onTabSelected(Tab tab, FragmentTransaction ft) {
        ft.replace(R.id.fragment_container, fragment);
        }
 
        @Override
        public void onTabUnselected(Tab tab, FragmentTransaction ft) {
        ft.remove(fragment);
        }
    }
}