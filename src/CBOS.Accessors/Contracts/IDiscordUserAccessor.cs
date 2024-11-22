using CBOS.Domain.Dtos;

namespace CBOS.Accessors.Contracts;

public  interface IDiscordUserAccessor
{
    Task<List<DiscordUserDto>> ListAsync();
    Task CreateAsync(DiscordUserDto DiscordUserDto);
    Task UpdateAsync(DiscordUserDto DiscordUserDto);
    Task RetrieveAsync(string discordUserId);
}
