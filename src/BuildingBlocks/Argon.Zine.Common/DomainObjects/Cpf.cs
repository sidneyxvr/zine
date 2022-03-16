using Argon.Zine.Commom.Utils;

namespace Argon.Zine.Commom.DomainObjects;

public class Cpf : ValueObject
{
    public const int NumberLength = 11;
    public string Number { get; private set; }

#pragma warning disable CS8618 
    protected Cpf() { }
#pragma warning restore CS8618 
    public Cpf(string? number)
    {
        Check.NotEmpty(number, nameof(Cpf));
        Check.True(IsValid(number!), nameof(Cpf));

        number = number!.OnlyNumbers();

        Number = number!;
    }

    public static implicit operator Cpf(string? number)
        => new(number);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    public static bool IsValid(string cpf)
    {
        if (cpf is null)
        {
            throw new ArgumentNullException(nameof(cpf));
        }

        cpf = cpf.OnlyNumbers();

        if (cpf.Length > NumberLength)
        {
            return false;
        }

        while (cpf.Length < NumberLength)
        {
            cpf = '0' + cpf;
        }

        var equal = true;
        for (var i = 1; i < NumberLength && equal; i++)
        {
            if (cpf[i] != cpf[0])
            {
                equal = false;
            }
        }

        if (equal || cpf == "12345678909")
        {
            return false;
        }

        var numbers = new int[NumberLength];

        for (var i = 0; i < NumberLength; i++)
        {
            numbers[i] = int.Parse(cpf[i].ToString());
        }

        var sum = 0;
        for (var i = 0; i < 9; i++)
        {
            sum += (10 - i) * numbers[i];
        }

        var result = sum % NumberLength;

        if (result == 1 || result == 0)
        {
            if (numbers[9] != 0)
            {
                return false;
            }
        }
        else if (numbers[9] != NumberLength - result)
        {
            return false;
        }

        sum = 0;
        for (var i = 0; i < 10; i++)
        {
            sum += (NumberLength - i) * numbers[i];
        }

        result = sum % NumberLength;

        if (result == 1 || result == 0)
        {
            if (numbers[10] != 0)
            {
                return false;
            }
        }
        else if (numbers[10] != NumberLength - result)
        {
            return false;
        }

        return true;
    }
}