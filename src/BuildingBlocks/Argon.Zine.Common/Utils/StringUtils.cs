namespace Argon.Zine.Commom.Utils;

public static class StringUtils
{
    public static string OnlyNumbers(this string str)
    {
        return new string(str.Where(char.IsDigit).ToArray());
    }
}