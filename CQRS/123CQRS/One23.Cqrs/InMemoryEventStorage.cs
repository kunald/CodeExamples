using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insight123.Base.Exceptions;
using Insight123.Contract;

namespace Insight123.Base
{
    public class InMemoryEventStorage<TEvent> : IEventStorage<TEvent> where TEvent : IEvent
    {
        private List<TEvent> _events;
        private readonly IEventBus _eventBus;

        public InMemoryEventStorage(IEventBus eventBus)
        {
            _events = new List<TEvent>();
            _eventBus = eventBus;
        }

        public static Action<object> Convert<T>(Action<T> myActionT)
        {
            if (myActionT == null) return null;
            return new Action<object>(o => myActionT((T)o));
        }

        public static dynamic ChangeTo(dynamic source, Type dest)
        {

            return System.Convert.ChangeType(source, dest);
        }

        public IEnumerable<TEvent> GetEvents(Guid aggregateId)
        {
            var events = _events.Where(p => p.AggregateId == aggregateId).Select(p => p);
            if (!events.Any())
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }
            return events;
        }

        public void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IEventProvider<TEvent>, new()
        {
            var uncommittedChanges = aggregateRoot.GetUncommittedChanges();
            //var version = aggregateRoot.Version;

            foreach (var @event in uncommittedChanges)
            {
                //version++;
                //@event.Version = version;
                _events.Add(@event);
            }
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }
    }
}
