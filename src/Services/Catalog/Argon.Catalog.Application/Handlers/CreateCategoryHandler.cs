using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class CreateCategoryHandler : RequestHandler<CreateCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<CreateCategoryHandler> _localizer;

        public CreateCategoryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<CreateCategoryHandler> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(
            CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var departmentExists = await _unitOfWork.DepartmentRepository
                .ExistsByIdAsync(request.DepartmentId, cancellationToken);

            if (!departmentExists)
            {
                return WithError("department", _localizer["Department Not Found"]);
            }

            var category = new Category(request.Name, request.Description, request.DepartmentId);

            await _unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
