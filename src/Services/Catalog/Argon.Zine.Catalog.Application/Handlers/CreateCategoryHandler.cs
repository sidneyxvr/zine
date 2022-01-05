using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Domain;
using Argon.Zine.Commom.Messages;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Catalog.Application.Handlers;

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
        var departmentExists = await _unitOfWork.CategoryRepository
            .ExistsByNameAsync(request.Name!, cancellationToken);

        if (!departmentExists)
        {
            return WithError("department", _localizer["Department Not Found"]);
        }

        var category = new Category(request.Name, request.Description);

        await _unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync();

        return ValidationResult;
    }
}