using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using HSR_Helper.DomainLibrary.Attribute;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain
{
    
	public class LunchDay
	{
		[PersistentProperty]
		public Guid Id { get; set; }
		public string Filename{ get; set; }
		[PersistentProperty, JsonProperty("date")]
		public string DateString { get; set; }
		[PersistentProperty, JsonProperty("menus")]
		public List<Dish> Dishes { get; set; }

		public LunchDay ()
		{
			Dishes = new List<Dish> ();
		}
	}
}
