﻿using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Core.Messages.IntegrationEvents;
using Argon.Suppliers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Application.CommandHandlers
{
    public class CreateSupplierHandler : BaseHandler, IRequestHandler<CreateSupplierCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSupplierHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var user = new User(request.UserId, request.FirstName, request.LastName, request.Email, request.Gender);

            var address = new Address(request.Street, request.Number, request.District, request.City, 
                request.State, request.PostalCode, request.Complement, request.Latitude, request.Longitude);

            var supplier = new Supplier(request.CorparateName, request.TradeName, request.CpfCnpj, user, address);

            var supplierCreatedEvent = new SupplierCreatedEvent
            {
                Name = supplier.TradeName,
                Latitude = address.Location.Latitude,
                Longitude = address.Location.Longitude,
                Address = address.ToString()
            };

            supplier.AddDomainEvent(supplierCreatedEvent);

            await _unitOfWork.SupplierRepository.AddAsync(supplier);
            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
