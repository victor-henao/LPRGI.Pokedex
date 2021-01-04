using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LPRGI.Pokedex.Model
{
    public class Type
    {
        [JsonProperty("pokemon")]
        public List<TypePokemon> Pokemons { get; set; }

        public class TypePokemon
        {
            [JsonProperty("pokemon")]
            public NamedResource PokemonResource { get; set; }
        }
    }
}
