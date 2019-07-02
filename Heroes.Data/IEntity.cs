using Newtonsoft.Json;
using System;

namespace Heroes.Data
{
    public interface IEntity
    {
        [JsonProperty("id")]
        Guid Id { get; set; }

        [JsonProperty("created_on")]
        DateTime CreatedOn { get; set; }
    }
}
