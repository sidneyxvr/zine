using Argon.Core.DomainObjects;
using Argon.Customers.Application.Commands.CustomerCommands;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Customers.Application.CommandHandlers.CustomerHandlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ValidationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = await _customerRepository.GetByIdAsync(request.AggregateId);

            if (customer is null)
            {
                throw new NotFoundException();
            }

            customer.Update(request.FirstName, request.Surname, request.BirthDate, request.Gender);

            await _customerRepository.UnitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
