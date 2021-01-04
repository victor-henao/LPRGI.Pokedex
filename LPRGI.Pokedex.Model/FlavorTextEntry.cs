using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;

namespace LPRGI.Pokedex.Model
{
    public class FlavorTextEntry
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public NamedResource Language { get; set; }
    }
}
