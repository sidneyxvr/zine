using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
{
    public class DeleteAddressHandler : RequestHandler<DeleteAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, Include.Addresses, Include.MainAddress);

            if (customer is null)
            {
                throw new NotFoundException(Localizer.GetTranslation("CustomerNotFound"));
            }

            customer.DeleteAddress(request.AddressId);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
