using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LPRGI.Pokedex.Model
{
    public partial class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonType> Types { get; set; }

        [JsonPropertyName("species")]
        public PokemonSpecies Species { get; set; }

        public string Description { get; set; }

        public string EvolutionChain { get; set; }

        public override string ToString()
        {
            // Concaténation des noms des types
            var typesToString = string.Join(", ", Types.Select((t) => t.Type.Name));

            return
                $"Informations sur {Name} :           \n" +
                $"Numéro             - {Id}           \n" +
                $"Type(s)            - {typesToString}\n" +
                $"Description        -                \n" +
                $"{Description}                       \n" +
                $"Chaîne d'évolution - {EvolutionChain}";
        }
    }
}
