// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HSR_Helper.iOS
{
	[Register ("LunchTableView")]
	partial class LunchTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIView PageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel BadgeSaldo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPageControl PageController { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PageView != null) {
				PageView.Dispose ();
				PageView = null;
			}

			if (BadgeSaldo != null) {
				BadgeSaldo.Dispose ();
				BadgeSaldo = null;
			}

			if (PageController != null) {
				PageController.Dispose ();
				PageController = null;
			}
		}
	}
}
