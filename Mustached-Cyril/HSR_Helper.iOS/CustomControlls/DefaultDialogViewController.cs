using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class DefaultDialogViewController : DialogViewController
	{
		public DateTime CustomLastUpdate {
			get {
				if (refreshView != null)
					return refreshView.LastUpdate;
				return DateTime.MinValue;
			}
			set {
				if (refreshView != null)
					refreshView.LastUpdate = value;
			}
		}

		public DefaultDialogViewController (RootElement root, UITableViewStyle tableStyle = UITableViewStyle.Grouped, EventHandler refreshRequested = null) : base (root)
		{
			if (refreshRequested != null)
				RefreshRequested += refreshRequested;
			Style = tableStyle;
			TableView.BackgroundView = null;
			TableView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}
	}
}

