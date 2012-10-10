
using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.iOS.Controller;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using MonoTouch.Dialog;
using System.Collections.Generic;

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
			_pageScrollController.AddPage (GetInitScreen ());
			_pageScrollController.OnPageChange += PageChanged;
			if (ApplicationSettings.Instance.Persistency.Exists<Timetable> ())
				TimetableCallback (ApplicationSettings.Instance.Persistency.Load<Timetable> ());
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserTimetable (ApplicationSettings.Instance.UserCredentials, TimetableCallback);
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}

		private DefaultDialogViewController GetInitScreen ()
		{
			var _activityView = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.WhiteLarge);
			_activityView.Frame.Width = 30;
			_activityView.Frame.Height = 30;
			_activityView.Frame.X = 100;
			_activityView.Color = ApplicationColors.PAGECONTROLLER_PAGES;
			_activityView.StartAnimating ();
			return new DefaultDialogViewController (
				new RootElement ("Lade"){
					new Section("lade daten..."),
					new Section("dies kann beim ersten mal etwas dauern..."),
					new Section(""){
						new UIViewElement("", _activityView, true)
					}
				}
			);
		}

		private void PageChanged (int newPage)
		{
			NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
		}

		private void TimetableCallback (Timetable timetable)
		{
			ApplicationSettings.Instance.Persistency.Save (timetable);
			UIApplication.SharedApplication.InvokeOnMainThread (() =>
			{
				_pageScrollController.Clear ();
				foreach (var day in timetable.TimetableDays) {
					if (day.Lessions.Count () == 0)
						continue;
					var root = new RootElement ((string.IsNullOrEmpty (day.Weekday) ? "Ohne Wochentag" : day.Weekday));
					foreach (var lession in day.Lessions) {
						var section = new Section (lession.Name + " " + lession.Type);
						foreach (var alloc in lession.CourseAllocations) {
							string t = alloc.Timeslot;
							if (alloc.RoomAllocations.Count () > 0)
								t += "\n" + alloc.RoomAllocations.FirstOrDefault ().Roomnumber;
							section.Add (new CustomFontMultilineElement (t, lession.LecturersShortVersion));
						}
						root.Add (section);
					}
					_pageScrollController.AddPage (new DefaultDialogViewController (UITableViewStyle.Plain, root));
				}
			});
		}
	}
}

