using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;

namespace OneDrive.Sample.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private static readonly string[] Scopes = { "https://graph.microsoft.com/.default" };


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        GraphServiceClient client = CreateGraphClient();
        ListItem result = await client.Drive.Root.ListItem.Request().GetAsync(stoppingToken);
        _logger.LogInformation("Drive root item count: {count}", result.Description.Length);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    private GraphServiceClient CreateGraphClient()
    {
        TokenCredential tokenCredential = new ClientSecretCredential(
            tenantId: "your-tenant-id",
            clientId: "your-client-id",
            clientSecret: "the-client-secret"
        );

        return new GraphServiceClient(tokenCredential, Scopes);
    }
}
