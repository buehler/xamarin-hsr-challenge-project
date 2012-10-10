using System;
using MonoTouch.UIKit;

namespace HSR_Helper.iOS
{
	public static class ApplicationColors
	{
		public static readonly UIColor NAVIGATIONBAR = UIColor.FromRGB (122,106,83);
		public static readonly UIColor TABBAR = UIColor.FromRGB (61,53,42);
		public static readonly UIColor PAGECONTROLLER_PAGES = UIColor.FromRGB (122,106,83);
		public static readonly UIColor PAGECONTROLLER_CURRENT_PAGE = UIColor.FromRGB (61,53,42);
		public static readonly UIColor DEFAULT_BACKGROUND = UIColor.FromPatternImage(UIImage.FromBundle("whitey"));//UIColor.FromRGB (213, 222, 217);
		public static readonly UIColor TABLE_HEADER_FONT = UIColor.FromRGB (2, 2, 2);
		public static readonly UIColor TABLE_FONT = UIColor.FromRGB (2, 2, 2);
		public static readonly UIColor TABLE_DETAIL_FONT = UIColor.FromRGB (0, 95, 156);
		public static readonly UIColor TABLE_NOT_ENOUGH_MONEY = UIColor.FromRGB (249, 125, 112);
	}
}

