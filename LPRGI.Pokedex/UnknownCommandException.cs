using System;

namespace LPRGI.Pokedex
{
    /// <summary>
    /// Exception levée quand l'utilisateur tape une commande inconnue.
    /// </summary>
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException(string message) : base(message) { }
    }
}
