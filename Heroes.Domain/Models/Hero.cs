using Heroes.Data.Models;
using Newtonsoft.Json;
using System;

namespace Heroes.Domain.Models
{
    public class Hero : BaseEntity
    {
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

        public Hero()
        {
        }

        public Hero(
            HeroEntity heroEntity)
        {

            this.Id = heroEntity.Id;
            this.Name = heroEntity.Name;
            this.Gender = heroEntity.Gender;

            if (!string.IsNullOrWhiteSpace(heroEntity.Affiliations))
            {
                this.Affiliations = heroEntity.Affiliations.Split(",", StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrWhiteSpace(heroEntity.Powers))
            {
                this.Powers = heroEntity.Powers.Split(",", StringSplitOptions.RemoveEmptyEntries);
            }

            this.Notes = heroEntity.Notes;
            this.CreatedOn = heroEntity.CreatedOn;
        }
    }
}
