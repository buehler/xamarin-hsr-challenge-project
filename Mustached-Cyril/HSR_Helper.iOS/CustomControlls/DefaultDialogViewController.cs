using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class DefaultDialogViewController : DialogViewController
	{
		public DefaultDialogViewController (RootElement root) : this (UITableViewStyle.Grouped ,root)
		{
		}

		public DefaultDialogViewController (UITableViewStyle tableStyle, RootElement root) : base(tableStyle, root)
		{
			TableView.BackgroundView = null;
			TableView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}
	}
}

