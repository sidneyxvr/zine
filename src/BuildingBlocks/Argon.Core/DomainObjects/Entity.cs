using Argon.Core.Messages;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public abstract class Entity<T>
        where T : struct
    {
        public T Id { get; set; }

        protected Entity(T id = default)
        {
            Id = id;
        }

        private readonly List<Event> _domainEvents = new();
        public IReadOnlyCollection<Event> DomainEvents 
            => _domainEvents.AsReadOnly();

        public void AddDomainEvent(Event @event)
        {
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(Event @event)
        {
            _domainEvents?.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ 31;
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
