using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator(IStringLocalizer<CreateDepartmentValidator> localizer)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .Length(Department.NameMinLength, Department.NameMaxLength).WithMessage(localizer["Invalid Name Length"]);

            RuleFor(c => c.Description)
                .MaximumLength(Department.DescriptionMaxLength).WithMessage(localizer["Invalid Description Length"]);
        }
    }
}
