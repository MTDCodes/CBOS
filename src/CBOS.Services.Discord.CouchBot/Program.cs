using CBOS.Accessors.Contracts;
using CBOS.Accessors.Implementations;
using CBOS.Data.Entities;
using CBOS.Domain.Mapper;
using CBOS.Services.Discord.CouchBot.Services;
using CBOS.Services.Discord.CouchBot.Services.Hosted;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
            GatewayIntents =
                GatewayIntents.GuildVoiceStates |
                GatewayIntents.GuildScheduledEvents |
                GatewayIntents.DirectMessages |
                GatewayIntents.GuildIntegrations |
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildMessageReactions |
                GatewayIntents.GuildPresences |
                GatewayIntents.Guilds |
                GatewayIntents.GuildMessages,
        });

        // TODO - Do better :) 
        services.AddDbContext<CBOSContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("CBOS_CONNECTIONSTRING")));
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddSingleton(client);
        services.AddSingleton<CommandService>();
        services.AddSingleton(new InteractionService(client));
        services.AddSingleton<DiscordBotCommandService>();
        services.AddSingleton<GuildInteractionService>();

        services.AddScoped<IDiscordUserAccessor, DiscordUserAccessor>();
        services.AddScoped<IGuildAccessor, GuildAccessor>();
        
        services.AddHostedService<DiscordBotService>();
    });