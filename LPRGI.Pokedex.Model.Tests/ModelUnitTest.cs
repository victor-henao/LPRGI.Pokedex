using LPRGI.Pokedex.Request;
using System.Threading.Tasks;
using Xunit;

namespace LPRGI.Pokedex.Model.Tests
{
    public class ModelUnitTest
    {
        [Fact]
        public async Task ModelFormatEvolutionChainTestAsync()
        {
            // On s'assure que la cha�ne d'�volution est bien format�e
            using var pokedexClient = new PokedexClient();
            var pokemon = await pokedexClient.GetPokemonAsync("bulbasaur");
            Assert.Equal("ivysaur, venusaur", pokemon.EvolutionChain);
        }

        [Fact]
        public async Task ModelFormatDescriptionTestAsync()
        {
            // On s'assure que la description est bien extraite
            using var pokedexClient = new PokedexClient();
            var pokemon = await pokedexClient.GetPokemonAsync("bulbasaur");
            Assert.Equal(
                "Au matin de sa vie, la graine sur\n" +
                "son dos lui fournit les �l�ments\n" +
                "dont il a besoin pour grandir.",
                pokemon.Description);
        }
    }
}
