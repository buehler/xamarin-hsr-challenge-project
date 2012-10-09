
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.iOS;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public partial class SettingsViewController : UIViewController
	{
		private IPhoneUserInformation userInformation;
		private DefaultDialogViewController vc;

		public SettingsViewController () : base ("SettingsView", null)
		{
			Title = "Einstellungen";
			NavigationItem.Title = "Einstellungen";
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			vc = GetSettingsView ();
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND_COLOR;
			SettingsView = vc.View;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private DefaultDialogViewController GetSettingsView ()
		{
			if (userInformation == null) {
				userInformation = new IPhoneUserInformation ();
			}
			var userEntry = new EntryElement ("Benutzername", "benutzername", "");
			var passwordEntry = new EntryElement ("Passwort", "passwort", "", true);
			
			userEntry.Changed += FUDI;
			passwordEntry.Changed += FUDI;
			
			var root = new RootElement ("Einstellungen"){
				new CustomFontSection("Benutzerinformationen", 16){
					userEntry,
					passwordEntry
				}
			};
			return new DefaultDialogViewController (root);
		}
		
		private void FUDI (object s, EventArgs e)
		{
			Console.WriteLine ("NOW from: " + s.ToString ());
		}
	}
}

