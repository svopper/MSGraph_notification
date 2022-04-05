using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MSGraph.Call.Playground.Core;

namespace MSGraph.Call.Playground.Functions
{
    public static class CallRecordSubscriptionLifeCycleHook
    {
        [FunctionName("CallRecordSubscriptionLifeCycleHook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var validationToken = req.Query["validationToken"].ToString();

            if (validationToken != "")
            {
                log.LogInformation(validationToken);
                req.HttpContext.Response.Headers.Add("Content-Type", "text/plain");
                return new OkObjectResult(validationToken);
            }

            var reader = new StreamReader(req.Body);
            
            var json = await reader.ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<LifecycleEventModel>(json);
            await ReauthorizeSubscription(request, log);
            log.LogInformation(await reader.ReadToEndAsync());
            return new AcceptedResult();
        }

        private static async Task ReauthorizeSubscription(LifecycleEventModel lifecycleEvent, ILogger log)
        {
            var graphService = new GraphService("client_id", "client_secret");
            var newExpiry = DateTime.Now.AddMinutes(60);
            log.LogInformation($"Subscription now expires {newExpiry}");
            var successful = false;
            do
            {
                try
                {
                    await graphService.UpdateSubscription(lifecycleEvent.Value[0].SubscriptionId, newExpiry);
                    successful = true;
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(5000);
                }
            } while (!successful);
        }
    }
}
