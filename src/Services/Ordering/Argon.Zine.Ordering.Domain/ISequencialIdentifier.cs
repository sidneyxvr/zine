namespace Argon.Zine.Ordering.Domain;

public interface ISequencialIdentifier
{
    Task<int> GetSequentialIdAsync();
}