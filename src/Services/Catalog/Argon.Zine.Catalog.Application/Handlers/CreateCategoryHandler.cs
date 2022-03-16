using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Domain;
using Argon.Zine.Commom;
using Argon.Zine.Commom.Messages;
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

    public override async Task<AppResult> Handle(
        CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _unitOfWork.CategoryRepository
            .ExistsByNameAsync(request.Name!, cancellationToken);

        if (categoryExists)
        {
            return WithError("category", _localizer["Category Not Found"]);
        }

        var category = new Category(request.Name, request.Description);

        await _unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.CommitAsync();

        return category;
    }
}