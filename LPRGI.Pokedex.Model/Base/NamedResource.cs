using Newtonsoft.Json;

namespace LPRGI.Pokedex.Model.Base
{
    /// <summary>
    /// Représentation d'un objet JSON qui contient un nom et un URL.
    /// </summary>
    public class NamedResource : Resource
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
