using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersAndPets.Models
{
    public class PetModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public PetType Type { get; set; }
    }

    public enum PetType
    {
        Dog,
        Cat,
        Fish
    }
}