using LPRGI.Pokedex.Model.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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

        public string Description { get; set; }

        public string EvolutionChain { get; set; }

        /// <summary>
        /// Extrait la description d'un <see cref="Pokemon"/>.
        /// </summary>
        /// <param name="pokemonSpecie">Contient l'espèce à extraire.</param>
        public void FormatDescription(PokemonSpecie pokemonSpecie)
        {
            var frFlavorTextEntries = pokemonSpecie.FlavorTextEntries.Where((f) => f.Language.Name == "fr");
            var frComments = frFlavorTextEntries.Select((flavorTextEntry) => flavorTextEntry.FlavorText);

            // On prend la première description
            Description = frComments.ElementAt(0);
        }

        /// <summary>
        /// Extrait la chaîne dévolution d'un <see cref="Pokemon"/>.
        /// </summary>
        /// <param name="evolutionChain">Contient la chaîne dévolution à extraire.</param>
        public void FormatEvolutionChain(EvolutionChain evolutionChain)
        {
            var evolvesToSpecies = new List<string>();
            GetSpecies(evolutionChain.Chain.EvolvesTo);

            // On obtient la chaîne d'évolution de manière récursive
            void GetSpecies(List<EvolutionChain.ChainLink> chainLinks)
            {
                if (chainLinks.Count > 0)
                {
                    evolvesToSpecies.Add(chainLinks[0].SpeciesResource.Name);
                    GetSpecies(chainLinks[0].EvolvesTo);
                }
            }

            var speciesJoined = evolvesToSpecies.Count > 0 ? string.Join(", ", evolvesToSpecies) : "aucune chaîne d'évolution";

            EvolutionChain = speciesJoined;
        }

        public override string ToString()
        {
            // Concaténation des noms des types
            var typesToString = string.Join(", ", Types.Select((t) => t.TypeResource.Name));

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
