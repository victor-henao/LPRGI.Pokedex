using System;

namespace LPRGI.Pokedex.Request.Exceptions
{
    /// <summary>
    /// Exception levée quand l'utilisateur recherche un <see cref="Model.PokemonModel.Pokemon"/> qui n'existe pas.
    /// </summary>
    public class UnknownPokemonException : Exception
    {
        public UnknownPokemonException(string message) : base(message) { }
    }
}
