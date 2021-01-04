using LPRGI.Pokedex.Request;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Command
{
    public static class Input
    {
        static readonly string[] Commands = new string[]
        {
            "name",
            "type",
            "help",
            "exit"
        };

        /// <summary>
        /// Analyse la saisie de l'utilisateur et vérifie si elle est valide.
        /// </summary>
        /// <param name="input">La saisie de l'utilisateur sous la forme d'une chaîne de caractères.</param>
        /// <returns>Un tableau de chaînes de caractères contenant une commande et éventuellement des paramètres.</returns>
        public static string[] Parse(this string input)
        {
            // Séparation des arguments et récupération de la commande
            var args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = args[0];

            // Une commande qui est écrite en lettres majuscules est valide
            // Dans ce cas, on préfère la convertir en lettres minuscules
            return Commands.Contains(command, StringComparer.CurrentCultureIgnoreCase) ?
                Array.ConvertAll(args, (item) => item.ToLower()) : throw new UnknownCommandException(
                    "Commande inconnue, entrez 'help' pour plus d'informations");
        }

        /// <summary>
        /// Envoie une requête vers L'API à partir d'arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="pokedexClient"></param>
        /// <returns></returns>
        public static async Task RequestAsync(this string[] args, PokedexClient pokedexClient)
        {
            var command = args[0];

            switch (command)
            {
                // Recherche d'un Pokémon par son nom
                case var cmd when command == "name":
                    var pokemonName = args[1];
                    var pokemon = await pokedexClient.GetPokemonAsync(pokemonName);
                    Console.WriteLine(pokemon);
                    break;

                // Recherche de Pokémons avec un type
                case var cmd when command == "type":
                    var type = args[1];
                    var pokemonsByType = await pokedexClient.GetPokemonsByTypeAsync(type);

                    var pokemonNames = pokemonsByType.Pokemons.Select((pokemon) => pokemon.PokemonResource.Name);
                    var pokemonNamesJoined = string.Join(", ", pokemonNames);

                    Console.WriteLine(pokemonNamesJoined);
                    break;

                // Affichage de l'aide
                case var cmd when command == "help":
                    Console.WriteLine("Commandes :");

                    // Nom
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("name <nom du Pokémon>              ");
                    Console.ResetColor();
                    Console.Write(" - obtient les détails d'un Pokémon à partir de son nom\n");

                    // Type
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("type <type de Pokémon (en anglais)>");
                    Console.ResetColor();
                    Console.Write(" - obtient une liste de Pokémons ayant ce type\n");

                    // Sortie
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("exit                               ");
                    Console.ResetColor();
                    Console.Write(" - sortie du programme\n");
                    break;

                // Sortie du programme
                case var cmd when command == "exit":
                    Program.Exit = true;
                    break;
            }
        }
    }
}
