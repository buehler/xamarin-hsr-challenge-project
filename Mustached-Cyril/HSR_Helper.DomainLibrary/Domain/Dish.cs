using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace HSR_Helper.DomainLibrary.Domain
{
	public class Dish
	{
		[JsonProperty("id")]
		public int DishId { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		private string _priceInternal;
		[JsonProperty("price_internal")]
		public string PriceInternal { 
			get {
				return _priceInternal;
			} 
			set {
				_priceInternal = "CHF " + value;
			} 
		}
	}
}
