using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class CreateDepartmentHandler : RequestHandler<CreateDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department(request.Name, request.Description);

            await _unitOfWork.DepartmentRepository.AddAsync(department);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
