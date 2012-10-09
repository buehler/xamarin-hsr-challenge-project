using System;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class CustomFontSection : Section
	{
		public CustomFontSection (string caption, int fontSize = 24, UITextAlignment alignment = UITextAlignment.Center, UIColor textColor = null) : base("")
		{
			var label = new UILabel ();
			var frame = label.Frame;
			frame.Inflate (0, fontSize);
			label.Frame = frame;
			label.Font = UIFont.BoldSystemFontOfSize (fontSize);
			label.Text = caption;
			label.TextAlignment = alignment;
			label.BackgroundColor = ApplicationColors.DEFAULT_BACKGROUND_COLOR;
			label.TextColor = (textColor == null ? ApplicationColors.TABLE_HEADER_FONT_COLOR : textColor);
			this.HeaderView = label;
		}
	}
}

