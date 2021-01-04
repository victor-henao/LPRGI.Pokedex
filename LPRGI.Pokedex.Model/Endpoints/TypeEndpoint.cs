using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model.Endpoints
{
    public class TypeEndpoint
    {
        [JsonProperty("name")]
        public List<TypeEndpointResult> Results { get; set; }

        public class TypeEndpointResult
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
