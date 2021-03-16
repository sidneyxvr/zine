using Argon.Core.DomainObjects;
using Argon.Core.Internationalization;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.AddressHandlers
{
    public class UpdateAddressHandler : BaseHandler, IRequestHandler<UpdateAddressCommand, ValidationResult>
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

            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new NotFoundException(Localizer.GetTranslation("CustomerNotFound"));
            }

            customer.UpdateAddress(request.AddressId, request.Street, request.Number, request.District, request.City, request.State, 
                request.Country, request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
