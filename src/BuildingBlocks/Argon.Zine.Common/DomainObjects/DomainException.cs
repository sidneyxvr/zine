namespace Argon.Zine.Commom.DomainObjects;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}