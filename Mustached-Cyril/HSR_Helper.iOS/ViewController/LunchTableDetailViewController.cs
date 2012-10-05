
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Domain;

namespace HSR_Helper.iOS
{
	public partial class LunchTableDetailViewController : UIViewController
	{
		public LunchDay LunchDay{get; private set;}
	    
		public LunchTableDetailViewController (LunchDay lunchDay) : base ("LunchTableDetailView", null)
		{
			this.LunchDay = lunchDay;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TestLabel.Text = "";
			TestLabel.Lines = 0;
			TestLabel.LineBreakMode = UILineBreakMode.WordWrap;
			foreach(Dish d in LunchDay.Dishes)
			{
				TestLabel.Text += d.Title + "\n" + d.Description + "\n\n";
			}
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

