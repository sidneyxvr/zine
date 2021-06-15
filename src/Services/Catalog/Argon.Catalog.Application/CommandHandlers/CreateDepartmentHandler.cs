using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.CommandHandlers
{
    public class CreateDepartmentHandler : RequestHandler<CreateDepartmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<ValidationResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            //if (await _unitOfWork.DepartmentRepository.DepartmentExistsByNameAsync(request.Name))
            //{
            //    return WithError(Localizer.GetTranslation("DepartmentExists"));
            //}

            //var department = new Department(request.Name, request.Description);

            //await _unitOfWork.DepartmentRepository.AddAsync(department);
            //await _unitOfWork.CommitAsync();

            //return ValidationResult;
            throw new NotImplementedException();
        }
    }
}
