using Argon.Ordering.Domain;
using System.Threading.Tasks;

namespace Argon.Ordering.Infra.Data
{
    public class SequencialIdentifier : ISequencialIdentifier
    {
        static int sequentialId = 1000;
        public Task<int> GetSequentialId()
            => Task.FromResult(sequentialId++);
    }
}
