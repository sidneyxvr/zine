using Argon.Core.Messages.IntegrationCommands;
using Argon.Customers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.CustomerHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = new Customer(request.UserId, request.FirstName, request.LastName,
                request.Email, request.Cpf, request.BirthDate, request.Gender, request.Phone);

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
