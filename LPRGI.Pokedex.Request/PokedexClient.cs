﻿using LPRGI.Pokedex.Model;
using LPRGI.Pokedex.Model.PokemonModel;
using LPRGI.Pokedex.Request.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Request
{
    public class PokedexClient : HttpClient
    {
        private HttpResponseMessage responseMessage;
        private readonly MemoryCache memoryCache;

        public PokedexClient()
        {
            responseMessage = new HttpResponseMessage();
            memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        /// <summary>
        /// Recherche un <see cref="Pokemon"/> à partir de son nom.
        /// </summary>
        /// <param name="pokemonName"></param>
        /// <returns></returns>
        public async Task<Pokemon> GetPokemonAsync(string pokemonName)
        {
            // On vérifie si le Pokémon demandé est déjà dans le cache
            var pokemonInCache = memoryCache.Get<Pokemon>(pokemonName);
            if (pokemonInCache != null)
            {
                return pokemonInCache;
            }

            // On obtient d'abord le numéro du Pokémon, son nom et son ou ses type(s)
            string pokemonMessageContent;
            try
            {
                pokemonMessageContent = await GetMessageContentAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
            }
            catch (HttpRequestException)
            {
                throw new UnknownPokemonException("Le Pokémon demandé est introuvable.");
            }

            var pokemon = JsonConvert.DeserializeObject<Pokemon>(pokemonMessageContent);

            // On extrait ensuite sa description
            var descriptionMessageContent = await GetMessageContentAsync(pokemon.SpeciesResource.Url);
            var pokemonSpecie = JsonConvert.DeserializeObject<PokemonSpecie>(descriptionMessageContent);
            pokemon.FormatDescription(pokemonSpecie);

            // On extrait enfin la chaîne d'évolution
            var evolutionChainMessageContent = await GetMessageContentAsync(pokemonSpecie.EvolutionChainResource.Url);
            var evolutionChain = JsonConvert.DeserializeObject<EvolutionChain>(evolutionChainMessageContent);
            pokemon.FormatEvolutionChain(evolutionChain);

            memoryCache.Set(pokemon.Name, pokemon);
            return pokemon;
        }

        /// <summary>
        /// Obtient un objet qui encapsule une liste de Pokémons appartenant au type recherché.
        /// </summary>
        /// <param name="pokemonType"></param>
        /// <returns></returns>
        public async Task<Type> GetPokemonsByTypeAsync(string pokemonType)
        {
            // On vérifie si le type demandé est déjà dans le cache
            var typeInCache = memoryCache.Get<Type>(pokemonType);
            if (typeInCache != null)
            {
                return typeInCache;
            }

            string typeResultsMessageContent;
            try
            {
                typeResultsMessageContent = await GetMessageContentAsync("https://pokeapi.co/api/v2/type/" + pokemonType);
                var type = JsonConvert.DeserializeObject<Type>(typeResultsMessageContent);

                memoryCache.Set(type.Name, type);
                return type;
            }
            catch (HttpRequestException)
            {
                throw new UnknownPokemonTypeException("Le type Pokémon demandé est introuvable.");
            }
        }

        /// <summary>
        /// Obtient la représentation JSON d'une ressource à partir de son URL.
        /// </summary>
        /// <param name="url">L'URL de la ressource.</param>
        /// <returns></returns>
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
                memoryCache.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
