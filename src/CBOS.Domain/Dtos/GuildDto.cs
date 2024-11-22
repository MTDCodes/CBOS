namespace CBOS.Domain.Dtos;

public partial class GuildDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string OwnerId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual DiscordUserDto Owner { get; set; }
}