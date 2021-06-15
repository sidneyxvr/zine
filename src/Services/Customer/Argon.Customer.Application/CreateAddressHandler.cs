using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
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
            var address = new Address(_appUser.Id, request.Street, request.Number, 
                request.District, request.City, request.State, request.PostalCode,
                request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.CustomerRepository.AddAsync(address);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
