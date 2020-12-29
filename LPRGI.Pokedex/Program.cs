using System;

namespace LPRGI.Pokedex
{
    class Program
    {
        static void Main()
        {
            using var pokedexCient = new Request.PokedexClient();
            try
            {
                Console.WriteLine(pokedexCient.GetPokemonAsync("bulbasaur").Result);
                Console.WriteLine(pokedexCient.GetPokemonAsync("ditto").Result);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
