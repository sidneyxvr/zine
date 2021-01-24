using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Argon.Core.DomainObjects
{
    public class FullName : ValueObject
    {
        public const int NameMaxLength = 100;

        public string Name { get; private set; }

        protected FullName() { }

        public FullName(string name)
        {
            if (!IsValid(name)) throw new DomainException(Localizer.GetTranslation("InvalidFullName"));
            Name = name;
        }

        public string FirstName() => Name.Split(' ')?.FirstOrDefault();

        public string ShortName() => string.Join(' ', Name.Split(' ')?.Take(2));


        public static implicit operator FullName(string name)
        {
            return new FullName(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public static bool IsValid(string name)
        {
            return name is not null && name.Length <= NameMaxLength &&
                Regex.IsMatch(name, @"^(?![ ])(?!.*[ ]{2})((?:e|da|do|das|dos|de|d'|D'|la|las|el|los)\s*?|(?:[A-Z][^\s]*\s*?)(?!.*[ ]$))+$");
        }
    }
}
