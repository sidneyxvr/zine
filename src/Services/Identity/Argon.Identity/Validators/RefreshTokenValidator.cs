using Argon.Core.Messages.IntegrationCommands.Validators;
using Argon.Identity.Requests;
using FluentValidation;

namespace Argon.Identity.Validators
{
    public class RefreshTokenValidator : BaseValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(r => r.AccessToken).NotEmpty();

            RuleFor(r => r.RefreshToken).NotEmpty();
        }
    }
}
