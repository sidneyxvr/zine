using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.CommandHandlers
{
    public class CreateCategoryHandler : RequestHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<ValidationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //if (!await _unitOfWork.DepartmentRepository.DepartmentExistsByIdAsync(request.DepartmentId))
            //{
            //    return WithError(Localizer.GetTranslation("DepartmentDoesNotExist"));
            //}

            //var category = new Category(request.Name, request.Description, request.DepartmentId);

            //await _unitOfWork.DepartmentRepository.AddAsync(category);
            //await _unitOfWork.CommitAsync();

            //return request.ValidationResult;
            throw new NotImplementedException();
        }
    }
}
