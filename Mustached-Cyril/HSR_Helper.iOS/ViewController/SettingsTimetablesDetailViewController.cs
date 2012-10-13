using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class SettingsTimetablesDetailViewController : DefaultDialogViewController
	{
		private UIAlertView _alert;
		private Section _usernameSection = new Section("Benutzernamen");

		public SettingsTimetablesDetailViewController() : base(new RootElement("Stundenpläne"))
		{
			Root.Add(new Section(""){
				new StyledStringElement("Hinzufügen", AddTimetable){
					BackgroundColor = ApplicationColors.BUTTON_NORMAL
				}
			});
			Root.Add(_usernameSection);
			foreach (var username in ApplicationSettings.Instance.UserTimetablelist.Usernames)
			{
				_usernameSection.Add(new StringElement(username));
			}
			Title = NavigationItem.Title = "Stundenpläne";
		}

		private void AddTimetable()
		{
			_alert = new UIAlertView("Benutzername eingeben", "", new AlertDelegate(_usernameSection), "Abbrechen", new string[]{"Ok"});
			_alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			_alert.Show();
		}

		private class AlertDelegate : UIAlertViewDelegate
		{
			private Section _usernameSection;

			public AlertDelegate(Section usernameSection)
			{
				_usernameSection = usernameSection;
			}

			public override void Dismissed(UIAlertView alertView, int buttonIndex)
			{
				if (buttonIndex == 1 && !ApplicationSettings.Instance.UserTimetablelist.Usernames.Contains(alertView.GetTextField(0).Text) && !String.IsNullOrEmpty(alertView.GetTextField(0).Text))
				{
					ApplicationSettings.Instance.UserTimetablelist.Usernames.Add(alertView.GetTextField(0).Text);
					ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserTimetablelist);
					_usernameSection.Add(new StringElement(alertView.GetTextField(0).Text));
				}
			}
		}
	}
}

