using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Domain.Events;
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

            var customer = new Customer(
                request.AggregateId, request.FullName, request.Email, request.Cpf, request.BirthDate, request.Gender, request.Phone);

            customer.AddDomainEvent(new CreatedCustomerEvent(
                request.AggregateId, request.FullName, request.Email, request.Gender));

            await _customerRepository.AddAsync(customer);
            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
