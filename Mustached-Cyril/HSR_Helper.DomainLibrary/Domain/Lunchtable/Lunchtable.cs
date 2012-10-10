using System.Collections.Generic;
using Newtonsoft.Json;

namespace HSR_Helper.DomainLibrary.Domain.Lunchtable
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
