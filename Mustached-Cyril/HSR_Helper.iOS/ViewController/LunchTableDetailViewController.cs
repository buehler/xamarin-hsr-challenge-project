
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Domain;

namespace HSR_Helper.iOS
{
	public partial class LunchTableDetailViewController : UIViewController
	{
		private readonly LunchDay _lunchDay;
	    public string MENU;
		public LunchTableDetailViewController (LunchDay lunchDay) : base ("LunchTableDetailView", null)
		{
			_lunchDay = lunchDay;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			string a = "";
			foreach(Dish d in _lunchDay.Dishes)
			{
				a += d.Title + "\n" + d.Description+"\n\n";
			}
			MenuText.Text = MENU = a;
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

