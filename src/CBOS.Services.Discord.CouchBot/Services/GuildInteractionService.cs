using CBOS.Accessors.Contracts;
using CBOS.Domain.Dtos;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace CBOS.Services.Discord.CouchBot.Services;

public class GuildInteractionService(DiscordSocketClient discordSocketClient,
    IDiscordUserAccessor discordUserAccessor,
    IGuildAccessor guildAccessor,
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
    private async Task ProcessBotLeftGuild(SocketGuild arg)
    {
        logger.LogInformation("[Bot Left] {User} left {Guild}",
            discordSocketClient.CurrentUser.Username,
            arg.Name);

        var existingGuild = await guildAccessor.RetrieveAsync(arg.Id.ToString());
        if (existingGuild == null)
        {
            logger.LogDebug("[Bot Left] Bot wasn't in {Guild}",
                arg.Name);
            return;
        }

        existingGuild.Enabled = false;
        existingGuild.ModifiedDate = DateTime.UtcNow;
        await guildAccessor.UpdateAsync(existingGuild);

        logger.LogInformation("[Bot Left] {Guild} disabled",
            arg.Name);
    }

    // Event fires when the bot joins a guild.
    private async Task ProcessBotJoinedGuild(SocketGuild arg)
    {
        logger.LogInformation("[Bot Joined] {User} joined {Guild}",
            discordSocketClient.CurrentUser.Username,
            arg.Name);

        var existingGuild = await guildAccessor.RetrieveAsync(arg.Id.ToString());
        if (existingGuild != null)
        {
            logger.LogDebug("[Bot Joined] {Guild} already exists. Enabling.",
                arg.Name);
            existingGuild.Enabled = true;
            existingGuild.ModifiedDate = DateTime.UtcNow;
            await guildAccessor.UpdateAsync(existingGuild);
            return;
        }

        var existingDiscordUser = await discordUserAccessor.RetrieveAsync(arg.OwnerId.ToString());
        if (existingDiscordUser == null)
        {
            var owner = arg.GetUser(arg.OwnerId);
            logger.LogDebug("[Bot Joined] {User} didn't exist. Creating.",
                owner.Username);
            await discordUserAccessor.CreateAsync(new DiscordUserDto
            {
                CreatedDate = DateTime.UtcNow,
                Id = arg.OwnerId.ToString(),
                ModifiedDate = DateTime.UtcNow,
                Name = owner.Username,
            });
        }

        await guildAccessor.CreateAsync(new GuildDto
        {
            CreatedDate = DateTime.UtcNow,
            Enabled = true,
            Id = arg.Id.ToString(),
            ModifiedDate = DateTime.UtcNow,
            Name = arg.Name,
            OwnerId = arg.OwnerId.ToString(),
        });

        logger.LogInformation("[Bot Joined] {Guild} created",
            arg.Name);
    }
}
