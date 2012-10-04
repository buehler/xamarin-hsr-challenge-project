using Newtonsoft.Json.Bson;
using RestSharp;
using Newtonsoft.Json;
using System.IO;

namespace HSR_Helper.DomainLibrary.Helper
{
    public static class JsonHelper
    {
        public static T ParseJson<T>(IRestResponse response)
        {
            var deserializer = new JsonSerializer();
            return deserializer.Deserialize<T>(new JsonTextReader(new StringReader(response.Content)));
        }
    }
}
