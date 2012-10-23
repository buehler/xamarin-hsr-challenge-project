using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS.Controller
{
    //public delegate void PageChangedEventHandler(int newPage);
    
    public class PageScrollController<T> where T : UIViewController
    {
        private readonly UIScrollView _scrollView;
        private readonly UIPageControl _pageControl;
        private readonly List<T> _viewControllers = new List<T>(); 
        
        public int CurrentPage
        {
            get
            {
                return _pageControl.CurrentPage;
            }
        }
        
        public int PageCount
        {
            get
            {
                return _scrollView.Subviews.Count();
            }
        }
        
        public T this [int pageIndex]
        {
            get
            {
                if (pageIndex < 0 || pageIndex > _viewControllers.Count() - 1)
                    throw new ArgumentOutOfRangeException("pageIndex", "Index is out of bounds.");
                return _viewControllers.ElementAt(pageIndex);
            }
        }
        
        public PageScrollController(UIScrollView scrollView, UIPageControl pageControl)
        {
            _scrollView = scrollView;
            _pageControl = pageControl;
            _pageControl.ValueChanged += PageControllValueChanged;
            _pageControl.Pages = 1;
            _scrollView.PagingEnabled = true;
            _scrollView.ScrollEnabled = true;
            _scrollView.ShowsHorizontalScrollIndicator = false;
            _scrollView.ShowsVerticalScrollIndicator = false;
            _scrollView.Scrolled += ScrollViewScrolled;
        }
        
        public void AddPage(T controller)
        {
            ReframeScrollview();
            RectangleF lastFrame = _scrollView.Frame;
            PointF lastLocation = new PointF();
            lastLocation.X = lastFrame.Width * PageCount;
            lastFrame.Location = lastLocation;
            controller.View.Frame = lastFrame;
            _scrollView.AddSubview(controller.View);
            _viewControllers.Add(controller);
            _pageControl.Pages = (PageCount == 0 ? 1 : PageCount);
            PageChanged();
        }
        
        public void AddPages(IEnumerable<T> controller)
        {
            foreach (T c in controller)
            {
                AddPage(c);
            }
        }
        
        public void RemovePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex > _scrollView.Subviews.Count() - 1)
                throw new ArgumentOutOfRangeException("pageNumber", "No page with that number exists.");
            List<UIView> subviews = _scrollView.Subviews.ToList();
            subviews.ForEach(v => v.RemoveFromSuperview());
            var controller = (from c in _viewControllers
                              where _viewControllers.IndexOf(c) != (pageIndex)
                              select c).ToList();
            _viewControllers.Clear();
            AddPages(controller);
        }
        
        public void Clear()
        {
            List<UIView> subviews = _scrollView.Subviews.ToList();
            subviews.ForEach(v => v.RemoveFromSuperview());
            _viewControllers.Clear();
            _pageControl.Pages = (PageCount == 0 ? 1 : PageCount);
        }
        
        public void ScrollToPage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex > _scrollView.Subviews.Count() - 1)
                throw new ArgumentOutOfRangeException("pageNumber", "No page with that number exists.");
            if (pageIndex != CurrentPage)
            {
                _scrollView.SetContentOffset(new PointF(_scrollView.Frame.Width * pageIndex, 0), true);
                PageChanged();
            }
        }
        
        private void ReframeScrollview()
        {
            RectangleF scrollFrame = _scrollView.Frame;
            scrollFrame.Width = scrollFrame.Width * (!_viewControllers.Any() ? 1 : _viewControllers.Count() + 1);
            _scrollView.ContentSize = scrollFrame.Size;
        }
        
        private void ScrollViewScrolled(object sender, EventArgs e)
        {
            double page = Math.Floor((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
            _pageControl.CurrentPage = (int)page;
            PageChanged();
        }
        
        private void PageControllValueChanged(object sender, EventArgs e)
        {
            var pc = (UIPageControl)sender;
            double fromPage = Math.Floor((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
            var toPage = pc.CurrentPage;
            var pageOffset = _scrollView.ContentOffset.X + _scrollView.Frame.Width;
            Console.WriteLine("fromPage " + fromPage + " toPage " + toPage);
            if (fromPage > toPage)
                pageOffset = _scrollView.ContentOffset.X - _scrollView.Frame.Width;
            PointF p = new PointF(pageOffset, 0);
            _scrollView.SetContentOffset(p, true);
            PageChanged();
        }
        
        private void PageChanged()
        {
            if (OnPageChange != null && PageCount >= 1)
                OnPageChange(CurrentPage);
        }
        
        public event PageChangedEventHandler OnPageChange;
    }
}