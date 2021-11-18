using System.Text.RegularExpressions;

namespace Argon.Zine.Core.DomainObjects;

public static class Check
{
    public static void Equals(object object1, object object2, string message)
    {
        if (!object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void False(bool boolValue, string message)
    {
        if (boolValue)
        {
            throw new DomainException(message);
        }
    }

    public static void MaxLength(string? stringValue, int maximum, string message)
    {
        if (stringValue?.Length > maximum)
        {
            throw new DomainException(message);
        }
    }

    //public static void MinLength(string stringValue, int minimum, string message)
    //{
    //    int length = stringValue.Length;
    //    if (length > minimum)
    //    {
    //        throw new DomainException(message);
    //    }
    //}

    public static void Length(string stringValue, int minimum, int maximum, string message)
    {
        int length = stringValue.Trim().Length;
        if (length < minimum || length > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Length(string stringValue, int exactLength, string message)
    {
        int length = stringValue.Trim().Length;
        if (length != exactLength)
        {
            throw new DomainException(message);
        }
    }

    public static void Matches(string pattern, string stringValue, string message)
    {
        if (stringValue is not null && !Regex.IsMatch(stringValue, pattern))
        {
            throw new DomainException(message);
        }
    }

    public static void NotEmpty(string? stringValue, string message)
    {
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new DomainException(message);
        }
    }

    public static void NotEmpty(Guid value, string message)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(message);
        }
    }

    public static void NotEquals(object object1, object object2, string message)
    {
        if (object1.Equals(object2))
        {
            throw new DomainException(message);
        }
    }

    public static void NotNull(object? object1, string message)
    {
        if (object1 is null)
        {
            throw new DomainException(message);
        }
    }

    public static void Min(DateTime value, DateTime minimum, string message)
    {
        if (value > minimum)
        {
            throw new DomainException(message);
        }
    }

    public static void Max(DateTime value, DateTime maximum, string message)
    {
        if (value < maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(decimal value, decimal minimum, decimal maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(double value, double minimum, double maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(float value, float minimum, float maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(string value, int minimum, int maximum, string message)
    {
        if (value is not null && (value.Length < minimum || value.Length > maximum))
        {
            throw new DomainException(message);
        }
    }

    public static void Range(int value, int minimum, int maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(long value, long minimum, long maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void Range(DateTime value, DateTime minimum, DateTime maximum, string message)
    {
        if (value < minimum || value > maximum)
        {
            throw new DomainException(message);
        }
    }

    public static void True(bool boolValue, string message)
    {
        if (!boolValue)
        {
            throw new DomainException(message);
        }
    }

    public static void IsEnum(object value, Type enumType, string message)
    {
        if (!Enum.IsDefined(enumType, value))
        {
            throw new DomainException(message);
        }
    }
}
