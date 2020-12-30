using System;
using System.Linq;

namespace LPRGI.Pokedex
{
    class Program
    {
        static void Main()
        {
            using var pokedexCient = new Request.PokedexClient();
            try
            {
                string input = string.Empty;
                Console.WriteLine("Entrez le nom ou le numéro d'un pokémon, 'exit' pour sortir");
                input = Console.ReadLine();
                bool isDigitPresent = input.Any(c => char.IsDigit(c));
                while (!isDigitPresent)
                {
                    //!string.Equals(input,"exit", StringComparison.CurrentCultureIgnoreCase) ||

                    Console.WriteLine(pokedexCient.GetPokemonAsync(input).Result);
                    input = Console.ReadLine();
                    isDigitPresent = input.Any(c => char.IsDigit(c));
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
