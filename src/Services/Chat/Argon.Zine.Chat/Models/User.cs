using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Chat.Models;

public class User : Entity
{
    public string Name { get; private set; }
    public string? ImageUrl { get; private set; }

    public User(Guid id, string name, string? imageUrl = null)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
    }
}