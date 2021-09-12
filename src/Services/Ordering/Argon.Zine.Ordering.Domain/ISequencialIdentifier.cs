using System.Threading.Tasks;

namespace Argon.Zine.Ordering.Domain
{
    public interface ISequencialIdentifier
    {
        Task<int> GetSequentialId();
    }
}
