using System;

namespace LPRGI.Pokedex
{
    class Program
    {
        static void Main()
        {
            using var pokedexCient = new Request.PokedexClient();
            Console.WriteLine(
                "Bienvenue dans le Pokédex!\n" +
                "Entrez 'help' pour afficher les commandes");

            var input = string.Empty;
            bool exit = false;

            try
            {
                do
                {
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case var cmd when input.Equals("help", StringComparison.CurrentCultureIgnoreCase):
                            Console.WriteLine(
                                "Commandes :\n" +
                                ">>> name <nom du Pokémon>");
                            break;

                        case var cmd when input.StartsWith("name", StringComparison.CurrentCultureIgnoreCase):
                            var pokemonName = input.Split(' ')[1];
                            Console.WriteLine(pokedexCient.GetPokemonAsync(pokemonName).Result);
                            break;

                        case var cmd when input.StartsWith("exit", StringComparison.CurrentCultureIgnoreCase):
                            exit = true;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Commande inconnue, entrez 'help' pour plus d'informations");
                            Console.ResetColor();
                            break;
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
