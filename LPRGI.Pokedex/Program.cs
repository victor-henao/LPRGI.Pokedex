using LPRGI.Pokedex.Request.Exceptions;
using System;
using System.Threading.Tasks;

namespace LPRGI.Pokedex.Command
{
    class Program
    {
        public static bool Exit { get; set; } = false;

        /// <summary>
        /// Point d'entrée de l'application.
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            using var pokedexClient = new Request.PokedexClient();
            Console.WriteLine(
                "Bienvenue dans le Pokédex!\n" +
                "Entrez 'help' pour afficher les commandes\n");

            var input = string.Empty;
            var command = string.Empty;
            var args = Array.Empty<string>();

            do
            {
                Console.Write("> ");

                // Récupération des arguments
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

                // Requête vers L'API
                try
                {
                    if (!(command == string.Empty))
                    {
                        await args.RequestAsync(pokedexClient);
                    }
                }
                catch (UnknownPokemonException ex1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex1.Message);
                    Console.ResetColor();
                }
                catch (UnknownPokemonTypeException ex2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex2.Message);
                    Console.ResetColor();
                }
            } while (!Exit);
        }
    }
}
