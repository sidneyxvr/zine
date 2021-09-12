using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages;
using Argon.Customers.Application.Commands;
using Argon.Customers.Domain;
using FluentValidation.Results;

namespace Argon.Customers.Application.Handlers
{
    public class DefineMainAddressHandler : RequestHandler<DefineMainAddressCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;

        public DefineMainAddressHandler(
            IAppUser appUser, 
            IUnitOfWork unitOfWork)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(
            DefineMainAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetByIdAsync(_appUser.Id, Include.All, cancellationToken);

            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }

            customer.DefineMainAddress(request.AddressId);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
