using LPRGI.Pokedex.Model;
using Newtonsoft.Json;
using System.Diagnostics;
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
                responseMessage = await GetAsync("https://pokeapi.co/api/v2/pokemon/" + pokemonName);
                responseMessage.EnsureSuccessStatusCode();

                // On obtient les informations simples sur le Pokémon : son id, nom et ses types
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseMessage.Content.ReadAsStringAsync().Result);

                responseMessage = await GetAsync(pokemon.Species.Url);
                responseMessage.EnsureSuccessStatusCode();

                // Ensuite on extrait sa descirption
                var pokemonSpecie = JsonConvert.DeserializeObject<PokemonSpecie>(responseMessage.Content.ReadAsStringAsync().Result);

                var frDescription = pokemonSpecie.FlavorTextEntries.Where((f) => f.Language.Name == "fr");
                foreach (var item in frDescription)
                {
                    Debug.WriteLine(item.FlavorText);
                }

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
