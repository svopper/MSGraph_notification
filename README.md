# MSGraph.Call.Playground

_Oprettet 22/2/22_
> Kaspers legeplads til undersøgelse af callRecord i MS Graph

## Baggrund

Dette projekt er en PoC af dims der rapporterer opkaldshistorik ind i DEALOG 3.0. Det kræves at ny webhook subscription sættes op hver 3. dag grundes MS Graph-begrænsning. Denne subscription rapporterer til endpoint - i dette tilfælde en Azure Function.

```csharp
    var subscription = new Subscription // Properties: https://docs.microsoft.com/en-us/graph/api/resources/subscription?view=graph-rest-1.0#properties
    {
        ChangeType = "created",
        NotificationUrl = "webhook URL", // url til Azure Function
        Resource = "communications/callRecords",
        ExpirationDateTime = DateTimeOffset.Parse("2022-02-24T18:23:45.9356913Z"),
        ClientState = "secretClientValue",
        LatestSupportedTlsVersion = "v1_2"
    };

    await graphClient.Subscriptions
        .Request()
        .AddAsync(subscription);
```

## Links

[callRecord docs (docs.microsoft.com)](https://docs.microsoft.com/en-us/graph/api/resources/callrecords-api-overview?view=graph-rest-1.0)

[callRecord latency (docs.microsoft.com)](https://docs.microsoft.com/en-us/graph/webhooks#latency) - forventede latency fra opkald afsluttes til notifikation sendes

[Generelt om Graph webhooks (docs.microsoft.com)](https://docs.microsoft.com/en-us/graph/webhooks)

[Generel opsætning af Graph webhooks (docs.microsoft.com)](https://docs.microsoft.com/en-us/graph/api/resources/webhooks?view=graph-rest-1.0)

[Endpoint-validering i Graph webhooks (docs.microsoft.com)](https://docs.microsoft.com/en-us/graph/webhooks#notification-endpoint-validation)

