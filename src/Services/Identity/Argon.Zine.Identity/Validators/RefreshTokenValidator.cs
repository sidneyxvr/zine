using Argon.Zine.Identity.Requests;
using FluentValidation;

namespace Argon.Zine.Identity.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(r => r.AccessToken).NotNull().NotEmpty();

            RuleFor(r => r.RefreshToken).NotNull().NotEmpty();
        }
    }
}
