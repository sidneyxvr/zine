using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Domain;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application
{
    public class UpdateCustomerHandler : RequestHandler<UpdateCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new NotFoundException();
            }

            customer.Update(request.FirstName, request.LastName, request.BirthDate, request.Gender);

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
