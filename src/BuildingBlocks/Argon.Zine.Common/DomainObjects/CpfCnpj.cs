namespace Argon.Zine.Commom.DomainObjects;

public class CpfCnpj : ValueObject
{
    public const int NumberLength = 14;
    public string Number { get; private set; }

    public CpfCnpj(string? number)
    {
        Check.NotEmpty(number, nameof(CpfCnpj));
        if (number?.Length == Cpf.NumberLength)
        {
            Check.True(Cpf.IsValid(number!), nameof(CpfCnpj));
        }
        else
        {
            Check.True(Cnpj.IsValid(number!), nameof(CpfCnpj));
        }

        Number = number!;
    }

    public static implicit operator CpfCnpj(string number)
        => new(number);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}