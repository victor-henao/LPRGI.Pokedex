using System;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Command
{
    class Program
    {
        /// <summary>
        /// Point d'entrée de l'application.
        /// </summary>
        /// <returns></returns>
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
                var args = Console.ReadLine().Parse();
                var command = args[0];

                switch (command)
                {
                    // Recherche d'un Pokémon par son nom
                    case var cmd when command.Equals("name", StringComparison.CurrentCultureIgnoreCase):
                        var pokemonName = args[1];
                        var pokemon = await pokedexCient.GetPokemonAsync(pokemonName);
                        Console.WriteLine(pokemon);
                        break;

                    // Affichage de l'aide
                    case var cmd when command.Equals("help", StringComparison.CurrentCultureIgnoreCase):
                        Console.WriteLine("Commandes :");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("name <nom du Pokémon>");
                        Console.ResetColor();
                        Console.Write(" - obtient les détails d'un Pokémon à partir de son nom\n");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("exit                 ");
                        Console.ResetColor();
                        Console.Write(" - sortie du programme\n");
                        break;

                    // Sortie du programme
                    case var cmd when command.Equals("exit", StringComparison.CurrentCultureIgnoreCase):
                        exit = true;
                        break;
                }
            } while (!exit);
        }
    }
}
