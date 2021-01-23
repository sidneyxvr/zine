using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Argon.Core.DomainObjects
{
    public class Phone : ValueObject
    {
        public const int NumberMaxLength = 11;
        public const int NumberMinLength = 10;
        public string Number { get; private set; }

        protected Phone() { }

        public Phone(string number)
        {
            if (!Validate(number)) throw new DomainException(Localizer.GetTranslation("InvalidPhone"));
            Number = number;
        }

        public static implicit operator Phone(string number)
        {
            return new Phone(number);
        }

        public static bool Validate(string email)
        {
            var regexEmail = new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$");
            return regexEmail.IsMatch(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
