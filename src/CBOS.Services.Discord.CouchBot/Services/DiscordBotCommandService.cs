using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System.Reflection;

namespace CBOS.Services.Discord.CouchBot.Services
{
    public class DiscordBotCommandService(CommandService commandService, 
        DiscordSocketClient discordSocketClient,
        InteractionService interactionService,
        IServiceProvider serviceProvider)
    {
        public async Task Init()
        {
            discordSocketClient.SlashCommandExecuted += ProcessSlashCommand;
            discordSocketClient.MessageReceived += ProcessMessageReceived;

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(),
                serviceProvider)
                .ConfigureAwait(false);
            await interactionService.AddModulesAsync(Assembly.GetEntryAssembly(),
                serviceProvider)
                .ConfigureAwait(false);
        }

        private async Task ProcessMessageReceived(SocketMessage arg)
        {
            if (arg is not SocketUserMessage message)
            {
                return;
            }

            var argPos = 0;

            if(!(message.HasMentionPrefix(discordSocketClient.CurrentUser, ref argPos)) || message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(discordSocketClient, message);
            await commandService.ExecuteAsync(context,
                argPos,
                serviceProvider)
                .ConfigureAwait(false);
        }

        private async Task ProcessSlashCommand(SocketSlashCommand arg)
        {
            var context = new SocketInteractionContext<SocketSlashCommand>(discordSocketClient, arg);
            await interactionService
                .ExecuteCommandAsync(context, serviceProvider)
                .ConfigureAwait(false);
        }
    }
}
