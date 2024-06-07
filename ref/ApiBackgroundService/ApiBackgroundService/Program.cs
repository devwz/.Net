using ApiBackgroundService;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder();

builder.Services.AddHostedService<QueuedBackgroundService>();
builder.Services.AddSingleton<IBackgroundQueue, BackgroundQueue>();

WebApplication webApp  = builder.Build();

Random random = new Random();

string LoadQuote()
{
    string[] quotes = new string[]
    {
        "Chamando o suporte técnico...",
        "Capinando um terreno...",
        "Fazendo um café...",
        "Desfazendo nós em cabos de rede...",
        "Colocando ketchup no código fonte...",
        "Tirando uma soneca de 4 segundos...",
        "Tentando parecer ocupado...",
        "Aguardando John Doe..."
    };

    return quotes[random.Next(0, quotes.Length)];
}

webApp.MapGet("/background", (
    [FromServices] IBackgroundQueue queue,
    [FromServices] ILogger<Program> logger,
    [FromServices] IHostApplicationLifetime applicationLifetime,
    HttpRequest httpRequest) =>
{
    // _ = httpRequest;

    queue.QueueBackgroundWorkItem(async token =>
    {
        int delayLoop = 0;
        // string id = httpRequest.HttpContext.Request.Headers["id"];
        string id = "well";
        string guid = Guid.NewGuid().ToString();
        logger.LogInformation("Queued Background Task {Guid} starting.", guid);

        while (!token.IsCancellationRequested && delayLoop < 4)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(4), token);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if the Delay is cancelled
                logger.LogInformation("Queued Background Task {Guid} cancelled.", guid);
            }

            delayLoop++;

            logger.LogInformation("{Guid} - {Id}: {Quote}. " +
                "{DelayLoop}/4", guid, id, LoadQuote(), delayLoop);

            if (delayLoop == 4)
            {
                logger.LogInformation("Queued Background Task {Guid} complete.", guid);
            }
        }

    });

    return "200";
});

webApp.Run();