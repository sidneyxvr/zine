using System.Threading.Tasks;

namespace Argon.Ordering.Domain
{
    public interface ISequencialIdentifier
    {
        Task<int> GetSequentialId();
    }
}
