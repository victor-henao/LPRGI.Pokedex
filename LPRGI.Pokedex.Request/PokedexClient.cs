using LPRGI.Pokedex.Model;
using LPRGI.Pokedex.Model.PokemonModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Request
{
    public class PokedexClient : HttpClient
    {
        private HttpResponseMessage responseMessage;
        private readonly List<Pokemon> pokemonCache;

        public PokedexClient()
        {
            responseMessage = new HttpResponseMessage();
            pokemonCache = new List<Pokemon>();
        }

        /// <summary>
        /// Recherche un <see cref="Pokemon"/> à partir de son nom.
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public async Task<Pokemon> GetPokemonAsync(string pokemonName)
        {
            // On vérifie si le Pokémon demandé est déjà dans le cache
            Pokemon pokemonInCache = pokemonCache.Where((pokemon) => pokemon.Name == pokemonName).FirstOrDefault();
            if (pokemonInCache != null)
            {
                return pokemonInCache;
            }

            // On obtient d'abord le numéro du Pokémon, son nom et son ou ses type(s)
            var pokemonMessageContent = await GetMessageContentAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(pokemonMessageContent);

            // On extrait ensuite sa description
            var descriptionMessageContent = await GetMessageContentAsync(pokemon.SpeciesResource.Url);
            var pokemonSpecie = JsonConvert.DeserializeObject<PokemonSpecie>(descriptionMessageContent);
            pokemon.FormatDescription(pokemonSpecie);

            // On extrait enfin la chaîne d'évolution
            var evolutionChainMessageContent = await GetMessageContentAsync(pokemonSpecie.EvolutionChainResource.Url);
            var evolutionChain = JsonConvert.DeserializeObject<EvolutionChain>(evolutionChainMessageContent);
            pokemon.FormatEvolutionChain(evolutionChain);

            pokemonCache.Add(pokemon);
            return pokemon;
        }

        public async Task<string> GetPokemonsByTypeAsync(string pokemonType)
        {
            var typeResultsMessageContent = await GetMessageContentAsync("https://pokeapi.co/api/v2/type/" + pokemonType);
            var type = JsonConvert.DeserializeObject<Type>(typeResultsMessageContent);

            var typePokemons = type.Pokemons.Select((pokemon) => pokemon.PokemonResource.Name);
            var typePokemonsJoined = string.Join(", ", typePokemons);

            return typePokemonsJoined;
        }

        /// <summary>
        /// Obtient la représentaion JSON d'une ressource à partir de son URL.
        /// </summary>
        /// <param name="url">L'URL de la ressource.</param>
        /// <returns>Une tâche contenant la représentaion JSON comme résultat.</returns>
        private async Task<string> GetMessageContentAsync(string url)
        {
            responseMessage = await GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsStringAsync();
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
