using Argon.Zine.Core.DomainObjects;
using Argon.Zine.Core.Messages;
using Argon.Customers.Application.Commands;
using Argon.Customers.Domain;
using FluentValidation.Results;

namespace Argon.Customers.Application.Handlers
{
    public class UpdateCustomerHandler : RequestHandler<UpdateCustomerCommand>
    {
        private readonly IAppUser _appUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerHandler(
            IAppUser appUser,
            IUnitOfWork unitOfWork)
        {
            _appUser = appUser;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(
            UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetByIdAsync(_appUser.Id, Include.None, cancellationToken);

            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
            }

            customer.Update(request.FirstName, request.LastName, request.BirthDate, request.Gender);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
