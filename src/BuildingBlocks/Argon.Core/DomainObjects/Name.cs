using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class Name : ValueObject
    {
        public const int FirstNameMaxLength = 50;
        public const int SurnameMaxLength = 50;
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        protected Name() { }

        public string FullName => $"{FirstName} {Surname}";

        public Name(string firstName, string surname)
        {
            AssertionConcern.AssertArgumentNotEmpty(firstName, Localizer.GetTranslation("EmptyFirstName"));
            AssertionConcern.AssertArgumentNotEmpty(surname, Localizer.GetTranslation("EmptySurname"));
            AssertionConcern.AssertArgumentLength(firstName, FirstNameMaxLength, Localizer.GetTranslation("FirstNameMaxLength"));
            AssertionConcern.AssertArgumentLength(surname, SurnameMaxLength, Localizer.GetTranslation("SurnameMaxLength"));

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
