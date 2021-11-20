namespace Argon.Zine.Customers.Domain;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();

    public ICustomerRepository CustomerRepository { get; }
}