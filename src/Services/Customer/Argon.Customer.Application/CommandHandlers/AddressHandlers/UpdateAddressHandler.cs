using Argon.Core.DomainObjects;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.AddressHandlers
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateAddressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
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

            customer.UpdateAddress(request.AddressId, request.Street, request.Number,
                request.District, request.City, request.State, request.Country,
                request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
