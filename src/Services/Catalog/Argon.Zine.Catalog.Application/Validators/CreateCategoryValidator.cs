using Argon.Zine.Catalog.Application.Commands;
using Argon.Zine.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Zine.Catalog.Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(IStringLocalizer<CreateCategoryValidator> localizer)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .Length(Category.NameMinLength, Category.NameMaxLength).WithMessage(localizer["Invalid Name Length"]);

            RuleFor(c => c.Description)
                .MaximumLength(Category.DescriptionMaxLength).WithMessage(localizer["Invalid Description Length"]);
        }
    }
}
