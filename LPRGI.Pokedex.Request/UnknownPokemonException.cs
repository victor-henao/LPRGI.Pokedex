using System;

namespace LPRGI.Pokedex.Request
{
    public class UnknownPokemonException : Exception
    {
        public UnknownPokemonException(string message) : base(message) { }
    }
}
