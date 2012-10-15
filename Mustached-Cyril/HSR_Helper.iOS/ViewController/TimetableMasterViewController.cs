
using System;
using MonoTouch.Dialog;
using System.Collections.Generic;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Persistency;

namespace HSR_Helper.iOS
{
	public class TimetableMasterViewController : DefaultDialogViewController
	{
		private ApplicationSettings.UserTimetableList _userList;

		public TimetableMasterViewController() : base(new RootElement("Stundenpläne"))
		{
			Title = "Stundenpläne";
			NavigationItem.Title = "Stundenpläne";
			TabBarItem.Image = UIImage.FromBundle("Timetable-icon");
			ApplicationSettings.Instance.UserCredentials.ObjectChanged += PersistentObjectChanged;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			if (_userList == null || !_userList.Equals(ApplicationSettings.Instance.UserTimetablelist))
			{
			
				Root.Clear();
				Root.Add(new Section("Eigener Stundenplan"){
				new StyledStringElement(ApplicationSettings.Instance.UserCredentials.Name, () => {ButtonClicked(ApplicationSettings.Instance.UserCredentials.Name);}){
					Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator
				}
			});
				var timetables = new Section("Andere Stundenpläne");
				foreach (var username in ApplicationSettings.Instance.UserTimetablelist.Usernames)
				{
					timetables.Add(new StyledStringElement(username, () => {
						ButtonClicked(username);}){
						Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator
					});
				}
				Root.Add(timetables);
				_userList = new ApplicationSettings.UserTimetableList(){Usernames = new HashSet<string>(ApplicationSettings.Instance.UserTimetablelist.Usernames)};
			}
		}

		private void PersistentObjectChanged(PersistentObject obj)
		{
			if (IsViewLoaded)
			{

			}
		}

		private void ButtonClicked(string username)
		{
			NavigationController.PushViewController(new TimetableViewController(username), true);
		}
	}
}

