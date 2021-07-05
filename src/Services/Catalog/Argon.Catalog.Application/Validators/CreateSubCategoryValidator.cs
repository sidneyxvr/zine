using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateSubCategoryValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        public CreateSubCategoryValidator(IStringLocalizer<CreateSubCategoryValidator> localizer)
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage(localizer["Required Category Id"]);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .Length(SubCategory.NameMinLength, SubCategory.NameMaxLength).WithMessage(localizer["Invalid Name Length"]);

            RuleFor(c => c.Description)
                .MaximumLength(SubCategory.DescriptionMaxLength).WithMessage(localizer["Invalid Description Length"]);
        }
    }
}
