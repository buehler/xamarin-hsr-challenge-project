using System;
using HSR_Helper.DomainLibrary.Domain;
using System.Collections.Generic;

namespace HSR_Helper.iOS
{
	public class DishTableItemGroup
	{
		public Dish Dish {get; private set;}

		protected List<DishTableItem> _items = new List<DishTableItem>();
		public List<DishTableItem> Items
		{
			get { return this._items; }
			set { this._items = value; }
		}

		public DishTableItemGroup ()
		{
		}
	}
}

