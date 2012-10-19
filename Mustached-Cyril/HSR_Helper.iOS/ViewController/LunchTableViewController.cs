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
        private DateTime _lastUpdate;
        private UIPageViewController _pageController;

        public LunchTableViewController() : base ("LunchTableView", null)
        {
            Title = "Menü";
            NavigationItem.Title = "Menü";
            TabBarItem.Image = UIImage.FromBundle("Menu-icon");
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //_pageScrollController = new PageScrollController<DialogViewController>(ScrollView, PageController);
            //_pageScrollController.OnPageChange += PageChanged;


            this._pageController = new UIPageViewController(UIPageViewControllerTransitionStyle.PageCurl,
                                                            UIPageViewControllerNavigationOrientation.Horizontal,
                                                            UIPageViewControllerSpineLocation.Min);
            this._pageController.View.Frame = this.PageView.Bounds;
            this.PageView.AddSubview(this._pageController.View);
            _pageController.DataSource = new PageControllerDatasource();

            if (ApplicationSettings.Instance.Persistency.Exists<Lunchtable>())
                LoadLunchtable(ApplicationSettings.Instance.Persistency.Load<Lunchtable>());
            PageView.BackgroundColor = View.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
            BadgeSaldo.BackgroundColor = ApplicationColors.NAVIGATIONBAR;
            HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetUserBadgeInformation(ApplicationSettings.Instance.UserCredentials, BadgeInformationCallback);

        }

        public override void ViewDidAppear(bool animated)
        {
            if (_lastUpdate == DateTime.MinValue || DateTime.Now.DayOfYear != _lastUpdate.DayOfYear)
                HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(LunchtableCallback);
            base.ViewDidAppear(animated);
        }

        private void PageChanged(int newPage)
        {
            //NavigationItem.Title = _pageScrollController [newPage].Root.Caption;
        }

        private void LoadLunchtable(Lunchtable lunchtable)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                if ((_pageController.DataSource as PageControllerDatasource).Lunchtable == null)
                {
                    _pageController.SetViewControllers(new UIViewController[] { CreateView(lunchtable.LunchDays [1]) }, UIPageViewControllerNavigationDirection.Forward, false, s => { });
                }
                (_pageController.DataSource as PageControllerDatasource).Lunchtable = lunchtable;
                _pageController.ReloadInputViews();
            });
            _loadedTimetable = lunchtable;
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
        private void LunchtableCallback(Lunchtable lunchtable)
        {
            if (!lunchtable.Equals(_loadedTimetable))
            {
                ApplicationSettings.Instance.Persistency.Save(lunchtable);
                LoadLunchtable(lunchtable);
                _lastUpdate = DateTime.Now;
            }
        }

        private void BadgeInformationCallback(BadgeInformation badgeInformation)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                BadgeSaldo.Text = badgeInformation.BadgeSaldoString;
            });
        }

        private class PageControllerDatasource : UIPageViewControllerDataSource
        {

            public Lunchtable Lunchtable{ get; set; }

            public PageControllerDatasource() : base()
            {
            }

            public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                if (Lunchtable == null)
                    return null;
                return CreateView(Lunchtable.LunchDays [1]);
            }    

            public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                if (Lunchtable == null)
                    return null;
                return CreateView(Lunchtable.LunchDays [1]);
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
}