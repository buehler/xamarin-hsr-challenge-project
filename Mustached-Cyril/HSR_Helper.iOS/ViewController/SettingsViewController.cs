using System;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public class SettingsViewController : DefaultDialogViewController
	{
		public SettingsViewController () : base(new RootElement("Einstellungen"))
		{
			var userEntry = new EntryElement ("Benutzername", "benutzername", ApplicationSettings.Instance.UserInformation.Name);
			var passwordEntry = new EntryElement ("Passwort", "passwort", ApplicationSettings.Instance.UserInformation.Name, true);
			userEntry.Changed += UsernameChanged;
			passwordEntry.Changed += PasswordChanged;
			Root.Add (new CustomFontSection ("Benutzerinformationen", 16){
				userEntry,
				passwordEntry
			});
			Title = "Einstellungen";
			NavigationItem.Title = "Einstellungen";
		}

		private void UsernameChanged (object s, EventArgs e)
		{
			Console.WriteLine ("Username changed");
			var field = s as EntryElement;
			if (field != null) {
				ApplicationSettings.Instance.UserInformation.Name = field.Value;
				ApplicationSettings.Instance.Persistency.Save (ApplicationSettings.Instance.UserInformation);
			}
		}

		private void PasswordChanged (object s, EventArgs e)
		{
			Console.WriteLine ("PW Changed");
			var field = s as EntryElement;
			if (field != null) {
				ApplicationSettings.Instance.UserInformation.Password = field.Value;
				ApplicationSettings.Instance.Persistency.Save (ApplicationSettings.Instance.UserInformation);
			}
		}
	}
}

