using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.AddressCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.AddressHandlers
{
    public class DefineMainAddressHandler : BaseHandler, IRequestHandler<DefineMainAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public DefineMainAddressHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(DefineMainAddressCommand request, CancellationToken cancellationToken)
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

            customer.DefineMainAddress(request.AddressId);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
