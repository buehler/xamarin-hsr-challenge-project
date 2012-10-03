
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public partial class LunchTableDetailViewController : UIViewController
	{
		private readonly string _weekday;
		public LunchTableDetailViewController (string weekday) : base ("LunchTableDetailView", null)
		{
			_weekday = weekday;
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
			Weekday.Text = _weekday;
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

