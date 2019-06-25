using Newtonsoft.Json;
using System;

namespace Heroes.Data
{
    public interface IDocument
    {
        [JsonProperty("id")]
        Guid Id { get; set; }

        [JsonProperty("object")]
        string Object { get; }

        [JsonProperty("created_on")]
        DateTime CreatedOn { get; set; }
    }
}
