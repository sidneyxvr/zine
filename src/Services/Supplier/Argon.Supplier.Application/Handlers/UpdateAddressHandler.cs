using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Suppliers.Application.Commands;
using Argon.Suppliers.Domain;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Application.Handlers
{
    public class UpdateAddressHandler : RequestHandler<UpdateAddressCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public UpdateAddressHandler(
            IAppUser appUser, 
            IUnitOfWork unitOfWork,
            IStringLocalizer localizer)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public override async Task<ValidationResult> Handle(
            UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.SupplierRepository
                .GetByIdAsync(_appUser.Id, Include.Address, cancellationToken);

            if(supplier is null)
            {
                throw new ArgumentNullException(nameof(supplier), "supplier cannot be null");
            }

            var address = supplier.Address;

            if(address is null)
            {
                throw new DomainException(_localizer["Address not found"]);
            }

            address.Update(request.Street, request.Number, 
                request.District, request.City, request.State, request.PostalCode, 
                request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.SupplierRepository.UpdateAsync(supplier, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
