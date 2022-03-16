namespace Argon.Zine.Commom.DomainObjects;

public class BirthDate : ValueObject
{
    public const int MinAge = 18;
    public const int MaxAge = 100;

    private DateOnly _date;

    public DateOnly Date => _date;

    protected BirthDate() { }

    public BirthDate(int year, int month, int day)
    {
        var date = new DateOnly(year, month, day);

        ValidateBirthDate(date.ToDateTime(TimeOnly.MinValue));

        _date = date;
    }

    public BirthDate(DateTime date)
        : this(date.Year, date.Month, date.Day) { }

    public BirthDate(DateOnly date)
        : this(date.Year, date.Month, date.Day) { }


    public static implicit operator BirthDate(DateTime date) 
        => new(date);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _date;
    }

    public int Birthday => _date.Day;

    public override string ToString()
    {
        return _date.ToString("dd/MM/yyyy");
    }

    private static void ValidateBirthDate(DateTime birthDate)
    {
        Check.Min(birthDate, DateTime.UtcNow.AddYears(-MinAge), nameof(BirthDate));
        Check.Max(birthDate, DateTime.UtcNow.AddYears(-MaxAge), nameof(BirthDate));
    }
}