using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		private UIWindow _window;
		private UITabBarController _tabBarController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			UINavigationBar.Appearance.TintColor = ApplicationColors.NAVIGATIONBAR_COLOR;
			_tabBarController = new UITabBarController
			                        {
			                            ViewControllers = new UIViewController[]
			                                                  {
															  	  new UINavigationController(new LunchTableViewController()),
																  new UINavigationController(new TimetableViewController()),
																  new UINavigationController(GetSettingsView())
			                                                  }
			                        };

			_window.RootViewController = _tabBarController;
			_window.MakeKeyAndVisible ();
			
			return true;
		}

		private DefaultDialogViewController GetSettingsView ()
		{
//			if (userInformation == null) {
//				userInformation = new IPhoneUserInformation ();
//			}
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
			var vc = new DefaultDialogViewController (root);
			vc.Title = "Einstellugen";
			vc.NavigationItem.Title = "Einstellungen";
			return vc;
		}
		
		private void FUDI (object s, EventArgs e)
		{
			Console.WriteLine ("NOW from: " + s.ToString ());
		}
	}
}

