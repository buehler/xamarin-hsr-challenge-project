using System;
using System.Drawing;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using HSR_Helper.iOS.Controller;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using MonoTouch.Dialog;
using HSR_Helper.DomainLibrary.Domain.Userinformation;

namespace HSR_Helper.iOS
{
    public partial class LunchTableViewController : UIViewController
    {
        private PageScrollController<DialogViewController> _pageScrollController;
        private Lunchtable _loadedTimetable;
        private BadgeInformation _loadedBadgeInformation;
        private DateTime _lastUpdate;

        public LunchTableViewController() : base ("LunchTableView", null)
        {
            Title = "Menü";
            NavigationItem.Title = "Menü";
            TabBarItem.Image = UIImage.FromBundle("Menu-icon");
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _pageScrollController = new PageScrollController<DialogViewController>(ScrollView, PageController);
            _pageScrollController.OnPageChange += PageChanged;
            if (ApplicationSettings.Instance.Persistency.Exists<Lunchtable>())
                LoadLunchtable(ApplicationSettings.Instance.Persistency.Load<Lunchtable>());
            if (ApplicationSettings.Instance.Persistency.Exists<BadgeInformation>())
                LoadBadgeInformation(ApplicationSettings.Instance.Persistency.Load<BadgeInformation>());
            else
                BadgeSaldo.Text = (new BadgeInformation()).BadgeSaldoString;
            View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
            BadgeSaldo.BackgroundColor = ApplicationColors.NAVIGATIONBAR;
            BadgeSaldo.TextColor = UIColor.White;
        }

        public override void ViewDidAppear(bool animated)
        {
            if (_lastUpdate == DateTime.MinValue || DateTime.Now.DayOfYear != _lastUpdate.DayOfYear)
                HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(LunchtableCallback);
            HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserBadgeInformation(ApplicationSettings.Instance.UserCredentials, BadgeInformationCallback);
            base.ViewDidAppear(animated);
        }

        private void PageChanged(int newPage)
        {
            NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
        }

        private void LoadLunchtable(Lunchtable lunchtable)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                _pageScrollController.Clear();
                if (lunchtable.HasError)
                {
                    _pageScrollController.AddPage(new DefaultDialogViewController(new RootElement("Error"){
                        new Section("Error"){
                            new MultilineElement(lunchtable.ErrorMessage)
                        }
                    }));
                }
                else
                {
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
                }
            });
            _loadedTimetable = lunchtable;
        }

        private void LunchtableCallback(Lunchtable lunchtable)
        {
            if (!lunchtable.Equals(_loadedTimetable))
            {
                ApplicationSettings.Instance.Persistency.Save(lunchtable);
                LoadLunchtable(lunchtable);
                _lastUpdate = DateTime.Now;
            }
        }

        private void LoadBadgeInformation(BadgeInformation badgeInformation)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                if (badgeInformation.HasError)
                {
                    if (_loadedBadgeInformation == null && _loadedBadgeInformation.CashAmount == 0)
                        BadgeSaldo.Text = badgeInformation.ErrorMessage;
                }
                else
                {
                    BadgeSaldo.Text = badgeInformation.BadgeSaldoString;
                }
            });
            _loadedBadgeInformation = badgeInformation;
        }

        private void BadgeInformationCallback(BadgeInformation badgeInformation)
        {
            if (!badgeInformation.Equals(_loadedBadgeInformation))
            {
                ApplicationSettings.Instance.Persistency.Save(badgeInformation);
                LoadBadgeInformation(badgeInformation);
            }
        }

        private DialogViewController CreateView(LunchDay lunchDay)
        {
            if (lunchDay == null)
            {
                return new DefaultDialogViewController(new RootElement(""){
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
					new CustomFontMultilineElement("", "CHF " + d.PriceInternal, 14, CustomFontMultilineElement.FontStyle.Bold, CustomFontMultilineElement.FontStyle.Bold)
				};
                root.Add(section);
            }

            return new DefaultDialogViewController(root);
        }
    }
}