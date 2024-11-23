namespace CBOS.Domain.Dtos;

public class GuildDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string OwnerId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public bool Enabled { get; set; }

    public virtual DiscordUserDto Owner { get; set; }
}