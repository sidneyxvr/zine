using Argon.Zine.Commom.Utils;

namespace Argon.Zine.Commom.DomainObjects;

public class Cnpj : ValueObject
{
    public const int NumberLength = 14;
    public string Number { get; private set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Number;
    }

#pragma warning disable CS8618
    protected Cnpj() { }
#pragma warning restore CS8618 
    public Cnpj(string? number)
    {
        Check.NotEmpty(number, nameof(Cpf));
        Check.True(IsValid(number!), nameof(Cpf));

        number = number!.OnlyNumbers();

        Number = number!;
    }

    public static implicit operator Cnpj(string? number)
        => new(number);

    public static bool IsValid(string cnpj)
    {
        if (cnpj is null)
        {
            throw new ArgumentNullException(nameof(cnpj));
        }

        return true;
    }
}