using System;
using Newtonsoft.Json;

namespace IDSApi
{
    public partial class GraphContext
    {
        [JsonProperty("@graph")]
        public virtual Graph[] Graph { get; set; }

        [JsonProperty("@context")]
        public virtual Context Context { get; set; }

        [JsonProperty("@type")]
        public virtual string Type { get; set; }

        [JsonProperty("@id")]
        public virtual string Id { get; set; }

        [JsonProperty("ids:version")]
        public virtual string IdsVersion { get; set; }

        [JsonProperty("ids:publicKey")]
        public virtual IdTypeKeyType IdsPublicKey { get; set; }

        [JsonProperty("ids:description")]
        public virtual ValueTypeType[] IdsDescription { get; set; }

        [JsonProperty("ids:title")]
        public virtual ValueTypeType[] IdsTitle { get; set; }

        [JsonProperty("ids:hasDefaultEndpoint")]
        public virtual IdTypeAccessType IdsHasDefaultEndpoint { get; set; }

        [JsonProperty("ids:resourceCatalog")]
        public virtual IdTypeType[] IdsResourceCatalog { get; set; }

        [JsonProperty("ids:securityProfile")]
        public virtual IdType IdsSecurityProfile { get; set; }

        [JsonProperty("ids:maintainer")]
        public virtual IdType IdsMaintainer { get; set; }

        [JsonProperty("ids:curator")]
        public virtual IdType IdsCurator { get; set; }

        [JsonProperty("ids:inboundModelVersion")]
        public virtual string[] IdsInboundModelVersion { get; set; }

        [JsonProperty("ids:outboundModelVersion")]
        public virtual string IdsOutboundModelVersion { get; set; }

    }

    public partial class Context
    {
        [JsonProperty("ids")]
        public virtual string Ids { get; set; }

        [JsonProperty("idsc")]
        public virtual string Idsc { get; set; }

        [JsonProperty("sameAs")]
        public virtual IdTypeType SameAs { get; set; }

        [JsonProperty("keyValue")]
        public virtual IdType KeyValue { get; set; }

        [JsonProperty("keyType")]
        public virtual IdTypeType KeyType { get; set; }

        [JsonProperty("accessURL")]
        public virtual IdTypeType AccessUrl { get; set; }

        [JsonProperty("inboundModelVersion")]
        public virtual IdType InboundModelVersion { get; set; }

        [JsonProperty("maintainer")]
        public virtual IdTypeType Maintainer { get; set; }

        [JsonProperty("publicKey")]
        public virtual IdTypeType PublicKey { get; set; }

        [JsonProperty("title")]
        public virtual IdType Title { get; set; }

        [JsonProperty("hasDefaultEndpoint")]
        public virtual IdTypeType HasDefaultEndpoint { get; set; }

        [JsonProperty("curator")]
        public virtual IdTypeType Curator { get; set; }

        [JsonProperty("securityProfile")]
        public virtual IdTypeType SecurityProfile { get; set; }

        [JsonProperty("description")]
        public virtual IdType Description { get; set; }

        [JsonProperty("version")]
        public virtual IdType Version { get; set; }

        [JsonProperty("outboundModelVersion")]
        public virtual IdType OutboundModelVersion { get; set; }

        [JsonProperty("resourceCatalog")]
        public virtual IdTypeType ResourceCatalog { get; set; }

        [JsonProperty("listedConnector")]
        public virtual IdTypeType ListedConnector { get; set; }

        [JsonProperty("owl")]
        public virtual string Owl { get; set; }
    }

    public partial class IdType
    {
        [JsonProperty("@id")]
        public virtual string Id { get; set; }
    }

    public partial class IdTypeType
    {
        [JsonProperty("@id")]
        public virtual string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    public partial class IdTypeAccessType
    {
        [JsonProperty("@id")]
        public virtual string Id { get; set; }

        [JsonProperty("@type")]
        public virtual string Type { get; set; }

        [JsonProperty("ids:accessURL")]
        public virtual IdType IdsAccessUrl { get; set; }
    }

    public partial class IdTypeKeyType
    {
        [JsonProperty("@id")]
        public virtual string Id { get; set; }

        [JsonProperty("@type")]
        public virtual string Type { get; set; }

        [JsonProperty("ids:keyType")]
        public virtual IdType IdsKeyType { get; set; }

        [JsonProperty("ids:keyValue")]
        public virtual string IdsKeyValue { get; set; }
    }

    public partial class ValueTypeType
    {
        [JsonProperty("@value")]
        public virtual string Value { get; set; }

        [JsonProperty("@type")]
        public virtual string Type { get; set; }
    }

    public partial class Graph
    {
        [JsonProperty("@id")]
        public virtual string Id { get; set; }

        [JsonProperty("@type")]
        public virtual string Type { get; set; }

        [JsonProperty("listedConnector", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string[] ListedConnector { get; set; }

        [JsonProperty("sameAs", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string SameAs { get; set; }

        [JsonProperty("curator", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Curator { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Description { get; set; }

        [JsonProperty("hasDefaultEndpoint", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string HasDefaultEndpoint { get; set; }

        [JsonProperty("inboundModelVersion", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string[] InboundModelVersion { get; set; }

        [JsonProperty("maintainer", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Maintainer { get; set; }

        [JsonProperty("outboundModelVersion", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string OutboundModelVersion { get; set; }

        [JsonProperty("publicKey", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string PublicKey { get; set; }

        [JsonProperty("resourceCatalog", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string[] ResourceCatalog { get; set; }

        [JsonProperty("securityProfile", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string SecurityProfile { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Title { get; set; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Version { get; set; }

        [JsonProperty("accessURL", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string AccessUrl { get; set; }

        [JsonProperty("keyType", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string KeyType { get; set; }

        [JsonProperty("keyValue", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string KeyValue { get; set; }
    }

}
