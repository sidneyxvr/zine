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
    public class CreateAddressHandler : BaseHandler, IRequestHandler<CreateAddressCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customerId = Guid.NewGuid();

            var address = new Address(customerId, request.Street, request.Number, 
                request.District, request.City, request.State, request.PostalCode,
                request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.CustomerRepository.AddAsync(address);
            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
