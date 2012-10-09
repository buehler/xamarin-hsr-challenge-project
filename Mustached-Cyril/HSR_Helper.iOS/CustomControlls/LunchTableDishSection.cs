using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class LunchTableDishSection : Section
	{
		public LunchTableDishSection (string caption) : base("")
		{
			var label = new UILabel ();
			var frame = label.Frame;
			frame.Inflate (0, 24);
			label.Frame = frame;
			label.Font = UIFont.BoldSystemFontOfSize (24);
			label.Text = caption;
			label.TextAlignment = UITextAlignment.Center;
			label.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND_COLOR;
			label.TextColor = ApplicationColors.TABLE_HEADER_FONT_COLOR;
			this.HeaderView = label;
		}
	}
}

