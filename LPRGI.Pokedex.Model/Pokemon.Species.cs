using System.Text.Json.Serialization;

namespace LPRGI.Pokedex.Model
{
    partial class Pokemon
    {
        public class PokemonSpecies
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
