﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Argon.Core.DomainObjects
{
    public class Email : ValueObject
    {
        public const int AddressMaxLength = 254;
        public const int AddressMinLength = 5;
        public string Address { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            AssertionConcern.AssertArgumentNotEmpty(address, Localizer.GetTranslation("EmptyEmail"));
            AssertionConcern.AssertArgumentLength(address, AddressMinLength, AddressMaxLength,
                string.Format(Localizer.GetTranslation("EmailOutOfRange"), AddressMinLength, AddressMaxLength));
            if (!IsValid(address)) throw new DomainException(Localizer.GetTranslation("InvalidEmail"));
            Address = address;
        }

        public static implicit operator Email(string address) => 
            new (address);

        public static bool IsValid(string email)
        {
            if (email is null)
            {
                return true;
            }

            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }
    }
}
