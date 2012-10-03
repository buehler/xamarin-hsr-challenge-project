
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public partial class SettingsViewController : UIViewController
	{
		public SettingsViewController () : base ("SettingsView", null)
		{
			Title = "Einstellungen";
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Username.ShouldReturn += DismissKeyboard;
			Password.ShouldReturn += DismissKeyboard;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private bool DismissKeyboard(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}
	}
}

