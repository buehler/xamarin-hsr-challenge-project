using System;
using MonoTouch.Dialog;
using HSR_Helper.DomainLibrary.Domain.Timetable;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Persistency;
using System.Collections.Generic;

namespace HSR_Helper.iOS
{
	public class SettingsViewController : DefaultDialogViewController
	{
		public SettingsViewController() : base(new RootElement("Einstellungen"))
		{
			var userEntry = new EntryElement("Benutzername", "benutzername", ApplicationSettings.Instance.UserCredentials.Name);
			var passwordEntry = new EntryElement("Passwort", "passwort", ApplicationSettings.Instance.UserCredentials.Name, true);
			var deleteLocalFiles = new StyledStringElement("Lokale Daten löschen", DeleteLocalFiles){
				BackgroundColor = ApplicationColors.BUTTON_DANGER,
				TextColor = MonoTouch.UIKit.UIColor.White
			};
			userEntry.AutocorrectionType = passwordEntry.AutocorrectionType = MonoTouch.UIKit.UITextAutocorrectionType.No;
			userEntry.AutocapitalizationType = passwordEntry.AutocapitalizationType = MonoTouch.UIKit.UITextAutocapitalizationType.None;
			userEntry.Changed += UsernameChanged;
			passwordEntry.Changed += PasswordChanged;

			Root.Add(new Section("Benutzerinformationen"){
				userEntry,
				passwordEntry
			});

			Root.Add(new Section("Stundenplaneinstellungen"){
				new StyledStringElement("Andere Stundenpläne", () => {NavigationController.PushViewController(new SettingsTimetablesDetailViewController(), true);}){
					Accessory = UITableViewCellAccessory.DisclosureIndicator
				}
			});

			Root.Add(new Section("Daten löschen"){
				deleteLocalFiles
			});

			Title = "Einstellungen";
			NavigationItem.Title = "Einstellungen";
		}

		private void UsernameChanged(object s, EventArgs e)
		{
			Console.WriteLine("Username changed");
			var field = s as EntryElement;
			if (field != null)
			{
				ApplicationSettings.Instance.UserCredentials.Name = field.Value;
				ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
			}
		}

		private void PasswordChanged(object s, EventArgs e)
		{
			Console.WriteLine("PW Changed");
			var field = s as EntryElement;
			if (field != null)
			{
				ApplicationSettings.Instance.UserCredentials.Password = field.Value;
				ApplicationSettings.Instance.Persistency.Save(ApplicationSettings.Instance.UserCredentials);
			}
		}

		private void DeleteLocalFiles()
		{
			Console.WriteLine("Delete Local Files!!");
			ApplicationSettings.Instance.UserCredentials.Name = null;
			ApplicationSettings.Instance.UserCredentials.Password = null;
			foreach (var user in ApplicationSettings.Instance.UserTimetablelist.Usernames)
			{
				ApplicationSettings.Instance.Persistency.Delete(new Timetable(){Username = user});
			}
			ApplicationSettings.Instance.Persistency.Delete(ApplicationSettings.Instance.UserTimetablelist);
		}

	}
}

