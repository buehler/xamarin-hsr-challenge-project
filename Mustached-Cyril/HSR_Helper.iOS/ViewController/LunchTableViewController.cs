
using System;
using System.Drawing;
using HSR_Helper.iOS.Controller;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using HSR_Helper.DomainLibrary.Domain.Userinformation;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public partial class LunchTableViewController : UIViewController
	{
		private PageScrollController<DialogViewController> _pageScrollController;

		public LunchTableViewController () : base ("LunchTableView", null)
		{
			Title = "Menü";
			NavigationItem.Title = "Menü";
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_pageScrollController = new PageScrollController<DialogViewController> (ScrollView, PageController);
			_pageScrollController.OnPageChange += PageChanged;
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable (LunchtableCallback);
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}

		private void PageChanged (int newPage)
		{
			NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
		}

		private void LunchtableCallback (Lunchtable lunchtable)
		{
			UIApplication.SharedApplication.InvokeOnMainThread (() =>
			{
				foreach (LunchDay lunchDay in lunchtable.LunchDays) {
					_pageScrollController.AddPage (CreateView (lunchDay));
				}
			});
		}

		private DialogViewController CreateView (LunchDay lunchDay)
		{
			if (lunchDay == null) {
				return new DialogViewController (new RootElement ("BLUB"){
					new Section("Kein Eintrag"){
						new MultilineElement("Kein Menü gefunden")
					}
				});
			}
			var root = new RootElement (lunchDay.DateString);
			foreach (Dish d in lunchDay.Dishes) {
				var section = new CustomFontSection (d.Title)
				{
					new CustomFontMultilineElement(d.Description, 15, CustomFontMultilineElement.FontStyle.Normal),
					new CustomFontMultilineElement("", d.PriceInternal, 14, CustomFontMultilineElement.FontStyle.Bold, CustomFontMultilineElement.FontStyle.Bold)
				};
				root.Add (section);
			}

			return new DefaultDialogViewController (root);
		}
	}
}

