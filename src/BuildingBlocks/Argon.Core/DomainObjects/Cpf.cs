using Argon.Core.Utils;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class Cpf : ValueObject
    {
        public string Number { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Cpf() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Cpf(string? number)
        {
            Check.NotEmpty(number, nameof(Cpf));
            Check.True(CpfValidator.IsValid(number!), nameof(Cpf));

            number = number!.OnlyNumbers();
            
            Number = number!;
        }

        public static implicit operator Cpf(string? number) 
            => new (number);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
