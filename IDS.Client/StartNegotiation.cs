using System;
using Newtonsoft.Json;

namespace IDSApi
{
    public partial class StartNegotiation
    {
        [JsonProperty("@type")]
        public virtual string Type { get; set; }

        [JsonProperty("@id", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Id { get; set; }

        [JsonProperty("ids:description")]
        public virtual ValueTypeType[] IdsDescription { get; set; }

        [JsonProperty("ids:title")]
        public virtual ValueTypeType[] IdsTitle { get; set; }

        [JsonProperty("ids:action")]
        public virtual IdType[] IdsAction { get; set; }

        [JsonProperty("ids:target")]
        public virtual string IdsTarget { get; set; }
    }
}