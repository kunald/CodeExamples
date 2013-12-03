using System;
using System.Collections.Generic;

namespace Insight123.Contract
{
    public interface IEventStorage<TEvent> where TEvent : IEvent
    {
        IEnumerable<TEvent> GetEvents(Guid aggregateId);

        void  Save<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : class, IEventProvider<TEvent>, new();
    }
}
