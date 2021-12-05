using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages;
using Argon.Zine.Core.Messages.IntegrationCommands;
using Argon.Zine.Customers.Domain;
using FluentValidation.Results;

namespace Argon.Zine.Customers.Application.Handlers;

public class CreateCustomerHandler : RequestHandler<CreateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public override async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var name = new Name(request.FirstName!, request.LastName!);
        var customer = new Customer(request.UserId, name,
            request.Email, request.Cpf, request.BirthDate, request.Phone);

        await _unitOfWork.CustomerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.CommitAsync();

        return ValidationResult;
    }
}