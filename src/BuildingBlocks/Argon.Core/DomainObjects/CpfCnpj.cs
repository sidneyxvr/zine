using Argon.Core.Utils;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class CpfCnpj : ValueObject
    {
        public string Number { get; set; }

        public CpfCnpj(string number)
        {
            Check.NotEmpty(number, nameof(CpfCnpj));
            if (number?.Length == CpfValidator.NumberLength)
            {
                Check.True(CpfValidator.IsValid(number), nameof(CpfCnpj));
            }
            else
            {
                Check.True(CnpjValidator.IsValid(number!), nameof(CpfCnpj));
            }

            Number = number;
        }

        public static implicit operator CpfCnpj(string number)
            => new (number);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
