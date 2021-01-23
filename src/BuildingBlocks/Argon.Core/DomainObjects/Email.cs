using Argon.Core.Internationalization;
using System.Text.RegularExpressions;

namespace Argon.Core.DomainObjects
{
    public class Email
    {
        public const int EnderecoMaxLength = 254;
        public const int EnderecoMinLength = 5;
        public string Address { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            if (!Validate(address)) throw new DomainException(Localizer.GetValue("InvalidEmail"));
            Address = address;
        }

        public static implicit operator Email(string address)
        {
            return new Email(address);
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
