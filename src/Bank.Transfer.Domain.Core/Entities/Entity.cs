using Bank.Transfer.Domain.Core.Messages;
using System;
using System.Collections.Generic;

namespace Bank.Transfer.Domain.Core.Entities
{
    public abstract class Entity
    {
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();
        public Guid Id { get; protected set; }

        public void AddEvent(Event evento)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(evento);

        }
        
        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }
        public void CleanEvents()
        {
            _notifications?.Clear();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
