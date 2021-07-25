using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.Handlers
{
    public class DeleteAddressHandler : RequestHandler<DeleteAddressCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressHandler(
            IAppUser appUser,
            IUnitOfWork unitOfWork)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(
            DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetByIdAsync(_appUser.Id, Include.All, cancellationToken);

            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }

            customer.DeleteAddress(request.AddressId);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
