using Argon.Zine.Core.Messages;

namespace Argon.Zine.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity()
            => Id = Guid.NewGuid();

        private readonly List<Event> _domainEvents = new();
        public IReadOnlyCollection<Event> DomainEvents
            => _domainEvents?.AsReadOnly() ?? new List<Event>().AsReadOnly();

        public void AddDomainEvent(Event @event)
            => _domainEvents.Add(@event);

        public void RemoveDomainEvent(Event @event)
            => _domainEvents?.Remove(@event);

        public void ClearDomainEvents()
            => _domainEvents?.Clear();

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity a, Entity b)
            => !(a == b);

        public override int GetHashCode()
            => Id.GetHashCode() ^ 31;

        public override string ToString()
            =>  $"{GetType().Name} [Id={Id}]";
    }
}
