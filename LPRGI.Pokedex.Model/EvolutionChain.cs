using LPRGI.Pokedex.Model.PokemonModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model
{
    public class EvolutionChain
    {
        [JsonProperty("chain")]
        public ChainLink Chain { get; set; }

        public class ChainLink
        {
            [JsonProperty("evolves_to")]
            public List<ChainLink> EvolvesTo { get; set; }

            [JsonProperty("species")]
            public Pokemon.PokemonSpecies Species { get; set; }
        }
    }
}
