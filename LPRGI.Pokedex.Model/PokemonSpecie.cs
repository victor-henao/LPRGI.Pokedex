using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model
{
    public class PokemonSpecie
    {
        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonProperty("evolution_chain")]
        public PokemonSpecieEvolutionChain EvolutionChain { get; set; }

        public class PokemonSpecieEvolutionChain
        {
            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
