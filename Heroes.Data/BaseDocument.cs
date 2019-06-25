using Newtonsoft.Json;
using System;

namespace Heroes.Data
{
    public abstract class BaseDocument: IDocument
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("object")]
        public abstract string Object { get; }

        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }
    }
}
