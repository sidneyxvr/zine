using System.Text.RegularExpressions;

namespace Argon.Zine.Core.DomainObjects;

public class Email : ValueObject
{
    public const string RegularExpression = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

    public const int MaxLength = 254;
    public const int MinLength = 5;
    public string Address { get; private set; }

#pragma warning disable CS8618
    protected Email() { }
#pragma warning restore CS8618

    public Email(string? address)
    {
        Check.NotEmpty(address, nameof(Email));
        Check.Length(address!, MinLength, MaxLength, nameof(Email));
        Check.Matches(RegularExpression, address!, nameof(Email));

        Address = address!;
    }

    public static implicit operator Email(string? address) =>
        new(address);

    public static bool IsValid(string? email)
    {
        if (email is null)
        {
            return true;
        }

        var regexEmail = new Regex(RegularExpression);
        return regexEmail.IsMatch(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}