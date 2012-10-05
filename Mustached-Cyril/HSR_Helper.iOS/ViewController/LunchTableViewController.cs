
using System;
using System.Drawing;
using HSR_Helper.iOS.Controller;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using HSR_Helper.DomainLibrary.Domain;

namespace HSR_Helper.iOS
{
	public partial class LunchTableViewController : UIViewController
	{
	    private PageScrollController _pageScrollController;

		public LunchTableViewController () : base ("LunchTableView", null)
		{
			Title = "Menü";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            _pageScrollController = new PageScrollController(ScrollView, PageController);
			_pageScrollController.OnPageChange += PageChanged;
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(LunchtableCallback);
			NavigationItem.Title="Menü";
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void PageChanged(int newPage)
		{

		}

		private void LunchtableCallback(Lunchtable lunchtable)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			                                                   {
				foreach(LunchDay lunchDay in lunchtable.LunchDays)
				{
					_pageScrollController.AddPage(new LunchTableDetailViewController(lunchDay));
				}
			});
		}
	}
}

