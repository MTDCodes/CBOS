using AutoMapper;
using CBOS.Data.Entities;
using CBOS.Domain.Dtos;

namespace CBOS.Domain.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GuildDto, Guild>().ReverseMap();
        CreateMap<DiscordUserDto, Discorduser>().ReverseMap();
    }
}
