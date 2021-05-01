using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class RefreshTokenValidator : BaseValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(r => r.AccessToken).NotNull().NotEmpty();

            RuleFor(r => r.RefreshToken).NotNull().NotEmpty();
        }
    }
}
