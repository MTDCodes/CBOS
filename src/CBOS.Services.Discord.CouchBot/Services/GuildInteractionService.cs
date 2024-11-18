using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace CBOS.Services.Discord.CouchBot.Services
{
    public class GuildInteractionService(DiscordSocketClient discordSocketClient,
        ILogger<GuildInteractionService> logger)
    {
        // Initialize Guild Interactions for Discord Client
        public Task Init()
        {
            discordSocketClient.JoinedGuild += ProcessBotJoinedGuild;
            discordSocketClient.LeftGuild += ProcessBotLeftGuild;
            discordSocketClient.UserJoined += ProcessUserJoinedGuild;
            discordSocketClient.UserLeft += ProcessUserLeftGuild;

            return Task.CompletedTask;
        }

        // Event fired when a user leaves a guild that the bot is in.
        private Task ProcessUserLeftGuild(SocketGuild arg1, SocketUser arg2)
        {
            logger.LogInformation("[User Left] {User} left {Guild}",
                arg2.Username,
                arg1.Name);

            return Task.CompletedTask;
        }

        // Event fired when a user joins a guild that the bot is in.
        private Task ProcessUserJoinedGuild(SocketGuildUser arg)
        {
            logger.LogInformation("[User Joined] {User} joined {Guild}",
                arg.Username,
                arg.Guild.Name);

            return Task.CompletedTask;
        }

        // Event fires when the bot leaves a guild.
        private Task ProcessBotLeftGuild(SocketGuild arg)
        {
            logger.LogInformation("[Bot Left] {User} left {Guild}",
                discordSocketClient.CurrentUser.Username,
                arg.Name);

            return Task.CompletedTask;
        }

        // Event fires when the bot joins a guild.
        private Task ProcessBotJoinedGuild(SocketGuild arg)
        {
            logger.LogInformation("[Bot Joined] {User} joined {Guild}",
                discordSocketClient.CurrentUser.Username,
                arg.Name);

            return Task.CompletedTask;
        }
    }
}
