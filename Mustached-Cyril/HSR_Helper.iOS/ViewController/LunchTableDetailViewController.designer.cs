// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HSR_Helper.iOS
{
	[Register ("LunchTableDetailView")]
	partial class LunchTableDetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel MenuText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Weekday { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MenuText != null) {
				MenuText.Dispose ();
				MenuText = null;
			}

			if (Weekday != null) {
				Weekday.Dispose ();
				Weekday = null;
			}
		}
	}
}
