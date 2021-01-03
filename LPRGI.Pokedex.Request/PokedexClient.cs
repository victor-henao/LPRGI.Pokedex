using LPRGI.Pokedex.Model;
using Newtonsoft.Json;
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
            try
            {
                // On obtient d'abord les informations simples sur le Pokémon : son id, nom et types
                responseMessage = await GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
                responseMessage.EnsureSuccessStatusCode();
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseMessage.Content.ReadAsStringAsync().Result);

                // Ensuite on extrait sa descirption
                responseMessage = await GetAsync(pokemon.Species.Url);
                responseMessage.EnsureSuccessStatusCode();
                var pokemonSpecie = JsonConvert.DeserializeObject<PokemonSpecie>(responseMessage.Content.ReadAsStringAsync().Result);

                // Sélection des commentaires en français
                var frDescription = pokemonSpecie.FlavorTextEntries.Where((f) => f.Language.Name == "fr");
                var fullDescription = string.Empty;

                foreach (var item in frDescription)
                {
                    fullDescription += item.FlavorText + " ";
                }

                pokemon.Description = fullDescription;

                return pokemon;
            }
            catch (HttpRequestException)
            {
                throw;
            }
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
