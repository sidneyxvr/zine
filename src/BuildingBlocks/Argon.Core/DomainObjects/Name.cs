using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class Name : ValueObject
    {
        public const int MaxLengthFirstName = 50;
        public const int MaxLengthSurname = 50;
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        protected Name() { }

        public string FullName => $"{FirstName} {Surname}";

        public Name(string firstName, string surname)
        {
            Check.NotEmpty(firstName, nameof(firstName));
            Check.NotEmpty(surname, nameof(surname));
            Check.MaxLength(firstName, MaxLengthFirstName, nameof(firstName));
            Check.MaxLength(surname, MaxLengthSurname, nameof(surname));

            FirstName = firstName;
            Surname = surname;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return Surname;
        }
    }
}
