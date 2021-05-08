using Argon.Core.Messages;
using Argon.Suppliers.Application.Commands;
using Argon.Suppliers.Domain;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Application.CommandHandlers
{
    public class UpdateAddressHandler : BaseHandler, IRequestHandler<UpdateAddressCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var address = await _unitOfWork.SupplierRepository.GetAddressAsync(Guid.NewGuid(), request.AddressId);

            address.Update(request.Street, request.Number, request.District, request.City, request.State, 
                request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.SupplierRepository.UpdateAsync(address);
            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
