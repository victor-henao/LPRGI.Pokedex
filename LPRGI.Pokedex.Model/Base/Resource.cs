using Newtonsoft.Json;

namespace LPRGI.Pokedex.Model.Base
{
    /// <summary>
    /// Représentation d'un objet JSON qui contient un URL.
    /// </summary>
    public class Resource
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
