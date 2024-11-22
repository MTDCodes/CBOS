using CBOS.Services.Discord.CouchBot.Exceptions;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CBOS.Services.Discord.CouchBot.Services.Hosted;

public class DiscordBotService(DiscordBotCommandService discordBotCommandService,
    DiscordSocketClient discordSocketClient,
    GuildInteractionService guildInteractionService,
    ILogger<DiscordBotService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Discord Bot Service is starting.");

        var discordBotToken = Environment.GetEnvironmentVariable("CBOS_DISCORD_BOT_TOKEN");
        if (discordBotToken == null)
        {
            logger.LogCritical("Discord Bot Token Environment Variable is missing.");
            throw new DiscordBotTokenException();
        }

        logger.LogInformation("Logging into Discord and starting the connection.");

        await discordSocketClient
            .LoginAsync(TokenType.Bot, discordBotToken)
            .ConfigureAwait(false);
        await discordSocketClient
            .StartAsync()
            .ConfigureAwait(false);

        logger.LogInformation("Connection to Discord has been established.");

        while(discordSocketClient.CurrentUser == null)
        {
            logger.LogInformation("Discord User connection pending ...");
            await Task
                .Delay(TimeSpan.FromSeconds(5), cancellationToken)
                .ConfigureAwait(false);
        }

        logger.LogInformation("Discord user connected: {DiscordUser}",
            discordSocketClient.CurrentUser.Username);

        await discordBotCommandService.Init();
        await guildInteractionService.Init();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Discord Bot Service is stopping.");

        return Task.CompletedTask;
    }
}
