using Insight.Cqrs.Domain;
using Insight.Cqrs.Events;
using System;
using System.Collections.Generic;

namespace Insight.Cqrs.Storage
{
    public interface IEventStorage
    {
        IEnumerable<Event> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);
    }
}
