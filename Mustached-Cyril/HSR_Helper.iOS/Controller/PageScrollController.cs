using System;
using System.Linq;
using System.Drawing;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS.Controller
{
	public class PageScrollController
	{
        private UIScrollView _scrollView;
	    private UIPageControl _pageControl;

		public PageScrollController (UIScrollView scrollView, UIPageControl pageControl)
		{
		    _scrollView = scrollView;
		    _pageControl = pageControl;
		    _pageControl.ValueChanged += PageControllValueChanged;
		    _scrollView.PagingEnabled = true;
		    _scrollView.ScrollEnabled = true;
		    _scrollView.ShowsHorizontalScrollIndicator = false;
		    _scrollView.ShowsVerticalScrollIndicator = false;
		    _scrollView.Scrolled += ScrollViewScrolled;
		}

	    public void AddPage(UIViewController controller)
	    {
            RectangleF scrollFrame = _scrollView.Frame;
            scrollFrame.Width = scrollFrame.Width * (!_scrollView.Subviews.Any() ? 1 : _scrollView.Subviews.Count() + 1);
            _scrollView.ContentSize = scrollFrame.Size;

            RectangleF lastFrame = _scrollView.Frame;
            PointF lastLocation = new PointF();
            lastLocation.X = lastFrame.Width * _scrollView.Subviews.Count();
            lastFrame.Location = lastLocation;
            controller.View.Frame = lastFrame;
            _scrollView.AddSubview(controller.View);

	        _pageControl.Pages = _scrollView.Subviews.Count();
	    }

        public void ScrollToPage(int pageNumber)
        {
            _scrollView.SetContentOffset(new PointF(_scrollView.Frame.Width * pageNumber, 0), true);
        }

        private void ScrollViewScrolled(object sender, EventArgs e)
        {
            double page = Math.Floor((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
            _pageControl.CurrentPage = (int)page;
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
        }
	}
}

