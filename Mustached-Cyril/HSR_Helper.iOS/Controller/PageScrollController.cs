using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS.Controller
{
	public delegate void PageChangedEventHandler (int newPage);

	public class PageScrollController<T> where T : UIViewController
	{
		private readonly UIScrollView _scrollView;
		private readonly UIPageControl _pageControl;
		private readonly List<T> _viewControllers = new List<T> (); 

		public int CurrentPage {
			get {
				return _pageControl.CurrentPage;
			}
		}

		public int PageCount {
			get {
				return _viewControllers.Count ();
			}
		}

		public T this [int pageIndex] {
			get {
				if (pageIndex < 0 || pageIndex > _viewControllers.Count ())
					throw new ArgumentOutOfRangeException ("pageIndex", "Index is out of bounds.");
				return _viewControllers.ElementAt (pageIndex);
			}
		}

		public PageScrollController (UIScrollView scrollView, UIPageControl pageControl)
		{
			_scrollView = scrollView;
			_pageControl = pageControl;
			_pageControl.ValueChanged += PageControllValueChanged;
			_pageControl.Pages = 0;
			_scrollView.PagingEnabled = true;
			_scrollView.ScrollEnabled = true;
			_scrollView.ShowsHorizontalScrollIndicator = false;
			_scrollView.ShowsVerticalScrollIndicator = false;
			_scrollView.Scrolled += ScrollViewScrolled;
		}

		public void AddPage (T controller)
		{
			ReframeScrollview ();
			RectangleF lastFrame = _scrollView.Frame;
			PointF lastLocation = new PointF ();
			lastLocation.X = lastFrame.Width * _scrollView.Subviews.Count ();
			lastFrame.Location = lastLocation;
			controller.View.Frame = lastFrame;
			_scrollView.AddSubview (controller.View);
			_viewControllers.Add (controller);
			_pageControl.Pages = _scrollView.Subviews.Count ();
			if (PageCount == 1)
				PageChanged ();
		}

		public void AddPages (IEnumerable<T> views)
		{
			foreach (T view in views) {
				AddPage (view);
			}
		}

		public void RemovePage (int pageNumber)
		{
			for (int i = pageNumber-1; i < _scrollView.Subviews.Count (); i++) {
				if (i == (_scrollView.Subviews.Count () - 1)) {
					_scrollView.Subviews [i] = null;
					break;
				} else {
					_scrollView.Subviews [i] = _scrollView.Subviews [i + 1];
				}
			}
			_viewControllers.RemoveAt (pageNumber - 1);
			ReframeScrollview ();
			_pageControl.Pages = _viewControllers.Count ();
		}

		public void RemovePage (T controller)
		{
			RemovePage (_viewControllers.IndexOf (controller) + 1);
		}

		public void ScrollToPage (int pageNumber)
		{
			_scrollView.SetContentOffset (new PointF (_scrollView.Frame.Width * pageNumber, 0), true);
			PageChanged ();
		}

		private void ReframeScrollview ()
		{
			RectangleF scrollFrame = _scrollView.Frame;
			scrollFrame.Width = scrollFrame.Width * (!_viewControllers.Any () ? 1 : _viewControllers.Count () + 1);
			_scrollView.ContentSize = scrollFrame.Size;
		}

		private void ScrollViewScrolled (object sender, EventArgs e)
		{
			double page = Math.Floor ((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
			_pageControl.CurrentPage = (int)page;
			PageChanged ();
		}

		private void PageControllValueChanged (object sender, EventArgs e)
		{
			var pc = (UIPageControl)sender;
			double fromPage = Math.Floor ((_scrollView.ContentOffset.X - _scrollView.Frame.Width / 2) / _scrollView.Frame.Width) + 1;
			var toPage = pc.CurrentPage;
			var pageOffset = _scrollView.ContentOffset.X + _scrollView.Frame.Width;
			Console.WriteLine ("fromPage " + fromPage + " toPage " + toPage);
			if (fromPage > toPage)
				pageOffset = _scrollView.ContentOffset.X - _scrollView.Frame.Width;
			PointF p = new PointF (pageOffset, 0);
			_scrollView.SetContentOffset (p, true);
			PageChanged ();
		}

		private void PageChanged ()
		{
			if (OnPageChange != null)
				OnPageChange (CurrentPage);
		}

		public event PageChangedEventHandler OnPageChange;
	}
}

