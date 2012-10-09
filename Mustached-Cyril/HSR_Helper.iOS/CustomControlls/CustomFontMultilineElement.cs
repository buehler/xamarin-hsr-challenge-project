using System;
using MonoTouch.Dialog;
using System.Drawing;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class CustomFontMultilineElement : MultilineElement
	{
		public enum FontStyle
		{
			Normal,
			Bold,
			Italic
		}

		private readonly UIFont _font;

		public CustomFontMultilineElement (string caption, int fontSize = 16, FontStyle fontStyle = FontStyle.Bold) : base(caption)
		{
			switch (fontStyle) {
			case FontStyle.Bold:
				_font = UIFont.BoldSystemFontOfSize (fontSize);
				break;
			case FontStyle.Italic:
				_font = UIFont.ItalicSystemFontOfSize (fontSize);
				break;
			case FontStyle.Normal:
				_font = UIFont.SystemFontOfSize (fontSize);
				break;
			}
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			var textLabel = cell.TextLabel;
			textLabel.Font = _font;
			return cell;
		}

		public override float GetHeight (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			float margin = UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ? 40f : 110f;
			SizeF size = new SizeF (tableView.Bounds.Width - margin, float.MaxValue);
			string c = Caption;
			// ensure the (single-line) Value will be rendered inside the cell
			if (String.IsNullOrEmpty (c) && !String.IsNullOrEmpty (Value))
				c = " ";
			return tableView.StringSize (c, _font, size, UILineBreakMode.WordWrap).Height + 10;
		}
	}
}

