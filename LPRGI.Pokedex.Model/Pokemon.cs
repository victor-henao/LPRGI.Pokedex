﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LPRGI.Pokedex.Model
{
    public partial class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonType> Types { get; set; }

        public override string ToString()
        {
            var types = string.Empty;

            foreach (var type in Types)
            {
                types += type.Type.Name + " ";
            }

            return $"Informations sur {Name} : id =  {Id}, types = {types}";
        }
    }
}
