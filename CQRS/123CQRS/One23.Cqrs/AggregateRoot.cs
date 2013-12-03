using System;
using System.Collections.Generic;
using System.Linq;
using Insight123.Contract;

namespace Insight123.Base
{
    public abstract class AggregateRoot<TEvent> : IEventProvider<TEvent> where TEvent : IEvent
    {
        private readonly List<TEvent> _changes;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            Version = -1;
            EventVersion = -1;
            _changes = new List<TEvent>();
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        protected void ApplyChange(TEvent @event)
        {
            ApplyChange(@event, true);
            EventVersion++;
            @event.Version = EventVersion;

        }

        private void ApplyChange(TEvent @event, bool isNew)
        {
            dynamic d = this;

            d.Handle(Converter.ChangeTo(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }

        }

        public void LoadsFromHistory(IEnumerable<TEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
            Version = history.Last().Version;
            EventVersion = Version;
        }

        IEnumerable<TEvent> IEventProvider<TEvent>.GetUncommittedChanges()
        {
            return _changes;
        }
    }
}
