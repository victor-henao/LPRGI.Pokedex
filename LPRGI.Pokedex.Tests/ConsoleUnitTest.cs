using LPRGI.Pokedex.Command;
using Xunit;

namespace LPRGI.Pokedex.Tests
{
    public class ConsoleUnitTest
    {
        [Fact]
        public void CommandTest()
        {
            // Tests sur une commande inconnue
            Assert.Throws<UnknownCommandException>(() => "namee ditto".Parse());
            Assert.Throws<UnknownCommandException>(() => "hellp".Parse());
            Assert.Throws<UnknownCommandException>(() => "exiit".Parse());
        }

        [Fact]
        public void ParseTest()
        {
            Assert.Equal(new string[] { "name", "bulbasaur" }, "name bulbasaur".Parse());

            // Test avec des espaces
            Assert.Equal(new string[] { "name", "ditto" }, "name      ditto".Parse());
        }

        [Fact]
        public void CaseInsensitiveTest()
        {
            Assert.Equal(new string[] { "help" }, "HeLp".Parse());
            Assert.Equal(new string[] { "name", "ditto" }, "NAME ditto".Parse());
            Assert.Equal(new string[] { "name", "bulbasaur" }, "NAmE Bulbasaur".Parse());
            Assert.Equal(new string[] { "exit" }, "exiT".Parse());
        }
    }
}
