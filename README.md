# MSGraph.Call.Playground

_Oprettet 22/2/22_
> Kaspers legeplads til undersøgelse af callRecord i MS Graph

## Baggrund

Dette projekt er en PoC af dims der rapporterer opkaldshistorik ind i DEALOG 3.0. Det kræves at [ny webhook subscription sættes op hver 3. dag](https://portal.azure.com/#@danskerhverv.org/resource/subscriptions/0b659584-ca18-43df-9bc1-bdc0b901b241/resourceGroups/DE-Playground/providers/Microsoft.Web/sites/msgraph-callrecord-hook/appServices) grundes MS Graph-begrænsning. Denne subscription rapporterer til endpoint - i dette tilfælde en Azure Function.

```csharp
    var subscription = new Subscription // Properties: https://docs.microsoft.com/en-us/graph/api/resources/subscription?view=graph-rest-1.0#properties
    {
        ChangeType = "created",
        NotificationUrl = "URL TO NOTIFICATION", // url til Azure Function
        Resource = "communications/callRecords",
        ExpirationDateTime = DateTimeOffset.Parse("2022-02-24T18:23:45.9356913Z"),
        ClientState = "secretClientValue",
        LatestSupportedTlsVersion = "v1_2"
    };

    await graphClient.Subscriptions
        .Request()
        .AddAsync(subscription);
```

## Azure Ressourcer

[msgraph-callrecord-hook](https://portal.azure.com/#@danskerhverv.org/resource/subscriptions/0b659584-ca18-43df-9bc1-bdc0b901b241/resourceGroups/DE-Playground/providers/Microsoft.Web/sites/msgraph-callrecord-hook/appServices) - Azure Function for MSGraph.Call.Playground.Functions

## Links

[callRecord docs](https://docs.microsoft.com/en-us/graph/api/resources/callrecords-api-overview?view=graph-rest-1.0)

[callRecord latency](2a7ed85f-b772-4802-9e1e-c36f3bfff8a4) - forventede latency fra opkald afsluttes til notifikation sendes

[Generelt om Graph webhooks](https://docs.microsoft.com/en-us/graph/webhooks)

[Generel opsætning af Graph webhooks](https://docs.microsoft.com/en-us/graph/api/resources/webhooks?view=graph-rest-1.0)

[Endpoint-validering i Graph webhooks](https://docs.microsoft.com/en-us/graph/webhooks#notification-endpoint-validation)

[Triggers af webhook i Azure Portal](https://portal.azure.com/#blade/WebsitesExtension/FunctionMenuBlade/monitor/resourceId/%2Fsubscriptions%2F0b659584-ca18-43df-9bc1-bdc0b901b241%2FresourceGroups%2FDE-Playground%2Fproviders%2FMicrosoft.Web%2Fsites%2Fmsgraph-callrecord-hook%2Ffunctions%2FCallRecordHook)

## callRecord - eksempler

### Udgående kald uden svar
71f373e5-1276-4924-97e5-93e0006a6870

### Udgående kald med svar (Ikke bekræftet)
38b374c3-657e-4a91-927e-77cd0b98d08a
