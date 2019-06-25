using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Heroes.Domain.Models
{
    public class HeroAddOptions
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("powers")]
        public string[] Powers { get; set; }

        [JsonProperty("affiliations")]
        public string[] Affiliations { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
   }
}
