using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Commands.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator(IStringLocalizer localizer)
        {
            //RuleFor(p => p.Name)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyName"))
            //    .MaximumLength(Service.NameMaxLength).WithMessage(Localizer.GetTranslation("NameMaxLength"));

            //RuleFor(p => p.Name)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyName"))
            //    .MaximumLength(Service.NameMaxLength).WithMessage(Localizer.GetTranslation("NameMaxLength"));

            //RuleFor(p => p.Images)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyImage"));

            //RuleFor(p => p.CategoryId)
            //    .NotEmpty().WithMessage(Localizer.GetTranslation("EmptyCategory"));
        }
    }
}
