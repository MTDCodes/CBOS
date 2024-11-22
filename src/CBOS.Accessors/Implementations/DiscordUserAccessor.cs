using AutoMapper;
using CBOS.Accessors.Contracts;
using CBOS.Data.Entities;
using CBOS.Domain.Dtos;

namespace CBOS.Accessors.Implementations;

public class DiscordUserAccessor(CBOSContext context,
    Mapper mapper) : IDiscordUserAccessor
{
    public Task CreateAsync(DiscordUserDto DiscordUserDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<DiscordUserDto>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task RetrieveAsync(string discordUserId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DiscordUserDto DiscordUserDto)
    {
        throw new NotImplementedException();
    }
}
