using System;
using MonoTouch.Dialog;
using System.Drawing;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public class CustomFontMultilineElement : StyledMultilineElement
	{
		public enum FontStyle
		{
			Normal,
			Bold,
			Italic
		}

		private readonly UIFont _font;
		private readonly UIFont _detailFont;

		public CustomFontMultilineElement (string caption) : this(caption, 16, FontStyle.Bold)
		{
		}

		public CustomFontMultilineElement (string caption, int fontSize, FontStyle fontStyle):base(caption)
		{
			_font = GetFontFromStyle (fontStyle, fontSize);
			TextColor = ApplicationColors.TABLE_FONT_COLOR;
		}

		public CustomFontMultilineElement (string caption, string value):this(caption, value, 16, FontStyle.Bold, FontStyle.Normal)
		{
		}

		public CustomFontMultilineElement (string caption, string value, int fontSize, FontStyle fontStyle, FontStyle detailFontStyle) : base(caption, value)
		{
			_font = GetFontFromStyle (fontStyle, fontSize);
			TextColor = ApplicationColors.TABLE_FONT_COLOR;
			_detailFont = GetFontFromStyle (detailFontStyle, fontSize);
			DetailColor = ApplicationColors.TABLE_DETAIL_FONT_COLOR;
		}

		public override UITableViewCell GetCell (UITableView tv)
		{
			var cell = base.GetCell (tv);
			var textLabel = cell.TextLabel;
			textLabel.Font = _font;
			if (!String.IsNullOrEmpty (Value) && _detailFont != null) {
				cell.DetailTextLabel.Font = _detailFont;
			}
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

		private UIFont GetFontFromStyle (FontStyle fontStyle, int fontSize)
		{
			switch (fontStyle) {
			case FontStyle.Bold:
				return UIFont.BoldSystemFontOfSize (fontSize);
			case FontStyle.Italic:
				return UIFont.ItalicSystemFontOfSize (fontSize);
			case FontStyle.Normal:
				return UIFont.SystemFontOfSize (fontSize);
			default:
				return null;
			}
		}
	}
}

