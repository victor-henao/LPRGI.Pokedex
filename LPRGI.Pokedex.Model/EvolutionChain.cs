using LPRGI.Pokedex.Model.Base;
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
            public NamedResource SpeciesResource { get; set; }
        }
    }
}
