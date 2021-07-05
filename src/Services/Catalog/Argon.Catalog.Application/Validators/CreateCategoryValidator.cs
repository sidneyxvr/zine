using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator(IStringLocalizer<CreateCategoryValidator> localizer)
        {
            RuleFor(c => c.DepartmentId)
                .NotEmpty().WithMessage(localizer["Required Department Id"]);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .Length(Category.NameMinLength, Category.NameMaxLength).WithMessage(localizer["Invalid Name Length"]);

            RuleFor(c => c.Description)
                .MaximumLength(Category.DescriptionMaxLength).WithMessage(localizer["Invalid Description Length"]);
        }
    }
}
