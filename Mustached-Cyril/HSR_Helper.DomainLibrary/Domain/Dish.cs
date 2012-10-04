using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace HSR_Helper.DomainLibrary.Domain
{
    public class Dish : IPersistentObject
    {
        public Guid Id { get; set; }
        [JsonProperty("id")]
        public int DishId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("price_external")]
        public string PriceExternal { get; set; }
        [JsonProperty("price_internal")]
        public string PriceInternal { get; set; }
    }
}
