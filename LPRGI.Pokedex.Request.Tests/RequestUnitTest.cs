using System.Threading.Tasks;
using Xunit;

namespace LPRGI.Pokedex.Request.Tests
{
    public class RequestUnitTest
    {
        [Fact]
        public async Task PokemonRequestTestAsync()
        {
            using var pokedexClient = new PokedexClient();
            await Assert.ThrowsAsync<UnknownPokemonException>(() => pokedexClient.GetPokemonAsync("dito"));
        }
    }
}
