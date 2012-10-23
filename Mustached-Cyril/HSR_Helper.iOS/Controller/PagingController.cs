using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;

namespace HSR_Helper.iOS.Controller
{
    public delegate void PageChangedEventHandler(int newPage);

    public class PagingController
    {
        private UIPageViewController _pageViewController;
        private PageControllerDatasource _dataSource = new PageControllerDatasource();

        public int CurrentPage
        {
            get
            {
                return _dataSource.CurrentPage;
            }
        }

        public int PageCount
        {
            get
            {
                return _dataSource.Controllers.Count();
            }
        }

        public PagingController(UIView showView)
        {
            _pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.PageCurl,
                                                           UIPageViewControllerNavigationOrientation.Horizontal,
                                                           UIPageViewControllerSpineLocation.Min);
            showView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
            _pageViewController.View.Frame = showView.Bounds;
            showView.AddSubview(_pageViewController.View);
            _pageViewController.DataSource = _dataSource;
        }

        public void AddPage(DefaultDialogViewController viewController)
        {
            _dataSource.Controllers.Add(viewController);
            if (PageCount == 0)
            {
                _pageViewController.SetViewControllers(new UIViewController[] { viewController }, UIPageViewControllerNavigationDirection.Forward, false, s => { });
                _pageViewController.ReloadInputViews();
            }
        }

        public void Clear()
        {
            _dataSource.Controllers.Clear();
        }

        private void PageChanged()
        {
            if (OnPageChange != null && PageCount >= 1)
                OnPageChange(CurrentPage);
        }

        public event PageChangedEventHandler OnPageChange;

        private class PageControllerDatasource : UIPageViewControllerDataSource
        {
            public List<DefaultDialogViewController> Controllers = new List<DefaultDialogViewController>();

            public int CurrentPage
            {
                get;
                private set;
            }
            
            public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                if (Controllers.Count() == 0)
                    return null;
                var index = Controllers.IndexOf(referenceViewController as DefaultDialogViewController);
                if (index == -1 || index == 0)
                    return null;
                CurrentPage = index - 1;
                return Controllers [index - 1];
            }    
            
            public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                if (Controllers.Count() == 0)
                    return null;
                var index = Controllers.IndexOf(referenceViewController as DefaultDialogViewController);
                if (index == -1 || index == Controllers.Count() - 1)
                    return null;
                CurrentPage = index + 1;
                return Controllers [index + 1];
            }
        }
    }
}

