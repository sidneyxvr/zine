using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
{
    public class DefineMainAddressHandler : RequestHandler<DefineMainAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DefineMainAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(DefineMainAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId, Include.MainAddress, Include.Addresses);

            if (customer is null)
            {
                throw new NotFoundException(Localizer.GetTranslation("CustomerNotFound"));
            }

            customer.DefineMainAddress(request.AddressId);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
