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
            // Est-ce que le Pokémon recherché est dans le cache ?
            Pokemon pokemonInCache = pokemonCache.Where((pokemon) => pokemon.Name == pokemonName).FirstOrDefault();
            if (pokemonInCache != null)
            {
                return pokemonInCache;
            }

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
            pokemon.FormatDescription(pokemonSpecie);

            // On extrait la chaîne d'évolution
            responseMessage = await GetAsync(pokemonSpecie.EvolutionChain.Url);
            responseMessage.EnsureSuccessStatusCode();
            var evolutionChain = JsonConvert.DeserializeObject<EvolutionChain>(responseMessage.Content.ReadAsStringAsync().Result);
            pokemon.FormatEvolutionChain(evolutionChain);

            pokemonCache.Add(pokemon);
            return pokemon;
        }

        public async Task<List<Pokemon>> GetPokemonsByTypeAsync(string type)
        {


            return new List<Pokemon>();
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
