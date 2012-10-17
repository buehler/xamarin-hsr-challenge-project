using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSR_Helper.DomainLibrary.Persistency;
using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace HSR_Helper.DomainLibrary.Domain.Lunchtable
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
        public string PriceInternal
        { 
            get
            {
                return _priceInternal;
            } 
            set
            {
                _priceInternal = value;
            } 
        }

        public Dish()
        {
        }

        public Dish(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Dish;
            if (o != null)
            {
                return (DishId.Equals(o.DishId) && Title.Equals(o.Title) && Description.Equals(o.Description) && PriceInternal.Equals(o.PriceInternal));
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        public override string ToString()
        {
            return DishId.ToString() + Title + Description + PriceInternal;
        }
    }
}
