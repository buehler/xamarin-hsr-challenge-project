using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class DefaultDialogViewController : DialogViewController
	{
		public DefaultDialogViewController (RootElement root) : base (root)
		{
			TableView.BackgroundView = null;
			TableView.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND;
		}
	}
}

