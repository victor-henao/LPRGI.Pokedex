using LPRGI.Pokedex.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Request
{
    public class PokedexClient : HttpClient
    {
        private HttpResponseMessage responseMessage = new HttpResponseMessage();

        public async Task<Pokemon> GetPokemonAsync(string pokemonName)
        {
            // On obtient d'abord les informations simples sur le Pokémon : son id, nom et types
            responseMessage = await GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);

            // On vérifie si le nom du Pokémon existe bien
            switch (responseMessage.EnsureSuccessStatusCode().StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    throw new UnknownPokemonException("Le nom du Pokémon spécifié est introuvable.");
                default:
                    break;
            }

            var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseMessage.Content.ReadAsStringAsync().Result);

            // Ensuite on extrait sa descirption
            responseMessage = await GetAsync(pokemon.Species.Url);
            responseMessage.EnsureSuccessStatusCode();
            var pokemonSpecie = JsonConvert.DeserializeObject<PokemonSpecie>(responseMessage.Content.ReadAsStringAsync().Result);

            // Sélection des commentaires en français
            var frFlavorTextEntries = pokemonSpecie.FlavorTextEntries.Where((f) => f.Language.Name == "fr");
            var frComments = frFlavorTextEntries.Select((flavorTextEntry) => flavorTextEntry.FlavorText);
            var fullDescription = string.Empty;

            foreach (var item in frComments)
            {
                fullDescription += item + "\n\n";
            }

            pokemon.Description = frComments.ElementAt(0);

            // On extrait la chaîne d'évolution
            responseMessage = await GetAsync(pokemonSpecie.EvolutionChain.Url);
            responseMessage.EnsureSuccessStatusCode();
            var evolutionChain = JsonConvert.DeserializeObject<EvolutionChain>(responseMessage.Content.ReadAsStringAsync().Result);

            var evolvesToSpecies = new List<string>();
            GetSpecies(evolutionChain.Chain.EvolvesTo);

            void GetSpecies(List<EvolutionChain.ChainLink> chainLinks)
            {
                if (chainLinks.Count > 0)
                {
                    evolvesToSpecies.Add(chainLinks[0].Species.Name);
                    GetSpecies(chainLinks[0].EvolvesTo);
                }
            }

            var speciesJoined = evolvesToSpecies.Count > 0 ? string.Join(", ", evolvesToSpecies) : "aucune chaîne d'évolution";

            pokemon.EvolutionChain = speciesJoined;

            return pokemon;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                responseMessage.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
