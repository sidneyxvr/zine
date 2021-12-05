using Argon.Zine.Core.DomainObjects;

namespace Argon.Restaurants.Domain;

public class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDelete { get; private set; }

    public Guid RestaurantId { get; private set; }
    public Restaurant? Restaurant { get; private set; }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    public User(Guid id, Name name, Email email)
    {
        Check.NotEmpty(id, nameof(id));
        Check.NotNull(name, nameof(name));
        Check.NotNull(email, nameof(email));

        Id = id;
        Name = name;
        Email = email;
        IsActive = true;
        IsDelete = false;
    }
}