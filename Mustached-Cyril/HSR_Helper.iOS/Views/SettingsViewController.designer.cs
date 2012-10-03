// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace HSR_Helper.iOS
{
	[Register ("SettingsView")]
	partial class SettingsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField Username { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField Password { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Username != null) {
				Username.Dispose ();
				Username = null;
			}

			if (Password != null) {
				Password.Dispose ();
				Password = null;
			}
		}
	}
}
