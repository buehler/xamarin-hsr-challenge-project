
using System;
using System.Drawing;
using HSR_Helper.iOS.Controller;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using HSR_Helper.DomainLibrary.Domain;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public partial class LunchTableViewController : UIViewController
	{
		private PageScrollController<DialogViewController> _pageScrollController;

		public LunchTableViewController () : base ("LunchTableView", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//NavigationController.NavigationBar.TintColor = UIColor.Blue;
			_pageScrollController = new PageScrollController<DialogViewController>(ScrollView, PageController);
			_pageScrollController.OnPageChange += PageChanged;
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(LunchtableCallback);
            Title = "Menü";
			NavigationItem.Title = "Menü";
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private void PageChanged(int newPage)
		{
            NavigationItem.Title = _pageScrollController[newPage].Root.Caption;
		}

		private void LunchtableCallback(Lunchtable lunchtable)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			                                                   {
				foreach(LunchDay lunchDay in lunchtable.LunchDays)
				{
					_pageScrollController.AddPage(CreateView(lunchDay));
				}
			});
		}

		private DialogViewController CreateView(LunchDay lunchDay)
		{
			if(lunchDay == null){
				return new DialogViewController(new RootElement("BLUB"){
					new Section("Kein Eintrag"){
						new MultilineElement("Kein Menü gefunden")
					}
				});
			}
			var root = new RootElement(lunchDay.DateString);
			foreach(Dish d in lunchDay.Dishes)
			{
				var section = new Section(d.Title)
				{
					new MultilineElement(d.Description),
					new MultilineElement("Preis", d.PriceInternal)
				};
				root.Add(section);
			}
			var m = new DialogViewController(root);
//			m.TableView.BackgroundView = null;
//			m.TableView.BackgroundColor = new UIColor(0f,0.3f,0.7f,1f);
			return m;
		}
	}
}

