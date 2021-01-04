using LPRGI.Pokedex.Command;
using Xunit;

namespace LPRGI.Pokedex.Tests
{
    public class ConsoleUnitTest
    {
        [Fact]
        public void CommandTest()
        {
            // Test sur une commande inconnue
            Assert.Throws<UnknownCommandException>(() => "namee ditto".Parse());
            Assert.Throws<UnknownCommandException>(() => "hellp".Parse());
            Assert.Throws<UnknownCommandException>(() => "exiit".Parse());
        }

        [Fact]
        public void ParsingTest()
        {
            Assert.Equal(new string[] { "name", "bulbasaur" }, "name bulbasaur".Parse());

            // Tests avec plusieurs espaces
            Assert.Equal(new string[] { "name", "ditto" }, "name      ditto".Parse());
        }

        [Fact]
        public void CaseInsensitiveTest()
        {
            Assert.Equal(new string[] { "help" }, "HeLp".Parse());
            Assert.Equal(new string[] { "name", "ditto" }, "NAME ditto".Parse());
            Assert.Equal(new string[] { "exit" }, "exiT".Parse());
        }
    }
}
