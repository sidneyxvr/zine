using FluentValidation.Results;

namespace Argon.Identity.Requests
{
    public abstract class BaseRequest
    {
        public abstract ValidationResult Validate();
    }
}
