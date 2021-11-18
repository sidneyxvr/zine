namespace Argon.Zine.Core.DomainObjects;

public interface IAppUser
{
    Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string FullName { get; }
}