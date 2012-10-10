
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.iOS.Controller;
using HSR_Helper.DomainLibrary.Domain.Timetable;

namespace HSR_Helper.iOS
{
	public partial class TimetableViewController : UIViewController
	{
		private PageScrollController<DefaultDialogViewController> _pageScrollController;

		public TimetableViewController () : base ("TimetableView", null)
		{
			Title = "Stundenplan";
			NavigationItem.Title = "Stundenplan";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_pageScrollController = new PageScrollController<DefaultDialogViewController> (ScrollView, PageController);
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserTimetable (ApplicationSettings.Instance.UserCredentials, TimetableCallback);
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}

		private void TimetableCallback (Timetable timetable)
		{
			Console.WriteLine (timetable);
		}


	}
}

