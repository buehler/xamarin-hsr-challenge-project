using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Attribute;
using HSR_Helper.DomainLibrary.Persistency;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain
{
	public class Lunchtable
	{
		[PersistentProperty]
		public Guid Id { get; set; }
		public string Filename{ get; set; }
		[PersistentProperty]
		[JsonProperty("days")]
		public List<LunchDay> LunchDays { get; set; }
        
		public Lunchtable ()
		{
			LunchDays = new List<LunchDay> ();
		}
	}
}
