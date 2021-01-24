using Argon.Core.Internationalization;
using Argon.Core.Messages.Events;
using System;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Localizer Localizer;

        protected Entity()
        {
            Id = Guid.NewGuid();
            Localizer = Localizer.GetLocalizer();
        }

        private List<Event> _domainEvents;
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(Event @event)
        {
            _domainEvents ??= new List<Event>();
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

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
