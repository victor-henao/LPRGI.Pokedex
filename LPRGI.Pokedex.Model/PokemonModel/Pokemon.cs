using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model.PokemonModel
{
    public partial class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("types")]
        public List<PokemonType> Types { get; set; }

        [JsonProperty("species")]
        public NamedResource SpeciesResource { get; set; }

        public string Description { get; internal set; }

        public string EvolutionChain { get; internal set; }
    }
}
