
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.iOS.Controller;

namespace HSR_Helper.iOS
{
	public partial class TimetableViewController : UIViewController
	{
		//private PageScrollController<TimetableDetailViewController> _pageScrollController;

		public TimetableViewController () : base ("TimetableView", null)
		{
			Title = "Stundenplan";
			NavigationItem.Title = "Stundenplan";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//_pageScrollController = new PageScrollController<TimetableDetailViewController>(ScrollView, PageController);
			// Perform any additional setup after loading the view, typically from a nib.
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}
	}
}

