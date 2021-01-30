using Argon.Core.DomainObjects;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.AddressHandlers
{
    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteAddressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = await _customerRepository.GetByIdAsync(request.AggregateId);

            if (customer is null)
            {
                throw new NotFoundException();
            }

            customer.DeleteAddress(request.AddressId);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
