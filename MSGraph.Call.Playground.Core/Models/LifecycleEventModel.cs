using Microsoft.Graph;
using Newtonsoft.Json;
using System;

namespace MSGraph.Call.Playground.Functions
{
    public partial class LifecycleEventModel
    {
        [JsonProperty("value")]
        public Value[] Value { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("lifecycleEvent")]
        public string LifecycleEvent { get; set; }

        [JsonProperty("subscriptionId")]
        public Guid SubscriptionId { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("clientState")]
        public string ClientState { get; set; }

        [JsonProperty("resourceData")]
        public ResourceData ResourceData { get; set; }

        [JsonProperty("encryptedContent")]
        public ChangeNotificationEncryptedContent EncryptedContent { get; set; }

        [JsonProperty("organizationId")]
        public Guid OrganizationId { get; set; }

        [JsonProperty("subscriptionExpirationDateTime")]
        public DateTimeOffset SubscriptionExpirationDateTime { get; set; }
    }

    public partial class ResourceData
    {
        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("@odata.id")]
        public string OdataId { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
