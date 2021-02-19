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
            AssertionConcern.AssertArgumentNotEmpty(firstName, Localizer.GetTranslation("EmptyFirstName"));
            AssertionConcern.AssertArgumentNotEmpty(surname, Localizer.GetTranslation("EmptySurname"));
            AssertionConcern.AssertArgumentLength(firstName, MaxLengthFirstName, 
                string.Format(Localizer.GetTranslation("MaxLengthFirstName"), MaxLengthFirstName));
            AssertionConcern.AssertArgumentLength(surname, MaxLengthSurname, 
                string.Format(Localizer.GetTranslation("MaxLengthSurname"), MaxLengthSurname));

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
