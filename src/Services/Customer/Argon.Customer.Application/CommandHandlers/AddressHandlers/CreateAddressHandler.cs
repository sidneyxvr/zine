using Argon.Core.DomainObjects;
using Argon.Core.Internationalization;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.AddressHandlers
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateAddressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = await _customerRepository.GetByIdAsync(request.AggregateId);

            if(customer is null)
            {
                throw new NotFoundException();
            }

            customer.AddAddress(request.Street, request.Number, request.District, 
                request.City, request.State, request.Country, request.PostalCode, 
                request.Complement, request.Latitude, request.Longitude);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
