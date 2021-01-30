﻿using System.Collections.Generic;
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
            if (!IsValid(number)) throw new DomainException(Localizer.GetTranslation("InvalidPhone"));
            Number = number;
        }

        public static implicit operator Phone(string number)
        {
            return new Phone(number);
        }

        public static bool IsValid(string phone)
        {
            var regexEmail = new Regex(@"^[1-9]{2}9[1-9][0-9]{7}$");
            return phone is null || regexEmail.IsMatch(phone);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}