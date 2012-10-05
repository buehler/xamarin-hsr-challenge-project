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
		MonoTouch.UIKit.UILabel TestLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TestLabel != null) {
				TestLabel.Dispose ();
				TestLabel = null;
			}
		}
	}
}
