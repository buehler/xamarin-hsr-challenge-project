using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class DefaultDialogViewController : DialogViewController
	{
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

