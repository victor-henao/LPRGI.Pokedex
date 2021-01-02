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

            try
            {
                do
                {
                    Console.Write("> ");
                    input = Console.ReadLine();

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
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Commande inconnue, entrez 'help' pour plus d'informations");
                            Console.ResetColor();
                            throw new Exception();
                    }
                } while (!exit);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
