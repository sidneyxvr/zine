using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.CommandHandlers
{
    public class CreateSubCategoryHandler : RequestHandler<CreateSubCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<ValidationResult> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            //var category = await _unitOfWork.DepartmentRepository.GetCategoryByIdAsync(request.CategoryId);

            //if(category is null)
            //{
            //    return WithError(Localizer.GetTranslation("CategoryDoesNotExist"));
            //}

            //category.
            throw new NotImplementedException();
        }
    }
}
