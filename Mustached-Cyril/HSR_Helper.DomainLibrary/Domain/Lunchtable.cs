using System.Collections.Generic;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain
{
	public class Lunchtable
	{
		[JsonProperty("days")]
		public List<LunchDay> LunchDays { get; set; }
        
		public Lunchtable ()
		{
			LunchDays = new List<LunchDay> ();
		}
	}
}
