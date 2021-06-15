using Argon.Catalog.Domain;
using Argon.Core.Messages.IntegrationCommands.Validators;
using FluentValidation;

namespace Argon.Catalog.Application.Commands.Validators
{
    public class CreateCategoryValidator : BaseValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.DepartmentId)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyDepartment"));

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyName"))
                .Length(Category.NameMinLength, Category.NameMaxLength).WithMessage(Localizer.GetTranslation("NameOutOfRange", Category.NameMinLength, Category.NameMaxLength));

            RuleFor(c => c.Description)
                .MaximumLength(Category.DescriptionMaxLength).WithMessage(Localizer.GetTranslation("DescriptionOutOfRange", Category.DescriptionMaxLength));
        }
    }
}
