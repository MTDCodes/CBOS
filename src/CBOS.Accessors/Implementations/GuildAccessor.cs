using AutoMapper;
using CBOS.Accessors.Contracts;
using CBOS.Data.Entities;
using CBOS.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CBOS.Accessors.Implementations;

public class GuildAccessor(CBOSContext context,
    IMapper mapper) : IGuildAccessor
{
    public async Task CreateAsync(GuildDto guildDto)
    {
        var entity = mapper.Map<Guild>(guildDto);
        var t = await context
            .Guilds
            .AddAsync(entity)
            .ConfigureAwait(false);
        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public Task<List<GuildDto>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<GuildDto> RetrieveAsync(string guildId)
    {
        var entity = await context
            .Guilds
            .FirstOrDefaultAsync(x => x.Id == guildId)
            .ConfigureAwait(false);

        return mapper.Map<GuildDto>(entity);
    }

    public async Task UpdateAsync(GuildDto guildDto)
    {
        var entity = await context
            .Guilds
            .FirstOrDefaultAsync(x => x.Id == guildDto.Id)
            .ConfigureAwait(false);

        if (entity == null)
        {
            return;
        }

        entity.Enabled = guildDto.Enabled;
        entity.Modifieddate = guildDto.ModifiedDate;

        context
            .Guilds
            .Update(entity);
        await context
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }
}
