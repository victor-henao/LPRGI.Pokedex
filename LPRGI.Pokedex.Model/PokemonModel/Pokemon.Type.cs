using System.Text.Json.Serialization;

namespace LPRGI.Pokedex.Model.PokemonModel
{
    partial class Pokemon
    {
        public class PokemonType
        {
            [JsonPropertyName("type")]
            public Type Type { get; set; }
        }

        public class Type
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }
}
