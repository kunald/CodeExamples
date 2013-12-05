using System;
using System.Collections.Generic;

namespace Insight123.Contract
{
    public interface IEventStorage
    {
        IEnumerable<IEvent> GetAllEvents<TAggregate>(Guid aggregateId) where TAggregate : class, IEventProvider, new();
        //IEnumerable<IEvent> GetAllEvents(Guid aggregateId);

        IEnumerable<IEvent> GetEventsFromVersion<TAggregate>(Guid aggregateId, int version)
            where TAggregate : class, IEventProvider, new();
        //TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IEventProvider<TEvent>, new();
        //TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IEventProvider<TEvent>, new();
        void Save(IEventProvider eventProvider);
    }
}
