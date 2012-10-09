using System;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public class SettingsViewController : DefaultDialogViewController
	{
		public SettingsViewController () : base(new RootElement("Einstellungen"))
		{
			var userEntry = new EntryElement ("Benutzername", "benutzername", "");
			var passwordEntry = new EntryElement ("Passwort", "passwort", "", true);
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
		}

		private void PasswordChanged (object s, EventArgs e)
		{
			Console.WriteLine ("PW Changed");
		}
	}
}

