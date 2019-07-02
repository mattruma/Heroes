using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heroes.Data.Models
{
    public class HeroEntity : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("powers")]
        public string Powers { get; set; }

        [Column("affiliations")]
        public string Affiliations { get; set; }

        [Column("notes")]
        public string Notes { get; set; }
    }
}
