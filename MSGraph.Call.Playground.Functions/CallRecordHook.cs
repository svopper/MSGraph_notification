using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MSGraph.Call.Playground.Core;
using MSGraph.Call.Playground.Core.Models;

namespace MSGraph.Call.Playground.Functions
{
    public static class CallRecordHook
    {
        [FunctionName("CallRecordHook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Recieved call record");
            var validationToken = req.Query["validationToken"].ToString();

            if(validationToken != "")
            {
                log.LogInformation(validationToken);
                req.HttpContext.Response.Headers.Add("Content-Type", "text/plain");
                return new OkObjectResult(validationToken);
            }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            ChangeNotificationResponse changeNotification = JsonConvert.DeserializeObject<ChangeNotificationResponse>(requestBody);

            var cosmosService = new CosmosService();

            foreach (var item in changeNotification.Value)
            {
                var record = await GetCallRecord(item.ResourceData.Id);
                await cosmosService.PostCallRecordAsync(record);
            }
            cosmosService.Dispose();
            return new OkResult();
        }

        private static async Task<Microsoft.Graph.CallRecords.CallRecord> GetCallRecord(string callRecordId)
        {
            var graphService = new GraphService("client_id", "client_secret");

            var record = await graphService.GetCallRecord(callRecordId);

            return record;
        }
    }
}
