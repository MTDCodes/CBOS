using CBOS.Services.Discord.CouchBot.Services;
using CBOS.Services.Discord.CouchBot.Services.Hosted;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = CreateHostBuilder(args);
var cancellationTokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (_, _) =>
{
    cancellationTokenSource.Cancel();
};

var consoleTask = builder.RunConsoleAsync(cancellationTokenSource.Token);
consoleTask.Wait(cancellationTokenSource.Token);

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host
    .CreateDefaultBuilder(args)
    .ConfigureLogging((builder) =>
    {
        builder.ClearProviders();
        builder.AddConsole();
    })
    .ConfigureServices((context, services) =>
    {
        var client = new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Verbose,
        });
        services.AddSingleton(client);
        services.AddSingleton<CommandService>();
        services.AddSingleton(new InteractionService(client));
        services.AddSingleton<DiscordBotCommandService>();
        services.AddSingleton<GuildInteractionService>();
        services.AddHostedService<DiscordBotService>();
    });