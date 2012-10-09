using System;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public class DefaultDialogViewController : DialogViewController
	{
		public DefaultDialogViewController (RootElement root) : base (root)
		{
			TableView.BackgroundView = null;
			TableView.BackgroundColor = ApplicationColors.VIEW_BACKGROUND_COLOR;
		}
	}
}

