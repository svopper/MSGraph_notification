using Microsoft.Azure.Cosmos;
using Microsoft.Graph;
using MSGraph.Call.Playground.Core.Models;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MSGraph.Call.Playground.Core
{
    public class CosmosService : IDisposable
    {
        // The Azure Cosmos DB endpoint
        private static readonly string endpointUri = "";
        // The primary key for the Azure Cosmos account.
        private static readonly string primaryKey = "";

        private readonly CosmosClient _client;
        public CosmosService()
        {
            var options = new CosmosClientOptions()
            {
                ApplicationName = "CallRecordStorage",
                SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase }
            };
            _client = new CosmosClient(endpointUri, primaryKey, options);
        }

        public async Task PostCallRecordAsync(Microsoft.Graph.CallRecords.CallRecord callRecord)
        {
            var databaseId = "TeamsCallRecords";
            var containerId = "Items";

            try
            {
                var serializer = new Serializer();
                var json = serializer.SerializeObject(callRecord);
                var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.CallRecord>(json);

                var objectToCreate = new CosmosCallRecord { Id = Guid.NewGuid().ToString(), Type = deserialized.Type, CallRecord = deserialized };

                var database = _client.GetDatabase(databaseId);
                await database.CreateContainerIfNotExistsAsync(containerId, "/type");
                var container = database.GetContainer(containerId);

                var response = await container.CreateItemAsync(objectToCreate, new PartitionKey(objectToCreate.Type));
                
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", response.Resource.Id, response.RequestCharge);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception thrown: {0} \n", e);
            }
        }
        public void Dispose()
        {
            _client.Dispose();
        }
    }


    public class CosmosTest
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
