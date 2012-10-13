
using System;
using MonoTouch.Dialog;
using System.Collections.Generic;

namespace HSR_Helper.iOS
{
	public class TimetableMasterViewController : DefaultDialogViewController
	{
		private ApplicationSettings.UserTimetableList _userList;

		public TimetableMasterViewController() : base(new RootElement("Stundenpläne"))
		{
			Title = "Stundenpläne";
			NavigationItem.Title = "Stundenpläne";
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
				_userList = ApplicationSettings.Instance.UserTimetablelist;
			}
		}

		private void ButtonClicked(string username)
		{
			NavigationController.PushViewController(new TimetableViewController(username), true);
		}
	}
}

