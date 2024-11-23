using CBOS.Domain.Dtos;

namespace CBOS.Accessors.Contracts;

public  interface IDiscordUserAccessor
{
    Task<List<DiscordUserDto>> ListAsync();
    Task CreateAsync(DiscordUserDto discordUserDto);
    Task UpdateAsync(DiscordUserDto discordUserDto);
    Task<DiscordUserDto> RetrieveAsync(string discordUserId);
}
