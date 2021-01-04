using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;

namespace LPRGI.Pokedex.Model.PokemonModel
{
    partial class Pokemon
    {
        public class PokemonType
        {
            [JsonProperty("type")]
            public NamedResource TypeResource { get; set; }
        }
    }
}
