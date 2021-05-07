using Argon.Core.Messages;
using Argon.Core.Messages.IntegrationCommands;
using Argon.Suppliers.Domain;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Suppliers.Application.CommandHandlers.SupplierHandlers
{
    public class CreateSupplierHandler : BaseHandler, IRequestHandler<CreateSupplierCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISupplierRepository _supplierRepository;

        public CreateSupplierHandler(IUnitOfWork unitOfWork, 
            ISupplierRepository supplierRepository)
        {
            _unitOfWork = unitOfWork;
            _supplierRepository = supplierRepository;
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

            await _supplierRepository.AddAsync(supplier);

            await _unitOfWork.CommitAsync();

            return request.ValidationResult;
        }
    }
}
