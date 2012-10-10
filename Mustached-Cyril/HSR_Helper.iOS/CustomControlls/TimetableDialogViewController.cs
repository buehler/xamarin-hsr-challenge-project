using System;
using MonoTouch.Dialog;

namespace HSR_Helper.iOS
{
	public class TimetableDialogViewController : DefaultDialogViewController
	{
		public TimetableDialogViewController (RootElement root) : base(root)
		{
			Style = MonoTouch.UIKit.UITableViewStyle.Plain;
		}
	}
}

