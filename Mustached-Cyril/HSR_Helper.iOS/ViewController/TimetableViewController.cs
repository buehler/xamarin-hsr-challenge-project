
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
			_pageScrollController.OnPageChange += PageChanged;
			if (ApplicationSettings.Instance.Persistency.Exists<Timetable> ())
				TimetableCallback (ApplicationSettings.Instance.Persistency.Load<Timetable> (), null);
			else
				_pageScrollController.AddPage (GetNoDataScreen ());

			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}

		private DefaultDialogViewController GetNoDataScreen ()
		{
			return new DefaultDialogViewController (
				new RootElement ("keine daten"){
					new Section("keine daten"){
					new MultilineElement("pull to refresh...\n(dauert ein wenig)")
				}
				}
			, UITableViewStyle.Plain, RefreshRequested);
		}

		private void PageChanged (int newPage)
		{
			NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
		}

		private void TimetableCallback (Timetable timetable, object[] args)
		{
			ApplicationSettings.Instance.Persistency.Save (timetable);
			UIApplication.SharedApplication.InvokeOnMainThread (() =>
			{
				if (args != null) {
					args.ToList ().ForEach (o => (o as DefaultDialogViewController).ReloadComplete ());
				}
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
					var dvc = new DefaultDialogViewController (root, UITableViewStyle.Plain, RefreshRequested);
					dvc.CustomLastUpdate = timetable.LastUpdated;
					_pageScrollController.AddPage (dvc);
				}
			});
		}

		private void RefreshRequested (object s, EventArgs e)
		{
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserTimetable (ApplicationSettings.Instance.UserCredentials, TimetableCallback, new object[]{s});
		}
	}
}

