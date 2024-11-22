using CBOS.Domain.Dtos;

namespace CBOS.Accessors.Contracts;

public interface IGuildAccessor
{
    Task<List<GuildDto>> ListAsync();
    Task CreateAsync(GuildDto guildDto);
    Task UpdateAsync(GuildDto guildDto);
    Task RetrieveAsync(string guildId);
}
