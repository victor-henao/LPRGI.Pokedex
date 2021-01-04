using System;

namespace LPRGI.Pokedex.Command
{
    class Program
    {
        /// <summary>
        /// Point d'entrée de l'application.
        /// </summary>
        /// <returns></returns>
        static async void Main()
        {
            using var pokedexCient = new Request.PokedexClient();
            Console.WriteLine(
                "Bienvenue dans le Pokédex!\n" +
                "Entrez 'help' pour afficher les commandes\n");

            var input = string.Empty;
            var command = string.Empty;
            var args = Array.Empty<string>();

            bool exit = false;

            do
            {
                Console.Write("> ");

                try
                {
                    args = Console.ReadLine().Parse();
                    command = args[0];
                }
                catch (UnknownCommandException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    command = string.Empty;
                }

                switch (command)
                {
                    // Recherche d'un Pokémon par son nom
                    case var cmd when command == "name":
                        var pokemonName = args[1];
                        var pokemon = await pokedexCient.GetPokemonAsync(pokemonName);
                        Console.WriteLine(pokemon);
                        break;

                    // Affichage de l'aide
                    case var cmd when command == "help":
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
                    case var cmd when command == "exit":
                        exit = true;
                        break;
                }
            } while (!exit);
        }
    }
}
