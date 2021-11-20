using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Catalog.Domain;

public class Category : Entity, IAggregateRoot
{
    public const int NameMinLength = 3;
    public const int NameMaxLength = 25;
    public const int DescriptionMaxLength = 255;

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Category() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Category(string? name, string? description)
    {
        Check.NotEmpty(name, nameof(name));
        Check.Length(name!, NameMinLength, NameMaxLength, nameof(description));
        Check.MaxLength(description, DescriptionMaxLength, nameof(description));

        Name = name!;
        Description = description;
        IsActive = true;
    }
}