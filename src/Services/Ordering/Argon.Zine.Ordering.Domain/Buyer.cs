using Argon.Zine.Core.DomainObjects;

namespace Argon.Zine.Ordering.Domain;

public class Buyer : Entity, IAggregateRoot
{
    public Name Name { get; private set; }
    private List<PaymentMethod> _paymentMethods = new();
    public IReadOnlyCollection<PaymentMethod> PaymentMethods
        => _paymentMethods.AsReadOnly();

#pragma warning disable CS8618
    private Buyer() { }
#pragma warning restore CS8618

    public Buyer(Guid id, string firstName, string lastName)
        => (Id, Name) = (id, new(firstName, lastName));

    public void AddPaymentMethod(PaymentMethod paymentMethod)
        => _paymentMethods.Add(paymentMethod);

    public void RemoverPaymentMethod(Guid paymentMethodId)
        => _paymentMethods?.RemoveAll(p => p.Id == paymentMethodId);
}