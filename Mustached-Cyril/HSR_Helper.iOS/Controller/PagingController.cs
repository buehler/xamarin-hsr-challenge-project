using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using HSR_Helper.DomainLibrary.Domain.Lunchtable;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS.Controller
{
    public delegate void PageChangedEventHandler(int newPage);

    public class PagingController : UIPageViewController
    {
        //private UIPageViewController _pageViewController;
        private PageControllerDatasource _dataSource = new PageControllerDatasource();
        private UIPageControl _pageControl;

        public int CurrentPage
        {
            get
            {
                return (_dataSource.CurrentPage == 0 ? 1 : _dataSource.CurrentPage);
            }
        }

        public int PageCount
        {
            get
            {
                return (_dataSource.Controllers.Count() == 0 ? 1 : _dataSource.Controllers.Count());
            }
        }

        public PagingController(UIView showView) : base(UIPageViewControllerTransitionStyle.PageCurl, UIPageViewControllerNavigationOrientation.Horizontal, UIPageViewControllerSpineLocation.Min)
        {
//            _pageViewController = new UIPageViewController(UIPageViewControllerTransitionStyle.PageCurl,
//                                                           UIPageViewControllerNavigationOrientation.Horizontal,
//                                                           UIPageViewControllerSpineLocation.Min);
//            showView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
//            _pageViewController.View.Frame = showView.Bounds;
//            showView.AddSubview(_pageViewController.View);
//            _pageViewController.DataSource = _dataSource;
            showView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
            View.Frame = showView.Bounds;
            showView.AddSubview(View);
            DataSource = _dataSource;
        }

        public override 

        public PagingController(UIView showView, UIPageControl pageControl) : this(showView)
        {
            _pageControl = pageControl;
        }

        public void AddPage(DefaultDialogViewController viewController)
        {
            _dataSource.Controllers.Add(viewController);
            if (PageCount == 1)
            {
//                _pageViewController.SetViewControllers(new UIViewController[] { viewController }, UIPageViewControllerNavigationDirection.Forward, false, s => {});
                SetViewControllers(new UIViewController[] { viewController }, UIPageViewControllerNavigationDirection.Forward, false, s => {});
            }
            _pageControl.Pages = PageCount;
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
            public DefaultDialogViewController NoViewDefined{ get; private set; }

            public List<DefaultDialogViewController> Controllers = new List<DefaultDialogViewController>();

            public PageControllerDatasource() : base()
            {
                var root = new RootElement("no view");
                root.Add(new Section("Keine Daten"){
                    new MultilineElement("Keine Daten gefunden")
                });
                NoViewDefined = new DefaultDialogViewController(root);
            }

            public int CurrentPage
            {
                get;
                private set;
            }
            
            public override UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                Console.WriteLine("GetPrevious");
                if (Controllers.Count() == 0)
                    return NoViewDefined;
                var index = Controllers.IndexOf(referenceViewController as DefaultDialogViewController);
                if (index == -1 || index == 0)
                    return null;
                return Controllers [index - 1];
            }    
            
            public override UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
            {
                Console.WriteLine("GetNext");
                if (Controllers.Count() == 0)
                    return NoViewDefined;
                var index = Controllers.IndexOf(referenceViewController as DefaultDialogViewController);
                if (index == -1 || index == Controllers.Count() - 1)
                    return null;
                return Controllers [index + 1];
            }
        }
    }
}

