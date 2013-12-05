using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insight123.Base.Exceptions;
using Insight123.Contract;

namespace Insight123.Base
{
    public class InMemoryEventStorage : IEventStorage
    {
        private List<IEvent> _events;
        private readonly IEventBus _eventBus;

        public InMemoryEventStorage(IEventBus eventBus)
        {
            _events = new List<IEvent>();
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

        public void Save(IEventProvider eventProvider)
        {
            var uncommittedChanges = eventProvider.GetUncommittedChanges();

            foreach (var @event in uncommittedChanges)
            {
                _events.Add(@event);
            }
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = ChangeTo(@event, @event.GetType());
                _eventBus.Publish(desEvent);
            }
        }

        public IEnumerable<IEvent> GetAllEvents(Guid aggregateId)
        {
            var events = _events.Where(p => p.AggregateId == aggregateId).Select(p => p);
            if (!events.Any())
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }
            return events;
        }


        public IEnumerable<IEvent> GetEventsFromVersion<TAggregate>(Guid aggregateId, int version) where TAggregate : class, IEventProvider, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> GetAllEvents<TAggregate>(Guid aggregateId) where TAggregate : class, IEventProvider, new()
        {
            throw new NotImplementedException();
        }
    }
}
