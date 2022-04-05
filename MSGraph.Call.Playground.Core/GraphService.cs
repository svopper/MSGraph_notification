using Microsoft.Graph;
using Microsoft.Graph.CallRecords;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;

namespace MSGraph.Call.Playground.Core
{
    public class GraphService
    {
        private readonly GraphServiceClient _graph;

        public GraphService(string clientId, string clientSecret)
        {
            var accessToken = AcquireAccessToken(clientId, clientSecret).Result;

            var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                (requestMessage) =>
                {
                // This is adding a bearer token to the httpclient used in the requests.
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    return Task.FromResult(0);
                }));
            
            _graph = graphClient;
        }

        private async Task<string> AcquireAccessToken(string clientId, string clientSecret)
        {
            var graphApiResource = "https://graph.microsoft.com";
            var microsoftLogin = new Uri("https://login.microsoftonline.com/");
            var tenantID = "";

            // The authority to ask for a token: Azure Active Directory.
            var authority = new Uri(microsoftLogin, tenantID).AbsoluteUri;
            var authenticationContext = new AuthenticationContext(authority);
            var clientCredential = new ClientCredential(clientId, clientSecret);

            // Picks up the bearer token.
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(graphApiResource, clientCredential);

            return authenticationResult.AccessToken;
        }


        public async Task<CallRecord> GetCallRecord(string id)
        {
            var callRecord = await _graph.Communications.CallRecords[id]
                .Request()
                .Expand("sessions($expand=segments)")
                .GetAsync();

            return callRecord;
        }

        public async Task CreateSubscription()
        {
            var subscription = new Subscription
            {
                ChangeType = "created",
                NotificationUrl = "webhook URL",
                LifecycleNotificationUrl = "lifecycle webhook URL",
                Resource = "communications/callRecords",
                ExpirationDateTime = new DateTimeOffset(DateTime.Now.AddMinutes(15)), //https://docs.microsoft.com/en-us/graph/api/resources/subscription?view=graph-rest-1.0&preserve-view=true#maximum-length-of-subscription-per-resource-type
                ClientState = "secretClientValue",
                LatestSupportedTlsVersion = "v1_2"
            };

            await _graph.Subscriptions
                .Request()
                .AddAsync(subscription);

        }

        public async Task UpdateSubscription(Guid subscriptionId, DateTimeOffset expiryDate)
        {
            var subscription = new Subscription
            {
                ExpirationDateTime = expiryDate
            };

            await _graph.Subscriptions[subscriptionId.ToString()]
                .Request()
                .UpdateAsync(subscription);
        }

        public async Task DeleteSubscriptions()
        {
            var subs = await _graph.Subscriptions.Request().GetAsync();

            foreach (var sub in subs.Where(s => s.Resource == "communications/callRecords"))
            {
                await _graph.Subscriptions[sub.Id]
                .Request()
                .DeleteAsync();
            }
        }
    }
}
