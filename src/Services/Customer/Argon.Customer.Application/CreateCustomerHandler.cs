using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
{
    public class CreateCustomerHandler : RequestHandler<CreateCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.UserId, request.FirstName, request.LastName,
                request.Email, request.Cpf, request.BirthDate, request.Gender, request.Phone);

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
