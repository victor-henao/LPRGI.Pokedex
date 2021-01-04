using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model
{
    public class Type
    {
        [JsonProperty("pokemon")]
        public List<NamedResource> ResultsResource { get; set; }
    }
}
