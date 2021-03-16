using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.CustomerHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = new Customer(request.CustomerId, request.FirstName, request.Surname,
                request.Email, request.Cpf, request.BirthDate, request.Gender, request.Phone);

            await _customerRepository.AddAsync(customer);
            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
