using System;
using System.Threading.Tasks;

namespace LPRGI.Pokedex
{
    class Program
    {
        static readonly string[] Commands = new string[]
        {
            "name",
            "exit"
        };

        static async Task Main()
        {
            using var pokedexCient = new Request.PokedexClient();
            Console.WriteLine(
                "Bienvenue dans le Pokédex!\n" +
                "Entrez 'help' pour afficher les commandes\n");

            var input = string.Empty;
            bool exit = false;

            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case var cmd when input.StartsWith("name", StringComparison.CurrentCultureIgnoreCase):
                            var pokemonName = cmd.Split(" ")[1];
                            var pokemon = await pokedexCient.GetPokemonAsync(pokemonName);
                            Console.WriteLine(pokemon);
                            break;

                        case var cmd when input.StartsWith("help", StringComparison.CurrentCultureIgnoreCase):
                            Console.WriteLine(
                                "Commandes :\n" +
                                "name <nom du Pokémon> - obtient les détails d'un Pokémon à partir de son nom\n" +
                                "exit                  - sortie du programme");
                            break;

                        case var cmd when input.StartsWith("exit", StringComparison.CurrentCultureIgnoreCase):
                            exit = true;
                            break;

                        default:
                            throw new UnknownCommandException("Commande inconnue, entrez 'help' pour plus d'informations");
                    }
                }
                catch (UnknownCommandException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            } while (!exit);
        }
    }
}
