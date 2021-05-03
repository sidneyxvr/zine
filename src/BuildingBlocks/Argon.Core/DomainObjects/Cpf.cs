using Argon.Core.Utils;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class Cpf : ValueObject
    {
        public string Number { get; private set; }

        protected Cpf() { }

        public Cpf(string number)
        {
            number = number?.OnlyNumbers();

            Check.NotEmpty(number, nameof(Cpf));
            if (!CpfValidator.IsValid(number)) throw new DomainException(nameof(Cpf));
            Number = number;
        }

        public static implicit operator Cpf(string number)
        {
            return new Cpf(number);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
