using Argon.Catalog.Application.Commands;
using Argon.Catalog.Domain;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Argon.Catalog.Application.Validators
{
    public class CreateTagValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagValidator(IStringLocalizer<CreateTagValidator> localizer)
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage(localizer["Required Name"])
                .Length(Tag.MinLength, Tag.MaxLength).WithMessage(localizer["Invalid Name Length"]);
        }
    }
}
