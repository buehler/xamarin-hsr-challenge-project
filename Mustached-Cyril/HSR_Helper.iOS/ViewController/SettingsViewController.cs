using System;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public class SettingsViewController : DefaultDialogViewController
	{
		public SettingsViewController () : base(new RootElement("Einstellungen"))
		{
			var userEntry = new EntryElement ("Benutzername", "benutzername", ApplicationSettings.Instance.UserCredentials.Name);
			var passwordEntry = new EntryElement ("Passwort", "passwort", ApplicationSettings.Instance.UserCredentials.Name, true);
			var deleteLocalFiles = new StyledStringElement ("Lokale Daten löschen", DeleteLocalFiles){
				BackgroundColor = ApplicationColors.DANGER_BUTTON,
				TextColor = MonoTouch.UIKit.UIColor.White
			};
			userEntry.AutocorrectionType = passwordEntry.AutocorrectionType = MonoTouch.UIKit.UITextAutocorrectionType.No;
			userEntry.AutocapitalizationType = passwordEntry.AutocapitalizationType = MonoTouch.UIKit.UITextAutocapitalizationType.None;
			userEntry.Changed += UsernameChanged;
			passwordEntry.Changed += PasswordChanged;
			Root.Add (new Section ("Benutzerinformationen"){
				userEntry,
				passwordEntry
			});
			Root.Add (new Section ("Daten löschen"){
				deleteLocalFiles
			});
			Title = "Einstellungen";
			NavigationItem.Title = "Einstellungen";
		}

		private void UsernameChanged (object s, EventArgs e)
		{
			Console.WriteLine ("Username changed");
			var field = s as EntryElement;
			if (field != null) {
				ApplicationSettings.Instance.UserCredentials.Name = field.Value;
				ApplicationSettings.Instance.Persistency.Save (ApplicationSettings.Instance.UserCredentials);
			}
		}

		private void PasswordChanged (object s, EventArgs e)
		{
			Console.WriteLine ("PW Changed");
			var field = s as EntryElement;
			if (field != null) {
				ApplicationSettings.Instance.UserCredentials.Password = field.Value;
				ApplicationSettings.Instance.Persistency.Save (ApplicationSettings.Instance.UserCredentials);
			}
		}

		private void DeleteLocalFiles ()
		{
			Console.WriteLine ("Delete Local Files!!");
		}
	}
}

