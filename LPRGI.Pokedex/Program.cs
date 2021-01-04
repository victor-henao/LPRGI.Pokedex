using System;
using System.Linq;
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

                    // Recherche de Pokémons avec un type
                    case var cmd when command == "type":
                        var type = args[1];
                        var pokemonsByType = await pokedexCient.GetPokemonsByTypeAsync(type);

                        var pokemonNames = pokemonsByType.Pokemons.Select((pokemon) => pokemon.PokemonResource.Name);
                        var pokemonNamesJoined = string.Join(", ", pokemonNames);

                        Console.WriteLine(pokemonNamesJoined);
                        break;

                    // Affichage de l'aide
                    case var cmd when command == "help":
                        Console.WriteLine("Commandes :");

                        // Name
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("name <nom du Pokémon>              ");
                        Console.ResetColor();
                        Console.Write(" - obtient les détails d'un Pokémon à partir de son nom\n");

                        // Type
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("type <type de Pokémon (en anglais)>");
                        Console.ResetColor();
                        Console.Write(" - obtient une liste de Pokémons ayant ce type\n");

                        // Exit
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("exit                               ");
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
