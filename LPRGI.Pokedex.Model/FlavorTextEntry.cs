using Newtonsoft.Json;

namespace LPRGI.Pokedex.Model
{
    public class FlavorTextEntry
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("language")]
        public FlavorTextEntryLanguage Language { get; set; }

        public class FlavorTextEntryLanguage
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
