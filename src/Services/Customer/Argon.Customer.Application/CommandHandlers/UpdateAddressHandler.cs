using Argon.Core.Messages;
using Argon.Customers.Application.Commands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers
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

            var customerId = Guid.NewGuid();

            var address = await _unitOfWork.CustomerRepository.GetAddressAsync(customerId, request.AddressId);

            address.Update(request.Street, request.Number, request.District, request.City,
                request.State, request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.CustomerRepository.UpdateAsync(address);
            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
