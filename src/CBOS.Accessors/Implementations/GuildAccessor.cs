using AutoMapper;
using CBOS.Accessors.Contracts;
using CBOS.Data.Entities;
using CBOS.Domain.Dtos;

namespace CBOS.Accessors.Implementations;

public class GuildAccessor(CBOSContext context,
    Mapper mapper) : IGuildAccessor
{
    public Task CreateAsync(GuildDto guildDto)
    {
        throw new NotImplementedException();
    }

    public Task<List<GuildDto>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task RetrieveAsync(string guildId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(GuildDto guildDto)
    {
        throw new NotImplementedException();
    }
}
