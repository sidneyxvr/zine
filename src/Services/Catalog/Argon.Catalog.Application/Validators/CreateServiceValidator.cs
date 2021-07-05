using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator(IStringLocalizer localizer)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .MaximumLength(Service.NameMaxLength).WithMessage(localizer["Name's Max Length"]);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .MaximumLength(Service.NameMaxLength).WithMessage(localizer["Name's Max Length"]);

            RuleFor(p => p.Images)
                .NotEmpty().WithMessage(localizer["Required Image"]);

            RuleFor(p => p.SubCategoryId)
                .NotEmpty().WithMessage(localizer["Required SubCategory"]);
        }
    }
}
