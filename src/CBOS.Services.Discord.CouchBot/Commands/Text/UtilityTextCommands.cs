using Discord.Commands;

namespace CBOS.Services.Discord.CouchBot.Commands.Text
{
    public class UtilityTextCommands : ModuleBase
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await Context
                .Channel
                .SendMessageAsync("Pong!")
                .ConfigureAwait(false);
        }
    }
}
