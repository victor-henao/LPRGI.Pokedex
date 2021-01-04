using System.Text.Json.Serialization;

namespace LPRGI.Pokedex.Model.PokemonModel
{
    partial class Pokemon
    {
        public class PokemonSpecies
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
