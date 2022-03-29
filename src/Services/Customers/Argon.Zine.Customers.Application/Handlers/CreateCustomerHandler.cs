using Argon.Zine.Commom;
using Argon.Zine.Commom.DomainObjects;
using Argon.Zine.Commom.Messages;
using Argon.Zine.Commom.Messages.IntegrationCommands;
using Argon.Zine.Customers.Domain;

namespace Argon.Zine.Customers.Application.Handlers;

public class CreateCustomerHandler : RequestHandler<CreateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public override async Task<AppResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var name = new Name(request.FirstName!, request.Surname!);
        var customer = new Customer(request.UserId, name,
            request.Email, request.Cpf, new BirthDate(request.BirthDate), request.Phone);

        await _unitOfWork.CustomerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.CommitAsync();

        return customer;
    }
}