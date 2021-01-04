using System.Collections.Generic;
using System.Linq;

namespace LPRGI.Pokedex.Model.PokemonModel
{
    partial class Pokemon
    {
        /// <summary>
        /// Extrait la description d'un <see cref="Pokemon"/> sous la forme d'une chaîne de caractères.
        /// </summary>
        /// <param name="pokemonSpecie">Contient la description de l'espèce.</param>
        public void FormatDescription(PokemonSpecie pokemonSpecie)
        {
            // On prend la première description française
            var frFlavorTextEntries = pokemonSpecie.FlavorTextEntries.Where((flavorTextEntry) => flavorTextEntry.Language.Name == "fr");
            var frComments = frFlavorTextEntries.Select((flavorTextEntry) => flavorTextEntry.FlavorText);

            Description = frComments.ElementAt(0);
        }

        /// <summary>
        /// Extrait la chaîne d'évolution d'un <see cref="Pokemon"/>sous la forme d'une chaîne de caractères.
        /// </summary>
        /// <param name="evolutionChain">Contient la chaîne d'évolution.</param>
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
                $"Informations sur {Name} :           \n\n" +
                $"Numéro             - {Id}           \n\n" +
                $"Type(s)            - {typesToString}\n\n" +
                $"Description        -                \n\n" +
                $"{Description}                       \n\n" +
                $"Chaîne d'évolution - {EvolutionChain}";
        }
    }
}
