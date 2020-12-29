using LPRGI.Pokedex.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Request
{
    public class PokedexClient : HttpClient
    {
        private HttpResponseMessage responseMessage;

        public async Task<Pokemon> GetPokemonAsync(string pokemonName)
        {
            try
            {
                responseMessage = await GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
                responseMessage.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Pokemon>(responseMessage.Content.ReadAsStringAsync().Result);
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
