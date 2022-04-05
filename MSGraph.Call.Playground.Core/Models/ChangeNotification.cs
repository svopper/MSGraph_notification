using Newtonsoft.Json;

namespace MSGraph.Call.Playground.Core.Models
{
    public partial record ChangeNotificationResponse
    {
        [JsonProperty("value")]
        public ChangeNotification[] Value { get; set; }
    }

    public partial record ChangeNotification
    {
        [JsonProperty("tenantId")]
        public Guid TenantId { get; set; }

        [JsonProperty("subscriptionId")]
        public Guid SubscriptionId { get; set; }

        [JsonProperty("clientState")]
        public string ClientState { get; set; }

        [JsonProperty("changeType")]
        public string ChangeType { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("subscriptionExpirationDateTime")]
        public DateTimeOffset SubscriptionExpirationDateTime { get; set; }

        [JsonProperty("resourceData")]
        public ResourceData ResourceData { get; set; }
    }

    public partial record ResourceData
    {
        [JsonProperty("oDataType")]
        public string ODataType { get; set; }

        [JsonProperty("oDataId")]
        public string ODataId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
