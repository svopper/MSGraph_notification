using MSGraph.Call.Playground.Core;

namespace MSGraph.Call.Playground.Console;
class Program
{
    static async Task Main()
    {
        var graphService = new GraphService("client_id", "client_secret");

        //var callRecord = await graphClient.GetCallRecord("0bef90f1-fc05-425e-8d32-0de5bb57ae2f");
        await graphClient.CreateSubscription();
        //await graphClient.UpdateSubscription(new Guid("26c59458-a30d-464f-883d-04fbce1cfd21"), DateTimeOffset.Now.AddHours(2));
        //await graphClient.DeleteSubscriptions();

        System.Console.WriteLine("done");
    }
}
