using AutoMapper;
using CBOS.Accessors.Contracts;
using CBOS.Data.Entities;
using CBOS.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CBOS.Accessors.Implementations;

public class DiscordUserAccessor(CBOSContext context,
    IMapper mapper) : IDiscordUserAccessor
{
    public async Task CreateAsync(DiscordUserDto discordUserDto)
    {
        var entity = mapper.Map<Discorduser>(discordUserDto);
        var t = await context
            .Discordusers
            .AddAsync(entity)
            .ConfigureAwait(false);
        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public Task<List<DiscordUserDto>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<DiscordUserDto> RetrieveAsync(string discordUserId)
    {
        var entity = await context
            .Discordusers
            .FirstOrDefaultAsync(x => x.Id == discordUserId)
            .ConfigureAwait(false);

        return mapper.Map<DiscordUserDto>(entity);
    }

    public Task UpdateAsync(DiscordUserDto DiscordUserDto)
    {
        throw new NotImplementedException();
    }
}
