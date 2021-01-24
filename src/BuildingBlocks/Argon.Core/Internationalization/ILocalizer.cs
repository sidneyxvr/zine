namespace Argon.Core.Internationalization
{
    public interface ILocalizer
    {
        string GetValue(string key);
        string this[string key] { get; }
    }
}
