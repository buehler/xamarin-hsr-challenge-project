
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.iOS.Controller;

namespace HSR_Helper.iOS
{
	public partial class TimetableViewController : UIViewController
	{
		private PageScrollController _pageScrollController;

		public TimetableViewController () : base ("TimetableView", null)
		{
			Title = "Stundenplan";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_pageScrollController = new PageScrollController(ScrollView, PageController);
			_pageScrollController.AddPage(new LunchTableDetailViewController("Montag"));
			_pageScrollController.AddPage(new LunchTableDetailViewController("Dienstag"));
			_pageScrollController.AddPage(new LunchTableDetailViewController("Mittwoch"));
			_pageScrollController.AddPage(new LunchTableDetailViewController("Donnerstag"));
			_pageScrollController.AddPage(new LunchTableDetailViewController("Freitag"));
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

