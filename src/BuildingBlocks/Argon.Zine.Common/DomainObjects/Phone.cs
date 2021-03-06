using System.Text.RegularExpressions;

namespace Argon.Zine.Commom.DomainObjects;

public class Phone : ValueObject
{
    public const string RegularExpression = @"^[1-9]{2}9[1-9][0-9]{7}$";

    public const int NumberMaxLength = 11;
    public const int NumberMinLength = 10;
    public string? Number { get; private set; }

    protected Phone() { }

    public Phone(string? number)
    {
        if (number == null)
        {
            return;
        }

        Check.Matches(RegularExpression, number, nameof(Phone));
        Number = number;
    }

    public static implicit operator Phone(string? number)
        => new(number);

    public static bool IsValid(string? phone)
    {
        if (phone is null)
        {
            return true;
        }

        return Regex.IsMatch(phone, RegularExpression);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Number;
    }
}