
using System;
using MonoTouch.Dialog;
using System.Collections.Generic;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Domain.Userinformation;

namespace HSR_Helper.iOS
{
    public class TimetableMasterViewController : DefaultDialogViewController
    {
        private StyledStringElement _ownTimetable;
        private Section _otherTimetables;
        private UserTimetableList _loadedList;

        public TimetableMasterViewController() : base(new RootElement("Stundenpläne"))
        {
            Title = "Stundenpläne";
            NavigationItem.Title = "Stundenpläne";
            TabBarItem.Image = UIImage.FromBundle("Timetable-icon");
            ApplicationSettings.Instance.UserCredentials.ObjectChanged += PersistentObjectChanged;
            ApplicationSettings.Instance.UserTimetablelist.ObjectChanged += PersistentObjectChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PersistentObjectChanged(ApplicationSettings.Instance.UserCredentials);
            PersistentObjectChanged(ApplicationSettings.Instance.UserTimetablelist);
        }

        private void PersistentObjectChanged(PersistentObject obj)
        {
            if (IsViewLoaded)
            {
                if (obj.GetType() == typeof(UserCredentials))
                {
                    if (_ownTimetable == null)
                    {
                        _ownTimetable = new StyledStringElement(ApplicationSettings.Instance.UserCredentials.Name, OnOwnTapped){
                            Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator
                        };
                        Root.Add(new Section("Eigener Stundenplan"){_ownTimetable});
                    } else
                    {
                        _ownTimetable.Caption = ApplicationSettings.Instance.UserCredentials.Name;
                    }
                } else if (obj.GetType() == typeof(UserTimetableList))
                {
                    if (_otherTimetables == null)
                    {
                        _otherTimetables = new Section("Andere Stundenpläne");
                        Root.Add(_otherTimetables);
                    } else if (obj.Equals(_loadedList))
                    {
                        _otherTimetables.Clear();
                        foreach (var o in ApplicationSettings.Instance.UserTimetablelist.Usernames)
                        {
                            _otherTimetables.Add(new StyledStringElement(o, () => {
                                OnTapped(o);
                            }){
							    Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator
						    });
                        }
                        _loadedList = obj as UserTimetableList;
                    }
                }
                this.ReloadData();
            }
        }

        private void OnOwnTapped()
        {
            if (!string.IsNullOrEmpty(ApplicationSettings.Instance.UserCredentials.Name))
                OnTapped(ApplicationSettings.Instance.UserCredentials.Name);
        }

        private void OnTapped(string username)
        {
            NavigationController.PushViewController(new TimetableViewController(username), true);
        }
    }
}

