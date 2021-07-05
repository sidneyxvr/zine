using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using Argon.Core.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace Argon.Catalog.Application.Handlers
{
    public class CreateSubCategoryHandler : RequestHandler<CreateSubCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<CreateSubCategoryHandler> _localizer;

        public CreateSubCategoryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<CreateSubCategoryHandler> localizer)
        {
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ValidationResult> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _unitOfWork.CategoryRepository.ExistsAsync(request.CategoryId);

            if (!categoryExists)
            {
                return WithError("category", _localizer["Category Not Found"]);
            }

            var subCategory = new SubCategory(request.Name, request.Description, request.CategoryId);

            await _unitOfWork.SubCategoryRepository.AddAsync(subCategory);
            await _unitOfWork.CommitAsync();

            return ValidationResult;
        }
    }
}
