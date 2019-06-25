using Heroes.Data.Models;
using Newtonsoft.Json;

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
            HeroDocument heroDocument)
        {

            this.Id = heroDocument.Id;
            this.Object = heroDocument.Object;
            this.Name = heroDocument.Name;
            this.Gender = heroDocument.Gender;
            this.Affiliations = heroDocument.Affiliations;
            this.Powers = heroDocument.Powers;
            this.Notes = heroDocument.Notes;
            this.CreatedOn = heroDocument.CreatedOn;
        }
    }
}
