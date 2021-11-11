using Argon.Zine.Ordering.Domain;
using System.Threading.Tasks;

namespace Argon.Zine.Ordering.Infra.Data
{
    public class SequencialIdentifier : ISequencialIdentifier
    {
        static int sequentialId = 1000;
        public Task<int> GetSequentialIdAsync()
            => Task.FromResult(sequentialId++);
    }
}
