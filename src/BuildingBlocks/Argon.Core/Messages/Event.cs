using MediatR;
using System;

namespace Argon.Core.Messages
{
    public abstract record Event : Message, INotification
    {
        public Guid AggregateId { get; protected set; }
        public DateTime Timestamp { get; private set; }

        protected Event()
            => Timestamp = DateTime.UtcNow;
    }
}
