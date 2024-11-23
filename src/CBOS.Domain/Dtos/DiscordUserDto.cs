namespace CBOS.Domain.Dtos;

public class DiscordUserDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<GuildDto> Guilds { get; set; } = new List<GuildDto>();
}