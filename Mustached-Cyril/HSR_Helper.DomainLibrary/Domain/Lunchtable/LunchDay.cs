using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain.Lunchtable
{
    
	public class LunchDay
	{
		[JsonProperty("date")]
		public string DateString { get; set; }
		[JsonProperty("menus")]
		public List<Dish> Dishes { get; set; }

		public LunchDay ()
		{
			Dishes = new List<Dish> ();
		}

		public LunchDay (string datestring) : this()
		{
			DateString = datestring;
		}
	}
}
