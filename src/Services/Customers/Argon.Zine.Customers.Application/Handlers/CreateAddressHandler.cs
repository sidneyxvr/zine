using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages;
using Argon.Zine.Customers.Application.Commands;
using Argon.Zine.Customers.Domain;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Zine.Customers.Application.Handlers
{
    public class CreateAddressHandler : RequestHandler<CreateAddressCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressHandler(IUnitOfWork unitOfWork, IAppUser appUser)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetByIdAsync(_appUser.Id, Include.None, cancellationToken);

            if(customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }

            var address = new Address(_appUser.Id, request.Street, request.Number, 
                request.District, request.City, request.State, request.PostalCode,
                request.Complement, request.Latitude, request.Longitude);

            customer.AddAddress(address);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.CustomerRepository.AddAsync(address, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
