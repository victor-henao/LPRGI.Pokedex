using System;

namespace LPRGI.Pokedex.Model
{
    public class UnknownPokemonException : Exception
    {
        public UnknownPokemonException(string message) : base(message) { }
    }
}
