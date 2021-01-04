using System;

namespace LPRGI.Pokedex.Request.Exceptions
{
    /// <summary>
    /// Exception levée quand l'utilisateur recherche un <see cref="Model.Type"/> de <see cref="Model.PokemonModel.Pokemon"/>
    /// qui n'existe pas.
    /// </summary>
    public class UnknownPokemonTypeException : Exception
    {
        public UnknownPokemonTypeException(string message) : base(message) { }
    }
}
