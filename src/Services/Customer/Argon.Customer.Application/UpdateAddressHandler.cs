using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
{
    public class UpdateAddressHandler : RequestHandler<UpdateAddressCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressHandler(IAppUser appUser, IUnitOfWork unitOfWork)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.CustomerRepository.GetAddressAsync(_appUser.Id, request.AddressId);

            if(address is null)
            {
                throw new NotFoundException(Localizer.GetTranslation("AddressNotFound"));
            }

            address.Update(request.Street, request.Number, request.District, request.City,
                request.State, request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            await _unitOfWork.CustomerRepository.UpdateAsync(address);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
