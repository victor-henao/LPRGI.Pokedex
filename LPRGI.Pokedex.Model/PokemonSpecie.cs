using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model
{
    public class PokemonSpecie
    {
        [JsonProperty("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        [JsonProperty("evolution_chain")]
        public Resource EvolutionChainResource { get; set; }
    }
}
