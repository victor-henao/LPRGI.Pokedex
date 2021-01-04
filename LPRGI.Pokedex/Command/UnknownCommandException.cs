using System;

namespace LPRGI.Pokedex.Command
{
    /// <summary>
    /// Exception levée quand l'utilisateur tape une commande inconnue.
    /// </summary>
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException(string message) : base(message) { }
    }
}
