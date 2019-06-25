using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Heroes.Data.Models
{
    public class HeroDocument : BaseDocument
    {
        [JsonProperty("hero_id")]
        public Guid HeroId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("object")]
        public override string Object => "Hero";
    }
}
