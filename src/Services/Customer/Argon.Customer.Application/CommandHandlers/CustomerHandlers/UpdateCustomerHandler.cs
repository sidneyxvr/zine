using Argon.Core.DomainObjects;
using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.CustomerHandlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                throw new NotFoundException();
            }

            customer.Update(request.FirstName, request.LastName, request.BirthDate, request.Gender);

            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
