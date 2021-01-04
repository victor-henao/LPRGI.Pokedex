using System;
using System.Linq;

namespace LPRGI.Pokedex.Command
{
    public static class Input
    {
        static readonly string[] Commands = new string[]
        {
            "name",
            "type",
            "help",
            "exit"
        };

        /// <summary>
        /// Analyse la saisie de l'utilisateur et vérifie si elle est valide.
        /// </summary>
        /// <param name="input">La saisie de l'utilisateur sous la forme d'une chaîne de caractères.</param>
        /// <returns>Un tableau de chaînes de caractères contenant une commande et éventuellement des paramètres.</returns>
        public static string[] Parse(this string input)
        {
            // Séparation des arguments et récupération de la commande
            var args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var command = args[0];

            // Une commande qui est écrite en lettres majuscules est valide
            // Dans ce cas, on préfère la convertir en lettres minuscules
            return Commands.Contains(command, StringComparer.CurrentCultureIgnoreCase) ?
                Array.ConvertAll(args, (item) => item.ToLower()) : throw new UnknownCommandException(
                    "Commande inconnue, entrez 'help' pour plus d'informations");
        }
    }
}
