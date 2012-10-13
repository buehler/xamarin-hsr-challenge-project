using System;
using System.Drawing;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using HSR_Helper.iOS.Controller;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public partial class LunchTableViewController : UIViewController
	{
		private PageScrollController<DialogViewController> _pageScrollController;

		public LunchTableViewController() : base ("LunchTableView", null)
		{
			Title = "Menü";
			NavigationItem.Title = "Menü";
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			_pageScrollController = new PageScrollController<DialogViewController>(ScrollView, PageController);
			_pageScrollController.OnPageChange += PageChanged;
			if (ApplicationSettings.Instance.Persistency.Exists<Lunchtable>())
				LunchtableCallback(ApplicationSettings.Instance.Persistency.Load<Lunchtable>());
			HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(LunchtableCallback);
			View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}

		private void PageChanged(int newPage)
		{
			NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
		}

		private void LunchtableCallback(Lunchtable lunchtable)
		{
			ApplicationSettings.Instance.Persistency.Save(lunchtable);
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				_pageScrollController.Clear();
				foreach (LunchDay lunchDay in lunchtable.LunchDays)
				{
					_pageScrollController.AddPage(CreateView(lunchDay));
				}
				try
				{
					_pageScrollController.ScrollToPage((int)DateTime.Now.DayOfWeek - 1);
				} catch (Exception)
				{
				}
			});

		}

		private DialogViewController CreateView(LunchDay lunchDay)
		{
			if (lunchDay == null)
			{
				return new DialogViewController(new RootElement(""){
					new Section("Kein Eintrag"){
						new MultilineElement("Kein Menü gefunden")
					}
				});
			}
			var root = new RootElement(lunchDay.DateString);
			foreach (Dish d in lunchDay.Dishes)
			{
				var section = new Section(d.Title)
				{
					new CustomFontMultilineElement(d.Description, 15, CustomFontMultilineElement.FontStyle.Normal),
					new CustomFontMultilineElement("", d.PriceInternal, 14, CustomFontMultilineElement.FontStyle.Bold, CustomFontMultilineElement.FontStyle.Bold)
				};
				root.Add(section);
			}

			return new DefaultDialogViewController(root);
		}
	}
}