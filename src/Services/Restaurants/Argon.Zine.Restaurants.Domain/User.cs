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

    public User(Guid id, string? firstName, string? lastName, string? email)
    {
        Check.NotEmpty(id, nameof(id));
        Check.NotEmpty(firstName, nameof(firstName));
        Check.NotEmpty(lastName, nameof(lastName));

        Id = id;
        Name = new Name(firstName!, lastName!);
        Email = email;
        IsActive = true;
        IsDelete = false;
    }
}