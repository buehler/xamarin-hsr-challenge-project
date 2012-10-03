
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace HSR_Helper.iOS
{
	public partial class LunchTableViewController : UIViewController
	{
		public LunchTableViewController () : base ("LunchTableView", null)
		{
			Title = "Menü";
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
			CreateMenuPanels();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void CreateMenuPanels()
		{
			ScrollView.Scrolled += ScrollViewScrolled;
			int count = 5;
			RectangleF scrollFrame = ScrollView.Frame;
			scrollFrame.Width = scrollFrame.Width * count;
			ScrollView.ContentSize = scrollFrame.Size;
			AddMenuView("Montag");
			AddMenuView("Dienstag");
			AddMenuView("Mittwoch");
			AddMenuView("Donnerstag");
			AddMenuView("Freitag");
			PageController.Pages = count;
		}

		private void AddMenuView(Object menu)
		{
			RectangleF lastFrame = ScrollView.Frame;
			PointF lastLocation = new PointF();
			Console.WriteLine (lastFrame.Width);
			lastLocation.X = lastFrame.Width * ScrollView.Subviews.Count();
			lastFrame.Location = lastLocation;
			LunchTableDetailViewController controller = new LunchTableDetailViewController(menu.ToString());
			controller.View.Frame = lastFrame;
			ScrollView.AddSubview(controller.View);
		}

		private void ScrollToPage(int pageNumber){
			PointF newPage = new PointF(0,0);
			newPage.X = ScrollView.Frame.Width * pageNumber;
			ScrollView.SetContentOffset(newPage,true);
		}

		private void ScrollViewScrolled (object sender, EventArgs e)
		{
			double page = Math.Floor((ScrollView.ContentOffset.X - ScrollView.Frame.Width / 2) / ScrollView.Frame.Width) + 1;
			PageController.CurrentPage = (int)page;
		}
	}
}

