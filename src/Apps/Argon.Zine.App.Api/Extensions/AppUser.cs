using Argon.Zine.Commom.DomainObjects;

namespace Argon.Zine.App.Api.Extensions;

public class AppUser : IAppUser
{
    public AppUser(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string FullName => $"{FirstName} {LastName}";
}