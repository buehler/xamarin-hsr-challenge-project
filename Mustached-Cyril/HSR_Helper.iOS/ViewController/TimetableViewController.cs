
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
        private string _userName;
        private Timetable _loadedTimetable;

        public TimetableViewController(string userName) : base ("TimetableView", null)
        {
            _userName = userName;
            Title = "Stundenplan";
            NavigationItem.Title = "Stundenplan";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _pageScrollController = new PageScrollController<DefaultDialogViewController>(ScrollView, PageController);
            _pageScrollController.OnPageChange += PageChanged;
            if (ApplicationSettings.Instance.Persistency.Exists(new Timetable(){Username = _userName}))
                LoadTimetable(ApplicationSettings.Instance.Persistency.Load(new Timetable(){Username = _userName}));
            else
                _pageScrollController.AddPage(GetNoDataScreen());

            View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
        }

        private DefaultDialogViewController GetNoDataScreen()
        {
            return new DefaultDialogViewController(
				new RootElement("keine daten"){
					new Section("keine daten"){
					new MultilineElement("pull to refresh...\n(dauert ein wenig)")
				} 
				}
			, UITableViewStyle.Plain, RefreshRequested);
        }

        private void PageChanged(int newPage)
        {
            NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
        }

        private void LoadTimetable(Timetable timetable)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                _pageScrollController.Clear();
                if (timetable.BlockedTimetable)
                {
                    var root = new RootElement("Blockiert"){
                        new Section("Blockiert"){
                            new MultilineElement("Benutzer hat Stundenplan\nblockiert")
                        }
                    };
                    var dvc = new DefaultDialogViewController(root, UITableViewStyle.Plain, RefreshRequested);
                    dvc.CustomLastUpdate = timetable.LastUpdated;
                    _pageScrollController.AddPage(dvc);
                }
                else if (timetable.HasError)
                {
                    var root = new RootElement("Error"){
                        new Section("Error"){
                            new MultilineElement(timetable.ErrorMessage)
                        }
                    };
                    var dvc = new DefaultDialogViewController(root, UITableViewStyle.Plain, RefreshRequested);
                    dvc.CustomLastUpdate = timetable.LastUpdated;
                    _pageScrollController.AddPage(dvc);
                }
                else
                {
                    foreach (var day in timetable.TimetableDays)
                    {
                        if (day.Lessions.Count() == 0)
                            continue;
                        var root = new RootElement((string.IsNullOrEmpty(day.Weekday) ? "Ohne Wochentag" : day.Weekday));
                        foreach (var lession in day.Lessions)
                        {
                            var section = new Section(lession.Name + " " + lession.Type);
                            foreach (var alloc in lession.CourseAllocations)
                            {
                                string t = alloc.Timeslot;
                                if (alloc.RoomAllocations.Count() > 0)
                                    t += "\n" + alloc.RoomAllocations.FirstOrDefault().Roomnumber;
                                var tmpLession = new Lession(){CourseAllocations=lession.CourseAllocations,
                                                               Lecturers=lession.Lecturers,
                                                               Name=lession.Name,
                                                               Type=lession.Type};
                                section.Add(new MultilineElement(t, () => {

                                    OnElementTappet(tmpLession);}){Value=lession.LecturersShortVersion});
                            }
                            root.Add(section);
                        }
                        var dvc = new DefaultDialogViewController(root, UITableViewStyle.Plain, RefreshRequested);
                        dvc.CustomLastUpdate = timetable.LastUpdated;
                        _pageScrollController.AddPage(dvc);
                    }
                }
            });
            _loadedTimetable = timetable;
        }

        private void TimetableCallback(Timetable timetable, object[] args)
        {
            if (!timetable.Equals(_loadedTimetable))
            {
                if (!string.IsNullOrEmpty(timetable.Username)) 
                    ApplicationSettings.Instance.Persistency.Save(timetable);
                LoadTimetable(timetable);
            }
            else
            {
                UIApplication.SharedApplication.InvokeOnMainThread(() => {
                    if (args != null)
                    {
                        foreach (var o in args)
                        {
                            var obj = o as DefaultDialogViewController;
                            if (obj != null)
                            {
                                obj.ReloadComplete();
                            }
                        }
                    }
                });
            }
        }

        private void RefreshRequested(object s, EventArgs e)
        {
            HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserTimetable(ApplicationSettings.Instance.UserCredentials, _userName, TimetableCallback, new object[]{s});
        }

        private void OnElementTappet(Lession lession)
        {
            var root = new RootElement(lession.Name);
            var allocations = new Section("Termine");
            foreach (CourseAllocation alloc in lession.CourseAllocations)
            {
                string t = alloc.Timeslot;
                if (!string.IsNullOrEmpty(alloc.Type))
                    t += "\n" + alloc.Type;
                if (!string.IsNullOrEmpty(alloc.Description))
                    t += "\n" + alloc.Description;
                allocations.Add(new MultilineElement(t, (alloc.RoomAllocations.Count() > 0 ? alloc.RoomAllocations.FirstOrDefault().Roomnumber : string.Empty)));
            }
            root.Add(allocations);
            var lecturers = new Section("Betreuer");
            foreach (Lecturer l in lession.Lecturers)
            {
                lecturers.Add(new StringElement(l.Fullname, l.Shortname));
            }
            root.Add(lecturers);
            NavigationController.PushViewController(new DefaultDialogViewController(root), true);
        }
    }
}

