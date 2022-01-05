namespace Argon.Zine.Commom.DomainObjects;

public class Name : ValueObject
{
    public const int MaxLengthFirstName = 50;
    public const int MaxLengthLastName = 50;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
#pragma warning disable CS8618
    protected Name() { }
#pragma warning restore CS8618

    public string FullName => $"{FirstName} {LastName}";

    public Name(string firstName, string lastName)
    {
        Check.NotEmpty(firstName, nameof(firstName));
        Check.NotEmpty(lastName, nameof(lastName));
        Check.MaxLength(firstName, MaxLengthFirstName, nameof(firstName));
        Check.MaxLength(lastName, MaxLengthLastName, nameof(lastName));

        FirstName = firstName!;
        LastName = lastName!;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}