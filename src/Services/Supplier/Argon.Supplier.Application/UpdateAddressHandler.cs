using Argon.Core.Messages;
using Argon.Suppliers.Domain;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Application
{
    public class UpdateAddressHandler : RequestHandler<UpdateAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.SupplierRepository.GetAddressAsync(Guid.NewGuid(), request.AddressId);

            address.Update(request.Street, request.Number, request.District, request.City, request.State, 
                request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.SupplierRepository.UpdateAsync(address);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
